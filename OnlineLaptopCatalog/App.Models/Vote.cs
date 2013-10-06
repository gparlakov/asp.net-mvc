using System.ComponentModel.DataAnnotations;
namespace App.Models
{
    public class Vote
    {
        public int Id { get; set; }
        
        public int LaptopId { get; set; }

        public virtual Laptop Laptop { get; set; }

        public string UserId { get; set; }

        public virtual AppUser User { get; set; }
    }
}