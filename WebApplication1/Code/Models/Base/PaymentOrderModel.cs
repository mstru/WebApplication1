using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using WebApplication1.Code.DAL;

namespace WebApplication1.Code.Models.Base
{
    public class PaymentOrderModel : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        [Key]
        [Display(Name = "Id Transakcie")]
        public int TransId { get; set; }

        [Display(Name = "Id Klienta")]
        public Nullable<int> ClientId { get; set; }

        [Display(Name = "Typ transakcie")]
        public string TransType { get; set; }

        [Display(Name = "Dátum zadania")]
        public Nullable<System.DateTime> TransDate { get; set; }

        [Display(Name = "Suma")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> TransAmount { get; set; }

        [Display(Name = "Peňažná mena")]
        public string Currency { get; set; }

        public virtual ClientModel Client { get; set; }
    }
}