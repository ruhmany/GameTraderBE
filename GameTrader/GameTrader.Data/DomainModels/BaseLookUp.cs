using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace GameTrader.Data.DomainModels
{
    public class BaseLookUp
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
