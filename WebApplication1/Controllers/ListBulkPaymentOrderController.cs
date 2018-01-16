using System.Data;
using PagedList;
using System.Linq;
using System.Web.Mvc;
using WebApplication1.Code.DAL;
using System;

namespace WebApplication1.Controllers
{
    public class ListBulkPaymentOrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ListBulkPaymentOrder
        public ActionResult Index(string sort, int? page, string searchSender, string searchRecipient, string searchCurrency)
        {        
            var column = from s in db.BulkPaymentOrder
                         select s;

            if (!String.IsNullOrEmpty(searchSender))
            {
                column = column.Where(s => s.ClientIDSender.ToString() == searchSender);
            }

            if (!String.IsNullOrEmpty(searchRecipient))
            {
                column = column.Where(s => s.ClientIDRecipient.ToString() == searchRecipient);
            }


            ViewBag.TransIdSort = sort == "transId_asc" ? "transId_desc" : "transId_asc";
            ViewBag.ClientIDSenderSort = sort == "clientIDSender_asc" ? "clientIDSender_desc" : "clientIDSender_asc";
            ViewBag.ClientIDRecipientSort = sort == "clientIDRecipient_asc" ? "clientIDRecipient_desc" : "clientIDRecipient_asc";
            ViewBag.TransAmountSort = sort == "transAmount_asc" ? "transAmount_desc" : "transAmount_asc";
 
            switch (sort)
            {
                case "transId_asc":
                    column = column.OrderBy(s => s.TransId);
                    break;
                case "transId_desc":
                    column = column.OrderByDescending(s => s.TransId);
                    break;
                case "clientIDSender_asc":
                    column = column.OrderBy(s => s.Client.Username);
                    break;
                case "clientIDSender_desc":
                    column = column.OrderByDescending(s => s.Client.Username);
                    break;
                case "clientIDRecipient_asc":
                    column = column.OrderBy(s => s.Client.Username);
                    break;
                case "clientIDRecipient_desc":
                    column = column.OrderByDescending(s => s.Client.Username);
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