using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vending_Machine.Storage;

namespace Vending_Machine.Models
{
    public class Money : ICount
    {
        [Key]
        public int Id { get; set; }
        public int Count { get; set; }
        // TODO Убрать кодировку
        [Column(TypeName = "varchar(128) character set utf8")]
        public string Name { get; set; }
        public int Cost { get; set; }
        public bool Enable { get; set; }
    }
}