using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Code.DAL;
using WebApplication1.Code.Models.Base;

namespace WebApplication1.Code.Models
{
    public class Utilities
    {
        public static IEnumerable<SelectListItem> GetClients()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            List<SelectListItem> clients = new List<SelectListItem>();
            foreach (ClientModel model in ctx.Clients.ToList<ClientModel>())
            {
                clients.Add(new SelectListItem
                {
                    Text = model.Username + " " + model.Lastname,
                    Value = model.ClientId.ToString()
                });
            }
            return clients;
        }

        public static IEnumerable<SelectListItem> GetCurrencies()
        {
            ApplicationDbContext ctx = new ApplicationDbContext();
            List<SelectListItem> curr = new List<SelectListItem>();
            foreach (CurrencyModel model in ctx.Currency.ToList<CurrencyModel>())
            {
                curr.Add(new SelectListItem
                {
                    Text = model.Currency,
                    Value = model.CurrencyId.ToString()
                });
            }
            return curr;
        }

        public static string GetCurency(string id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();

            CurrencyModel model = ctx.Currency.Find(Int32.Parse(id));
            return model.Currency;
        }



        public static string GetClient(string id)
        {
            ApplicationDbContext ctx = new ApplicationDbContext();

            ClientModel model = ctx.Clients.Find(Int32.Parse(id));
            return model.Username + " " + model.Lastname;
        }
    }
}