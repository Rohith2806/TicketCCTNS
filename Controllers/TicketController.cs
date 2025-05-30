using logindemo.Data;
using logindemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace logindemo.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TicketController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Ticket/Index
        public IActionResult Index()
        {
            // Redirect to Create since that's your main ticket entry page
            return RedirectToAction("Create");
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            var model = new Ticket
            {
                TicketId = Guid.NewGuid().ToString()
            };
            return View(model);
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill out all required fields.");
                return View(ticket);
            }

            // Ensure TicketId is generated
            if (string.IsNullOrWhiteSpace(ticket.TicketId))
            {
                ticket.TicketId = Guid.NewGuid().ToString();
            }

            // Handle image upload
            if (ticket.ImageFile != null && ticket.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(ticket.ImageFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ticket.ImageFile.CopyToAsync(fileStream);
                }

                ticket.ImagePath = "/uploads/" + uniqueFileName;
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
