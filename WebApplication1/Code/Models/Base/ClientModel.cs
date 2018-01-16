using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using WebApplication1.Code.DAL;

namespace WebApplication1.Code.Models.Base
{
    public class ClientModel : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]    
        public int ClientId { get; set; }

        [Display(Name = "Meno")]
        [Required(ErrorMessage = "{0}: klienta je povinná položka")]
        [Remote("IsClientExist", "Catalog", ErrorMessage = "{0} klienta existuje.")]
        [StringLength(30)]
        public string Username { get; set; }

        [Display(Name = "Priezvisko")]
        [Required(ErrorMessage = "{0}: klienta je povinná položka")]
        [Remote("IsClientExist", "Catalog", ErrorMessage = "{0} klienta existuje.")]
        [StringLength(30)]
        public string Lastname { get; set; }

        [Display(Name = "Vklad")]
        [Required(ErrorMessage = "{0}: na účet je povinná položka")]
        [Range(3.0, 1000000000.0, ErrorMessage = "{0}: pre vytvorenie nového účtu je potrebné vložiť minimálne množstvo {1}.")]
        [DisplayFormat(DataFormatString = "{0:0.000}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Balance { get; set; }

        [Display(Name = "Peňažná mena")]
        [Required(ErrorMessage = "{0} je povinná položka")]
        public string Currency { get; set; }

        public virtual ICollection<BulkPaymentOrderModel> BulkPaymentOrder { get; set; }

        public virtual ICollection<PaymentOrderModel> PaymentOrder { get; set; }

        protected override void Seed(ApplicationDbContext context)
        {            
            IList<ClientModel> initData = new List<ClientModel>();

            initData.Add(new ClientModel() { ClientId = 1, Username = "Darth", Lastname = "Vader", Currency = "1", Balance = 220 });
            initData.Add(new ClientModel() { ClientId = 2, Username = "Leia", Lastname = "Leia", Currency = "1", Balance = 200 });
            initData.Add(new ClientModel() { ClientId = 3, Username = "Luke", Lastname = "Skywalker", Currency = "1", Balance = 139 });
            initData.Add(new ClientModel() { ClientId = 4, Username = "Yoda", Lastname = "Yoda", Currency = "1", Balance = 500});
            initData.Add(new ClientModel() { ClientId = 5, Username = "Kylo", Lastname = "Ren", Currency = "1", Balance = 1900 });
            initData.Add(new ClientModel() { ClientId = 6, Username = "Han", Lastname = "Solo", Currency = "1", Balance = 900 });
            initData.Add(new ClientModel() { ClientId = 7, Username = "Boba", Lastname = "Fett", Currency = "1", Balance = 90 });
            initData.Add(new ClientModel() { ClientId = 8, Username = "Clone", Lastname = "Trooper", Currency = "1", Balance = 4000 });
            initData.Add(new ClientModel() { ClientId = 9, Username = "Obi-Wan", Lastname = "Kenobi", Currency = "1", Balance = 1000 });
            initData.Add(new ClientModel() { ClientId = 10, Username = "R2-D2", Lastname = "R2-D2", Currency = "1", Balance = 8000});
            initData.Add(new ClientModel() { ClientId = 11, Username = "Rey", Lastname = "Rey", Currency = "1", Balance = 4511 });
            initData.Add(new ClientModel() { ClientId = 12, Username = "Chewbacca", Lastname = "Chewbacca", Currency = "1", Balance = 7811 });
            initData.Add(new ClientModel() { ClientId = 13, Username = "Jabba", Lastname = "Hutt", Currency = "1", Balance = 4000 });
            initData.Add(new ClientModel() { ClientId = 14, Username = "Darth", Lastname = "Maul", Currency = "1", Balance = 4522 });
            initData.Add(new ClientModel() { ClientId = 15, Username = "Qui-Jon", Lastname = "Jinn", Currency = "1", Balance = 7521 });
            initData.Add(new ClientModel() { ClientId = 16, Username = "Mace", Lastname = "Windu", Currency = "1", Balance = 1122 });
            initData.Add(new ClientModel() { ClientId = 17, Username = "Jar Jar", Lastname = "Binks", Currency = "1", Balance = 5555 });
            initData.Add(new ClientModel() { ClientId = 18, Username = "Dooku", Lastname = "Dooku", Currency = "1", Balance = 500 });
            initData.Add(new ClientModel() { ClientId = 19, Username = "Jango", Lastname = "Fett", Currency = "1", Balance = 500 });

            foreach (ClientModel c in initData)
                context.Clients.Add(c);

            base.Seed(context);
        }
    }
}