using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using WebApplication1.Code.DAL;

namespace WebApplication1.Code.Models.Base
{
    public class BulkPaymentOrderModel : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        [Key]
        [Display(Name = "Id Transakcie")]
        public int TransId { get; set; }

        [Display(Name = "Dátum zadania")]
        public Nullable<System.DateTime> TransDate { get; set; }

        [Display(Name = "Suma")]
        [DisplayFormat(DataFormatString = "{0:0.0000}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> TransAmount { get; set; }

        [Display(Name = "Zadávateľ")]
        public Nullable<int> ClientIDSender { get; set; }

        [Display(Name = "Prijímateľ")]
        public Nullable<int> ClientIDRecipient { get; set; }

        [Display(Name = "Peňažná mena")]
        public string Currency { get; set; }

        public virtual ClientModel Client { get; set; }
    }
}