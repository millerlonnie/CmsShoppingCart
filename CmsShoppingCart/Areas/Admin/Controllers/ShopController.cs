using CmsShoppingCart.Models;
using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CmsShoppingCart.Areas.Admin.Controllers
{
    public class ShopController : Controller
    {
         CmsShoppingCartContext db = new CmsShoppingCartContext();
        
        // GET: Admin/Shop/Categories
        public ActionResult Categories()
        {
            // Declare a list of models
            List<CategoryVM> categoryVMList;

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                // Init the list
                categoryVMList = db.Categories
                                .ToArray()
                                .OrderBy(x => x.Sorting)
                                .Select(x => new CategoryVM(x))
                                .ToList();
            }

            // Return view with list
            return View(categoryVMList);
        }

        // POST: Admin/Shop/AddNewCategory
        [HttpPost]
        public string AddNewCategory(string catName)
        {
            // Declare id
            string id;

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                // Check that the category name is unique
                if (db.Categories.Any(x => x.Name == catName))
                    return "titletaken";

                // Init DTO
                Category  dto = new Category ();

                // Add to DTO
                dto.Name = catName;
                dto.Slug = catName.Replace(" ", "-").ToLower();
                dto.Sorting = 100;

                // Save DTO
                db.Categories.Add(dto);
                db.SaveChanges();

                // Get the id
                id = dto.Id.ToString();
            }

            // Return id
            return id;
        }

        // GET: Admin/Shop/DeleteCategory/id
        public ActionResult DeleteCategory(int id)
        {
             
                // Get the category
                Category  dto = db.Categories.Find(id);

                // Remove the category
                db.Categories.Remove(dto);

                // Save
                db.SaveChanges();
          
            // Redirect
            return RedirectToAction("Categories");
        }

        // POST: Admin/Shop/ReorderCategories
        [HttpPost]
        public void ReorderCategories(int[] id)
        {
            
                // Set initial count
                int count = 1;

                // Declare CategoryDTO
                Category  dto;

                // Set sorting for each category
                foreach (var catId in id)
                {
                    dto = db.Categories.Find(catId);
                    dto.Sorting = count;

                    db.SaveChanges();

                    count++;
                }
            }


        //Post: Admin/Shop/RenameCategory
        [HttpPost]
        public string RenameCategory(string newCatName, int id)
        {
            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {

                //check category name is unique
                if (db.Categories.Any(x => x.Name == newCatName))
                    return "title taken";

                //Get DTO
                Category dto = db.Categories.Find(id);

                //Edit DTO
                dto.Name = newCatName;
                dto.Slug = newCatName.Replace(" ", "-").ToLower();
            }


            //Save 
            db.SaveChanges();


            //Return
            return "Ok";
        }

        //Get: Admin/Shop/AddProduct
        [HttpGet]
        public ActionResult AddProduct()
        {
            //initilaise the model
            ProductVM model = new ProductVM();

            //Add select list of categories to model
            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");///get Id and name from Catagegories Tabel
                    
            }

            //Return view with modoel

            return View(model);
        }


        //post back method for adding a product
        //Post: Admin/Shop/AddProduct
        [HttpPost]
        public ActionResult AddProduct(ProductVM model, HttpPostedFileBase file) //caters for uploaed image named file in AddProduct view
        {
            //check model state
            if(!ModelState.IsValid)
            {
                 
                    // populate categories property in the product model
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    return View(model);
             
            }

            //Make sure product name is unique
              
                //check if its unique
                if(db.Products.Any(x=> x.Name == model.Name))
                {
                    //if there's a match
                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    ModelState.AddModelError("", "That product name is taken");
                    return View(model);
                }
                  
            //Declare Product id
            int id;

            //Init and save productDTO
            Product product = new Product();

            product.Name = model.Name;
            product.Slug = model.Name.Replace(" ", "-").ToLower();
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId;

            //Also need to get categories name  using the categoires DTO

            Category catDTO = db.Categories.FirstOrDefault(x => x.Id == model.CategoryId); //for categories selectList
            product.CategoryName = catDTO.Name;

            //add dto
            db.Products.Add(product);
            db.SaveChanges();

            //get id of primary key of row just inserted
            id = product.Id;


            //Set TempData message
            TempData["SM"] = "You have added a product!";

            #region Upload Image

            //create necessary directories
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));


            var pathString1 = Path.Combine(originalDirectory.ToString(), "Products"); //another folder
            var pathString2 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() ); // creates another folder
            var pathString3 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Thubs" ); //creates another folder for thubs
            var pathString4 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery"); //creates another folder for gallery images
            var pathString5 = Path.Combine(originalDirectory.ToString(), "Products\\" + id.ToString() + "\\Gallery\\Thubs"); //creats another folder for gallery images

            if (!Directory.Exists(pathString1))
                Directory.CreateDirectory(pathString1);

            if (!Directory.Exists(pathString2))
                Directory.CreateDirectory(pathString2);

            if (!Directory.Exists(pathString3))
                Directory.CreateDirectory(pathString3);

            if (!Directory.Exists(pathString4))
                Directory.CreateDirectory(pathString4);

            if (!Directory.Exists(pathString5))
                Directory.CreateDirectory(pathString5);

       
            //Check if file was uploaded

            if(file != null && file.ContentLength > 0)
            {
                //Get file extensions
                string ext = file.ContentType.ToLower();
                //Verify extension
                if( ext != "image/jpg" &&
                    ext != "image/jpeg" && 
                    ext != "image/pjeg" && 
                    ext != "image/x-png" &&
                    ext != "image/png")
                {

                    model.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
                    ModelState.AddModelError("", "The image was not uploaded -wrong image extension");
                    return View(model);
                }

                //Intitilase image name
                string imageName = file.FileName;

                //Save image name to DTO
                Product dto = db.Products.Find(id);
                dto.ImageName = imageName;
                db.SaveChanges();

                //Set original and thub image paths#
                var path = string.Format("{0}\\{1}", pathString2, imageName);
                var path2 = string.Format("{0}\\{1}", pathString3, imageName);

                //save original image
                file.SaveAs(path);
                //create and save thub
                WebImage img = new WebImage(file.InputStream);
                img.Resize(200, 200);
                img.Save(path2);

            }

            #endregion

            //Redirect



            return RedirectToAction("AddProduct");
        }
    }
}