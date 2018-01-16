using System.Data;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Code.DAL;
using System;

namespace WebApplication1.Controllers
{
    public class ListPaymentOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListPaymentOrder
        public ActionResult Index(string sort, int? page, string searchClient, string searchTransType, string searchCurrency)
        {

            var column = from s in db.PaymentOrder
                         select s;

            if (!String.IsNullOrEmpty(searchClient))
            {
                column = column.Where(s => s.Client.ClientId.ToString() == searchClient);
            }

            if (!String.IsNullOrEmpty(searchTransType))
            {
                column = column.Where(s =>s.TransType.ToUpper().Contains(searchTransType.ToUpper()));
            }

            ViewBag.TransIdSort = sort == "transId_asc" ? "transId_desc" : "transId_asc";
            ViewBag.ClientIdSort = sort == "clientId_asc" ? "clientId_desc" : "clientId_asc";
            ViewBag.TransTypeSort = sort == "transType_asc" ? "transType_desc" : "transType_asc";
            ViewBag.TransAmountSort = sort == "transAmount_asc" ? "transAmount_desc" : "transAmount_asc";

            switch (sort)
            {
                case "transId_asc":
                    column = column.OrderBy(s => s.TransId);
                    break;
                case "transId_desc":
                    column = column.OrderByDescending(s => s.TransId);
                    break;
                case "clientId_asc":
                    column = column.OrderBy(s => s.Client.Username);
                    break;
                case "clientId_desc":
                    column = column.OrderByDescending(s => s.Client.Username);
                    break;
                case "transType_asc":
                    column = column.OrderBy(s => s.TransType);
                    break;
                case "transType_desc":
                    column = column.OrderByDescending(s => s.TransType);
                    break;
                case "transAmount_asc":
                    column = column.OrderBy(s => s.TransAmount);
                    break;
                case "transAmount_desc":
                    column = column.OrderByDescending(s => s.TransAmount);
                    break;

                default:
                    column = column.OrderBy(s => s.TransId);
                    break;
            }

            int pageSize = 10;
            int pageNum = (page ?? 1);

            return View(column.ToPagedList(pageNum, pageSize));
        }
    }
}