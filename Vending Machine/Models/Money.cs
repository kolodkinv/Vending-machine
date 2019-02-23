using System.ComponentModel.DataAnnotations;
using Vending_Machine.Storage;

namespace Vending_Machine.Models
{
    public class Money : ICount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool Enable { get; set; }
    }
}