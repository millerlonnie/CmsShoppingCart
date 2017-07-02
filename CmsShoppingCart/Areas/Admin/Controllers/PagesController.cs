using CmsShoppingCart.Models;
using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels;
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

        // GET: Admin/Pages/Add Page
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        // Post: Admin/Pages/Add Page
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
            //check model state //first thing done after submiiting a form
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                //Declare slug
                string slug;

                //initialise DTO
                Page dto = new Page();

                //DTO title
                dto.Title = model.Title;


                //check for set slug if need be
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower(); //replaces input title any white spaces with -
                }                                                   // and converts to lowercase

                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower(); //replaces input title any white spaces with -
                }                                                 // and converts to lowercase

                //make syre title and slug are unique
                if (db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "Title or slug already exists.");
                    return View(model);
                }

                //DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100; //when ever you add a page it will be the last page, since there wont be more than 100 items in the menu



                //Save DTO
                db.Pages.Add(dto);
                db.SaveChanges();
            }
            //set TempData message
            TempData["SM"] = "You have added a new page!"; //tempdata persists ulinke view bag

            //Redirect

            return RedirectToAction("AddPage");
        }

        // GET: Admin/Pages//Edit Page/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {

            //declarre page vm
            PageVM model;

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                //get page
                Page dto = db.Pages.Find(id);

                //confirm page exists
                if(dto == null)
                {
                    return Content("The page dose not exist."); // returns a string
                }

                //ini page vm

                model = new PageVM(dto); //alternatively if we had no view modelPgage Ve would be:
                                 /* model = new PageVM()
                                      {
                                         Id = dto.Id,
                                          Body = dto.Body, .......
                                       }; */
            }
            //return view with model
            return View(model);
        }

        //post: Admin/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
            //check model state
            if(! ModelState.IsValid)
            {
                return View(model);

            }

            //get page id
            int id = model.Id;

            //declace slug
            string slug = "home";

            //Get the page
            Page dto = db.Pages.Find(id);


            //DTO the title
            dto.Title = model.Title;

            //check for slug and set it if need be
            if(model.Slug != "home")
            {
                if(string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

            }

            //make sure title and slug are unique
            if(db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title) || 
               db.Pages.Where(x => x.Id != id).Any(x => x.Slug  == slug))
            {
                ModelState.AddModelError("", "That title or slug already exists.");
                return View(model);
            }

            //DTO the rest
            dto.Slug = slug;
            dto.Body = model.Body;
            dto.HasSidebar = model.HasSidebar;


            //Save the DTO
            db.SaveChanges();

            //set TempData message
            TempData["SM"] = "You have edited the page!";

            //Redirect

            return RedirectToAction("EditPage");
        }

        // GET: Admin/Pages/PageDetails/id
        public ActionResult PageDetails(int id)
        {
            //Declare Page VM
            PageVM model;

            //Get the page
            Page dto = db.Pages.Find(id);
            //Confirm page exsists
            if(dto == null)
            {
                return Content("The page dose not exist!");
            }

            //Ini Page VM
            model = new PageVM(dto);

            //retun view with model

            return View(model);
        }


        // GET: Admin/Pages/DeletePage/id
        public ActionResult DeletePage(int id)
        {
            //Get the page
            Page dto = db.Pages.Find(id);

            //Remove the page
            db.Pages.Remove(dto);

            //Save 
            db.SaveChanges();
            //Redirect

            return RedirectToAction("Index");

        }

        // Post: Admin/Pages/ReorderPages
        [HttpPost]
        public void ReorderPages(int[] id) //with an array //method saves sorted chnages on index
        {
            //set initial count
            int count = 1; //home is 0, 
            //declare page DTO
            Page dto;

            //set sorting for each page
            foreach(var pageId in id)
            {
                dto = db.Pages.Find(pageId);
                dto.Sorting = count;

                db.SaveChanges();

                count++; //incremenet count by 1

            }

        }

        // GET: Admin/Pages/EditSidebar
        [HttpGet]
        public ActionResult EditSidebar()
        {

            //declare model
            SidebarVM model;


            //get the DTO
            Sidebar dto = db.Sidebars.Find(1);
            //ini model
            model = new SidebarVM(dto);

            // return view with model
            return View(model);

        }

        [HttpPost]
        public ActionResult EditSidebar(SidebarVM model)
        {
            //Get the Dto
            Sidebar dto = db.Sidebars.Find(1);

            //Dto the body
            dto.Body = model.Body;

            //save Db
            db.SaveChanges();

            //set TempData message
            TempData["SM"] = "You have edited the sidebar!";

            //redirect
            return RedirectToAction("EditSidebar");
        }

    }
}