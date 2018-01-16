using System.Data;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Code.Models.Base;
using WebApplication1.Code.DAL;

namespace WebApplication1.Controllers
{
    public class CurrencyController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Currency
        public ActionResult Index(string sort, int? page)
        {
            ViewBag.CurrencySort = sort == "currency_asc" ? "currency_desc" : "currency_asc";
            ViewBag.CurrencyIdSort = sort == "currencyId_asc" ? "currencyId_desc" : "currencyId_asc";

            var column = from s in db.Currency
                         select s;

            switch (sort)
            {
                case "currencyId_asc":
                    column = column.OrderBy(s => s.CurrencyId);
                    break;
                case "currencyId_desc":
                    column = column.OrderByDescending(s => s.CurrencyId);
                    break;
                case "currency_asc":
                    column = column.OrderBy(s => s.Currency);
                    break;
                case "currency_desc":
                    column = column.OrderByDescending(s => s.Currency);
                    break;
                default:
                    column = column.OrderBy(s => s.CurrencyId);
                    break;
            }

            int pageSize = 10;
            int pageNum = (page ?? 1);

            return View(column.ToPagedList(pageNum, pageSize));
        }

        public ActionResult CreateCurrency()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateCurrency(CurrencyModel model)
        {
            if (ModelState.IsValid)
            {
                bool done = ClientDAL.AddCurrency(model);
                if (done)
                {
                    ViewBag.Message = "Pridanie peňažnej meny bolo úspešné!";
                }
                else
                {
                    ViewBag.Message = "Nastala chyba! Pridanie peňažnej meny bolo neúspešné! Kontaktujte centrum pomoci.";
                    return View(model);
                }

                model = new CurrencyModel();
                ModelState.Clear();
            }
            return View(model);
        }
    }
}