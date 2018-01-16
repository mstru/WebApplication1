using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Code.Models.Base;

namespace WebApplication1.Code.DAL
{
    public class ClientDAL
    {
        public static bool AddClient(ClientModel client)
        {
            try
            {           
                ApplicationDbContext ctx = new ApplicationDbContext();

                var existUsername = ctx.Clients.Any(ag => ag.Username == client.Username);
                var existLastname = ctx.Clients.Any(ag => ag.Lastname == client.Lastname);

                if (!existUsername || !existLastname)
                {
                    ctx.Clients.Add(client);
                    ctx.SaveChanges();
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.Add() --> " + ex.Message);
                return false;
            }
        }


        public static bool UpdateClient(ClientModel updatedClient)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();

                var existUsername = ctx.Clients.Any(ag => ag.Username == updatedClient.Username);
                var existLastname = ctx.Clients.Any(ag => ag.Lastname == updatedClient.Lastname);

                if (!existUsername || !existLastname)
                {
                    ctx.Clients.Attach(updatedClient);
                    ctx.Entry(updatedClient).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.Update() --> " + ex.Message);
                return false;
            }
        }

        public static List<ClientModel> GetClients(string username)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var accounts = from a in ctx.Clients
                               where a.Username == username
                               select a;
                return accounts.ToList<ClientModel>();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.GetClients() --> " + ex.Message);
                return null;
            }
        }

        public static ClientModel GetClient(int? id)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var account = ctx.Clients.Find(id);
                return account;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.GetClient() --> " + ex.Message);
                return null;
            }

        }

        public static bool DeleteClient(int id)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var account = ctx.Clients.Find(id);

                //remove all trans first  
                //account.BulkPaymentOrder.ToList().ForEach(t => account.BulkPaymentOrder.Remove(t));

                //remove client 
                ctx.Clients.Remove(account);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.Delete() --> " + ex.Message);
                return false;
            }
        }


        public static bool AddTransaction(BulkPaymentOrderModel trans)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.BulkPaymentOrder.Add(trans);

                if (trans.ClientIDSender != trans.ClientIDRecipient)
                {
                    //Odosielatel
                    if (trans.ClientIDSender != null && trans.ClientIDSender > 0)
                    {
                        ClientModel accountSender = ctx.Clients.Find(trans.ClientIDSender);
                        accountSender.Balance -= trans.TransAmount;
                    }

                    //Prijimatel
                    if (trans.ClientIDRecipient != null && trans.ClientIDRecipient > 0)
                    {
                        ClientModel accountRecipient = ctx.Clients.Find(trans.ClientIDRecipient);
                        accountRecipient.Balance += trans.TransAmount;
                    }
                }
                else
                {
                    return false;
                }

                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.AddTransaction() --> " + ex.Message);
                return false;
            }
        }

        public static bool AddTransaction(PaymentOrderModel trans)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                ctx.PaymentOrder.Add(trans);

                ClientModel account = ctx.Clients.Find(trans.ClientId);
               
                if (trans.TransType == "Vklad")
                    account.Balance += trans.TransAmount;
                else
                    account.Balance -= trans.TransAmount;

                if(account.Balance < 0)
                {
                    return false;
                }

                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.AddTransaction() --> " + ex.Message);
                return false;
            }
        }

        public static bool AddCurrency(CurrencyModel model)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();

                List<string> myList = new List<string>() { "USD", "EUR", "GBP", "INR", "AUD", "CAD", "SGD", "CHF", "MYR", "JPY", "CNY" };

                bool currencySupport = myList.Contains(model.Currency);
                bool exist = ctx.Currency.Any(ag => ag.Currency == model.Currency);

                if (currencySupport && !exist)
                {                   
                    ctx.Currency.Add(model);
                    ctx.SaveChanges();
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.Add() --> " + ex.Message);
                return false;
            }
        }

        public static List<CurrencyModel> GetCurrencies(int id)
        {
            try
            {
                ApplicationDbContext ctx = new ApplicationDbContext();
                var curr = from t in ctx.Currency
                            where t.CurrencyId == id
                            select t;
                return curr.ToList<CurrencyModel>();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Write("ClientDAL.GetCurrencies() --> " + ex.Message);
                return null;
            }
        }


        //public static List<BulkPaymentOrderModel> SearchTransactions(string username, string starttranAmount, string endtranAmount)
        //{
        //    try
        //    {
        //        DateTime sd = DateTime.Parse(starttranAmount);
        //        DateTime ed = DateTime.Parse(endtranAmount);

        //        ApplicationDbContext ctx = new ApplicationDbContext();
        //        var trans = from t in ctx.BulkPaymentOrder
        //                    where t.Client.Username == username && t.TransAmount.Value >= starttranAmount && t.TransAmount.Value <= endtranAmount
        //                    orderby t.TransDate descending
        //                    select t;
        //        return trans.ToList<BulkPaymentOrderModel>();
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.Current.Trace.Write("ClientDAL.SearchTransactions() --> " + ex.Message);
        //        return null;
        //    }
        //}
    }
}