using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class CreditCard
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        public int CardNumber { get; set; }

        public string CardName { get; set; }

        public int CardCVV { get; set; }

    }
}
