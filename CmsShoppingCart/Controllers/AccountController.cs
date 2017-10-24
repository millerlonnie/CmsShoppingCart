using CmsShoppingCart.Models;
using CmsShoppingCart.Models.Data;
using CmsShoppingCart.Models.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CmsShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        CmsShoppingCartContext db = new CmsShoppingCartContext();
        // GET: Account

        public ActionResult Index()
        {
            return Redirect("~/account/login"); //redicrect to action 
        }

        // Post: Account
        

        //Get: /account/login
        [HttpGet]
        public ActionResult Login()
        {
            //confirm user is not loged in

            string username = User.Identity.Name; //part of MVC, name property accessed via user identity
            if (!string.IsNullOrEmpty(username))//when user is logged in, 
                return RedirectToAction("user-profile"); //redirect to user profile

            //Return view
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {

            //check model state
            if(! ModelState.IsValid)
            {
                return View(model);
            }

            //check if the user is valid
            bool isValid = false;

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {

                if (db.Users.Any(x=> x.UserName.Equals(model.Username)&& x.Password.Equals(model.Password)))
                {
                    isValid = true;
                }

                if(! isValid) //if not valid 
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View(model);
                }
                else
                {
                    //seting a cookie or session for a user
                    FormsAuthentication.SetAuthCookie(model.Username, model.RemberME);
                    return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RemberME));

                }
            }
                return View();
        }

        //Get: /account/create-account
        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        //Post: /account/create-account
        [HttpPost]
        [ActionName("create-account")]
        public ActionResult CreateAccount(UserVM model)
        {
             //check model state
             if(! ModelState.IsValid)
            {
                return View("createAccount", model);
            }
             //check if passwords match
             if(! model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View("CreateAccount", model);
            }

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())

            {
                //make sure username is unique
                if (db.Users.Any(x => x.UserName.Equals(model.UserName))) //if theres a match its a problem
                {
                    ModelState.AddModelError("", "Username" + model.UserName + "is taken");
                    model.UserName = "";
                    return View("CreateAccount", model);
                }

                //create user DTO
                UserDTO userDTO = new UserDTO()
                {
                    //initialise its fileds
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.EmailAddress,
                    UserName = model.UserName,
                    Password = model.Password


                };

                //Add the DTO                          
                db.Users.Add(userDTO);
                //Save 
                db.SaveChanges();

                //Add to userRolesDTO  >>>of 2 which is for user for anyone who signs in
                int id = userDTO.Id;

                UserRoleDTO userRolesDTO = new UserRoleDTO()
                {
                    UserId = id,
                    RoleId = 2

                };

                db.UserRoles.Add(userRolesDTO);
              
            }
            //Crate a TempData message
            TempData["SM"] = "You are now registered and can login";

            //Redirect
            return Redirect("~/account/login");

            }

        //GET: /account/logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/account/login");
        }

        //despaying the username for the logeedin user
        public ActionResult UserNavPartial()
        {

            //Get username
            string username = User.Identity.Name;

            //Declare model
            UserNavPartailVM model;
         
                //Get the user
                UserDTO dto = db.Users.FirstOrDefault(x => x.UserName == username);

                //intiliase / Build the model
                model = new UserNavPartailVM()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };

            //Return view with model

            return PartialView(model);
        }

        //GET: /account/user-profile
        [HttpGet]
        [ActionName("user-profile")]
        public ActionResult UserProfile()
        {
            //Get username
            string username = User.Identity.Name;

            //Declare model
            UserProfileVM model;

            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                //Get user
                UserDTO dto = db.Users.FirstOrDefault(x => x.UserName == username);

                //Build model
                model = new UserProfileVM(dto);
            }

            //Return view with model

            return View("UserProfile", model); //specify viewname with profile. 
        }

        //Post: /account/user-profile
        [HttpPost]
        [ActionName("user-profile")]
        public ActionResult UserProfile(UserProfileVM model)
        {
            //check model state
            if(! ModelState.IsValid)
            {
                return View("UserProfile", model); //being specific of paostback method
            }

            //check if passwords match if need be
            if(!string.IsNullOrWhiteSpace(model.Password))
            {
                if(!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match");
                    return View("UserProfile", model);
                }
            }


            using (CmsShoppingCartContext db = new CmsShoppingCartContext())
            {
                //Get username
                string username = User.Identity.Name;
                //Make sure username is unique
                if(db.Users.Where(x => x.Id != model.Id).Any(x=> x.UserName == username))
                {
                    //if theres a match we have a problem
                    ModelState.AddModelError("","Username" + model.UserName + "already exists");
                    model.UserName = ""; //reset username
                    return View("UserProfile", model);

                }

                //Edit DTO
                UserDTO dto = db.Users.Find(model.Id);

                dto.FirstName = model.FirstName;
                dto.UserName = model.LastName;
                dto.EmailAddress = model.EmailAddress;
                dto.UserName = model.UserName;

                if(!string.IsNullOrWhiteSpace(model.Password)) //if passowrd is not temp edeit otherwiserse do nothing
                {
                    //if it is
                    dto.Password = model.Password;
                }

                //Save
                db.SaveChanges();
            }

            //Set TempData message
            TempData["SM"] = "You have edited your profile";

            //Redirect

             return Redirect("~/account/user-profile"); /////

             
        }


        }
}