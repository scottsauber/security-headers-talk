using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecurityHeadersDemo.Data;
using SecurityHeadersDemo.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace SecurityHeadersDemo.Pages.Shopping
{
    public class CarsModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public CarsModel(ApplicationDbContext applicationDbContext)
        {
            _dbContext = applicationDbContext;
        }

        public List<Item> Items { get; set; }

        [TempData]
        public string LastItemAdded { get; set; }

        public void OnGet()
        {
            Items = _dbContext.Items.ToList();
        }

        public IActionResult OnPost(int itemId)
        {
            _dbContext.CartItems.Add(new CartItem
            {
                ItemId = itemId
            });
            _dbContext.SaveChanges();

            LastItemAdded = _dbContext.Items.Where(i => i.ItemId == itemId).Select(i => i.Name).SingleOrDefault();

            return RedirectToPage();
        }
    }
}