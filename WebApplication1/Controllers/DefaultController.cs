using System;
using System.Data;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Code.Models.Base;
using WebApplication1.Code.DAL;

namespace WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Index(string sort, string currentFilter, string search, int? page)
        {
            ViewBag.UsernameSort = sort == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.LastnameSort = sort == "Username_asc" ? "Username_desc" : "Username_asc";
            ViewBag.BalanceSort = sort == "Balance_asc" ? "Balance_desc" : "Balance_asc";
            ViewBag.CurrencySort = sort == "Currency_asc" ? "Currency_desc" : "Currency_asc";

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            ViewBag.CurrentFilter = search;

            var column = from s in db.Clients
                         select s;

            if (!String.IsNullOrEmpty(search))
            {
                column = column.Where(s =>
                    s.Username.ToUpper().Contains(search.ToUpper()));
            }
            switch (sort)
            {
                case "name_asc":
                    column = column.OrderBy(s => s.Username);
                    break;
                case "name_desc":
                    column = column.OrderByDescending(s => s.Username);
                    break;
                case "Username_asc":
                    column = column.OrderBy(s => s.Lastname);
                    break;
                case "Username_desc":
                    column = column.OrderByDescending(s => s.Lastname);
                    break;
                case "Balance_asc":
                    column = column.OrderBy(s => s.Balance);
                    break;
                case "Balance_desc":
                    column = column.OrderByDescending(s => s.Balance);
                    break;
                case "Currency_asc":
                    column = column.OrderBy(s => s.Currency);
                    break;
                case "Currency_desc":
                    column = column.OrderByDescending(s => s.Currency);
                    break;

                default:
                    column = column.OrderBy(s => s.Username);
                    break;
            }

            int pageSize = 10;
            int pageNum = (page ?? 1);

            return View(column.ToPagedList(pageNum, pageSize));
        }


        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(ClientModel model)
        {
            bool done = ClientDAL.AddClient(model);
            if (done)
            {
                ViewBag.Message = "Vytvorenie klienta bolo úspešné!";
            }
            else if(model.Balance == null)
            {
                ViewBag.Message = "Nastala chyba! Vytvorenie klienta bolo neúspešné! Potrebné vyplniť povinné položky.";
                return View(model);
            }
            else if (model.Balance != null)
            {
                ViewBag.Message = "Nastala chyba! Vytvorenie klienta bolo neúspešné! klient už existuje.";
                return View(model);
            }

            model = new ClientModel();
            ModelState.Clear();

            return View(model);
        }

        public ActionResult EditAccount(int? id)
        {
            ClientModel model = ClientDAL.GetClient(id);
            //bool done = ClientDAL.UpdateClient(ClientDAL.GetClient(id));
            //if (done)
            //{
            //    ViewBag.Message = "Údaje úspešne aktualizované!";
            //}
            //else
            //{
            //    ViewBag.Message = "Nastala chyba! Modifikácia údajov neúspešná! Kontaktujte centrum pomoci.";
            //}
            return View(model);
        }

        [HttpPost]
        public ActionResult EditAccount(int id, ClientModel model)
        {
            //model.ClientId = id;

            bool done = ClientDAL.UpdateClient(model);
            if (done)
            {
                ViewBag.Message = "Údaje úspešne aktualizované!";
            }
            else
            {
                ViewBag.Message = "Nastala chyba! Modifikácia údajov neúspešná! Kontaktujte centrum pomoci.";

            }
            return View(model);
        }

        public ActionResult DeleteAccount(int id)
        {
            ClientModel model = ClientDAL.GetClient(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteAccount(int id, ClientModel model)
        {
            model.ClientId = id;

            bool done = ClientDAL.DeleteClient(id);
            if (done)
            {
                ViewBag.Message = "Údaje úspešne vymazané!";
            }
            else
            {
                ViewBag.Message = "Nastala chyba! Vymazanie údajov neúspešné! Kontaktujte centrum pomoci.";

            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientModel model = db.Clients.Find(id);
            db.Clients.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CreatePaymentOrder()
        {
            PaymentOrderModel model = new PaymentOrderModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePaymentOrder(PaymentOrderModel model)
        {
            bool done = ClientDAL.AddTransaction(model);
            if (done)
            {
                ModelState.Clear();
                model = new PaymentOrderModel();
                ViewBag.Message = "Vytvorenie transakcie bolo úspešné!";
            }
            else
            {
                ViewBag.Message = "Nastala chyba! Vytvorenie transakcie bolo neúspešné! Nemáte dostatočné prostriedky na vykonanie transakcie.";
            }
            return View(model);
        }

        public ActionResult CreateBulkPaymentOrder()
        {
            BulkPaymentOrderModel model = new BulkPaymentOrderModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateBulkPaymentOrder(BulkPaymentOrderModel model)
        {
            bool done = ClientDAL.AddTransaction(model);
            if (done)
            {
                ModelState.Clear();
                model = new BulkPaymentOrderModel();
                ViewBag.Message = "Vytvorenie transakcie bolo úspešné!";
            }
            else
            {
                ViewBag.Message = "Nastala chyba! Vytvorenie transakcie bolo neúspešné! Nemáte dostatočné prostriedky na vykonanie transakcie.";
            }
            return View(model);
        }
    }
}