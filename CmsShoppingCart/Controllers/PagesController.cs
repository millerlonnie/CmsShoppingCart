using CmsShoppingCart.Models;
using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels;
using CmsShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Controllers
{
    public class PagesController : Controller
    {
        CmsShoppingCartContext db = new CmsShoppingCartContext();
        // GET: Index/{page}  //{page}---menas option page
        public ActionResult Index(string page ="") //page parameter with an empty string
        {
            //Get/set page slug
            if (page == "") //if page is rqual to an empty string
                page = "home";

            //Declare model and DTO
            PageVM model;
            Page pageDto;

            //Check if page exists

            if(! db.Pages.Any( x => x.Slug.Equals(page)))
            {
                return RedirectToAction("Index", new { page =""}); //redirect to home if there's no match & page equals/= an empty string
            }

            //Get page DTO
            pageDto = db.Pages.Where(x => x.Slug == page).FirstOrDefault(); //if no first or default it returns null

            //Set page title
            ViewBag.PageTitle = pageDto.Title;

            //Check for sidebar
            if(pageDto.HasSidebar == true)
            {
                ViewBag.Sidebar = "Yes";
            }
            else
            {
                ViewBag.Sidebar = "No";
            }

            //Init model
            model = new PageVM(pageDto);
            //Return view with model


            return View(model);
        }

        public ActionResult _PagesMenuPartial()
        {
            // Declare a list of PageVM
            List<PageVM> pageVMList;

            // Get all pages except home
            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                pageVMList = db.Pages.ToArray().OrderBy(x => x.Sorting).Where(x => x.Slug != "home").Select(x => new PageVM(x)).ToList();
            }
            // Return partial view with list
            return PartialView(pageVMList);


            
        }

        public ActionResult _SidebarPartial()
        {
            //Declare model
            SidebarVM model;

            //Init model
            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                Sidebar dto = db.Sidebars.Find(1);

                model = new SidebarVM(dto);
            }


            //Return partial view with model
            return PartialView(model);
        }

    }
}