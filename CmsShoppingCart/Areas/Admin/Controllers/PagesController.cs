using CmsShoppingCart.Models;
using CmsShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller  
    {
        CmsShoppingCartContext db = new CmsShoppingCartContext();
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //Declare list of Page VM
            List<PageVM> pagesList;

            
                //Initialise list

                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
             
            //Return view with list

            return View(pagesList);
        }

        // GET: Admin/Add Page
        public ActionResult AddPage()
        {
            return View();
        }
    }
}