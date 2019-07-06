using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SecurityHeadersDemo.Data;
using SecurityHeadersDemo.Data.Models;

namespace SecurityHeadersDemo.Pages.Shopping
{
    // Only ignoring to demonstrate Cache-Control
    //[IgnoreAntiforgeryToken(Order = 1001)]
    public class ShoppingCartModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public ShoppingCartModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CartItem> ShoppingCart { get; set; }

        public List<CartItem> PreviousOrders { get; set; }


        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalInCart => ShoppingCart.Sum(c => c.Item.Price);

        public void OnGet()
        {
            ShoppingCart = _dbContext.CartItems
                .Where(c => c.DateTimePurchased == null)
                .Include(c => c.Item)
                .ToList();

            PreviousOrders = _dbContext.CartItems
                .Where(c => c.DateTimePurchased != null)
                .Include(c => c.Item)
                .ToList();
        }

        public IActionResult OnPost()
        {
            var cartItems = _dbContext.CartItems.ToList();

            // Give items all the same data
            var now = DateTime.Now;
            foreach (var item in cartItems)
            {
                item.DateTimePurchased = now;
            }

            _dbContext.SaveChanges();

            return RedirectToPage();
        }
    }
}