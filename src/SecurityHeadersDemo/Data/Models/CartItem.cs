using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityHeadersDemo.Data.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ItemId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy h:mm tt}")]
        public DateTime? DateTimePurchased { get; set; }

        public Item Item { get; set; }
    }
}
