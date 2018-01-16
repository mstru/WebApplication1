using System.Data.Entity;
using WebApplication1.Code.Models.Base;

namespace WebApplication1.Code.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
          : base("DefaultConnection")

        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<ClientModel> Clients { get; set; }

        public DbSet<BulkPaymentOrderModel> BulkPaymentOrder{ get; set; }

        public DbSet<PaymentOrderModel> PaymentOrder { get; set; }

        public DbSet<CurrencyModel> Currency { get; set; }
    }
}