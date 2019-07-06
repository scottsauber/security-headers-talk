using System.ComponentModel.DataAnnotations;

namespace SecurityHeadersDemo.Data.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

    public static class AllItems
    {
        public static Item ModelS = new Item { ItemId = 1, Name = "Model S", Price = 100_000, ImageUrl = "~/images/models.png" };
        public static Item ModelX = new Item { ItemId = 2, Name = "Model X", Price = 80_000, ImageUrl = "~/images/modelx.png" };
        public static Item Model3 = new Item { ItemId = 3, Name = "Model 3", Price = 30_000, ImageUrl = "~/images/model3.png" };

        public static Item[] All => new Item[]
        {
            ModelS,
            ModelX,
            Model3
        };
    }   
}
