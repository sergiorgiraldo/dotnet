using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyStuff.Data;
using MyStuff.Models;
using MyStuff.Shared;

namespace MyStuff.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: Products
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: List
        [HttpGet]
        public IActionResult List()
        {
            var products = _context.Products.Where(p => string.IsNullOrEmpty(p.BuyerName)).ToList();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            Product model = new Product
            {
                DateCreation = DateTime.Today,
                DateSell = new DateTime(1900, 01, 01)
            };
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile fileupload1, IFormFile fileupload2, IFormFile fileupload3, [Bind("Id,Name,Description,Price,BuyerName,BuyerEmail,BuyerPhone,DateCreation,DateSell, Image1, Image2, Image3")] Product product)
        {
            if (ModelState.IsValid)
            {
                Upload(fileupload1, 1, product);
                Upload(fileupload2, 2, product);
                Upload(fileupload3, 3, product);

                _context.Add(product);
                await _context.SaveChangesAsync();
                return LocalRedirect("/Products");
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Reserve/5
        public async Task<IActionResult> Reserve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Doubt/5
        public async Task<IActionResult> Doubt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormFile fileupload1, IFormFile fileupload2, IFormFile fileupload3, [Bind("Id,Name,Description,Price,BuyerName,BuyerEmail,BuyerPhone,DateCreation,DateSell, Image1, Image2, Image3")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Upload(fileupload1, 1, product);
                    Upload(fileupload2, 2, product);
                    Upload(fileupload3, 3, product);

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return LocalRedirect("/Products");
            }
            return View(product);
        }

        // POST: Products/Reserve/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reserve(int id, [Bind("Id,Name,Description,Price,BuyerName,BuyerEmail,BuyerPhone,DateCreation,DateSell, Image1, Image2, Image3")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    string subject = product.Name + " (" + product.Id + ") reservado";
                    string body = "Nome: " + product.BuyerName + Environment.NewLine;
                    body += "Email: " + product.BuyerEmail + Environment.NewLine;
                    body += "Fone: " + product.BuyerPhone + Environment.NewLine;
                    body += "Produto: " + product.Name;
                    Mail.Send(subject, body);

                    TempData["Message"] = "Reservado com sucesso";
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["Message"] = "Ocorreu um erro, tente novamente ou entre em contato conosco";

                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }

        // POST: Products/Doubt/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Doubt(int id, string name, string email, string phone, string doubt)
        {
            string subject = "Dúvida sobre o produto " + name + " (" + id + ")";
            string body = "Email: " + email + Environment.NewLine;
            body += "Phone: " + phone + Environment.NewLine;
            body += "Dúvida: " + doubt;
            Mail.Send(subject, body);

            TempData["Message"] = "Enviado com sucesso";

            return RedirectToAction("List");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return LocalRedirect("/Products");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private void Upload(IFormFile formFile, int Which, Product product)
        {
            if (formFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    formFile.CopyTo(memoryStream);
                    var imageUrl = ImgUr.Upload(memoryStream.ToArray());
                    switch (Which)
                    {
                        case 1: product.Image1 = imageUrl; break;
                        case 2: product.Image2 = imageUrl; break;
                        case 3: product.Image3 = imageUrl; break;
                    }

                }
            }
        }

    }
}
