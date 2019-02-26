using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vending_Machine.Models.Products;

namespace Vending_Machine.Models
{
    public class Image
    {
        public int Id { get; set; }
        public byte[] NormalImage { get; set; }
        
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        
    }
}