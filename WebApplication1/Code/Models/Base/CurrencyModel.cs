using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using WebApplication1.Code.DAL;

namespace WebApplication1.Code.Models.Base
{
    public class CurrencyModel : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        [Key]
        [Display(Name = "ID")]
        public int CurrencyId { get; set; }

        [Required(ErrorMessage = "Peňažná mena je povinná položka")]
        [Display(Name = "Peňažná mena")]
        public string Currency { get; set; }
    }
}