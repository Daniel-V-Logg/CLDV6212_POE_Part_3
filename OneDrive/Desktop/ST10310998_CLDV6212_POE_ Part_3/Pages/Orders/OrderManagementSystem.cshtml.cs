using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ST10310998_CLDV6212_POE__Part_1.Data;
using ST10310998_CLDV6212_POE__Part_1.Models;
using ST10310998_CLDV6212_POE__Part_1.Services;
using System.IO;
using System.Threading.Tasks;

namespace ST10310998_CLDV6212_POE__Part_1.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly BlobStorageService _blobService;
        private readonly QueueStorageService _queueService;
        private readonly TableService _tableService;
        private readonly FileService _fileService;

        public IndexModel(ApplicationDbContext context, BlobStorageService blobService, QueueStorageService queueService, TableService tableService, FileService fileService)
        {
            _context = context;
            _blobService = blobService;
            _queueService = queueService;
            _tableService = tableService;
            _fileService = fileService;
        }

        public async Task<IActionResult> OnPostUploadFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    await _blobService.UploadFileAsync("product-images", file.FileName, stream, file.ContentType);
                }
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostQueueMessageAsync(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                await _queueService.AddMessageAsync("order-queue", message);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddProfileAsync(string firstName, string lastName, string email, string phoneNumber)
        {
            var profile = new CustomerProfile
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber
            };
            await _tableService.AddEntityAsync(profile);
            return RedirectToPage();
        }
    }
}
