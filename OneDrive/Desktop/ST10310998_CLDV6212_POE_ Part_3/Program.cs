using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ST10310998_CLDV6212_POE__Part_1.Data;
using ST10310998_CLDV6212_POE__Part_1.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Azure.Data.Tables;
using ST10310998_CLDV6212_POE__Part_1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDatabase")));

// Register Azure Blob Service and Queue Service
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureStorage:BlobConnectionString")));
builder.Services.AddSingleton(x => new QueueServiceClient(builder.Configuration.GetConnectionString("AzureStorage:QueueConnectionString")));
builder.Services.AddSingleton(x => new ShareServiceClient(builder.Configuration.GetConnectionString("AzureStorage:FileConnectionString")));

// Register custom services
builder.Services.AddTransient<BlobStorageService>();
builder.Services.AddTransient<QueueStorageService>();
builder.Services.AddTransient<FileService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

static async Task Main(string[] args)
{
    // Initialize the TableServiceClient and TableService
    var tableServiceClient = new TableServiceClient("Server=tcp:customerstore.database.windows.net,1433;Initial Catalog=Customer_Store;Persist Security Info=False;User ID={st10310998@vcconnect.edu.za};Password={Pistrito@1};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=\"Active Directory Password\";");
    var tableService = new TableService(tableServiceClient);

    // Create a new customer profile
    var customerProfile = new CustomerProfile("CountryA", "customer123", "John", "Doe", "john.doe@example.com", "1234567890");

    // Add customer profile to the Azure Table
    await tableService.AddEntityAsync(customerProfile);

    // Retrieve the same customer profile
    var retrievedProfile = await tableService.GetEntityAsync("CountryA", "customer123");
    Console.WriteLine($"Retrieved: {retrievedProfile.FirstName} {retrievedProfile.LastName}");

    // Delete the customer profile   wait you need to add details in the connection string
    await tableService.DeleteEntityAsync("CountryA", "customer123");
}

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();