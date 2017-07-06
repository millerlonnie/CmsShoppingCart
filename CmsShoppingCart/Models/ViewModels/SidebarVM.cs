using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Models.ViewModels
{
    public class SidebarVM
    {
        //constr
      public   SidebarVM()
        {
        }

        //
       public SidebarVM(Sidebar row)
        {
            Id = row.Id;
            Body = row.Body;

        }


        public int Id { get; set; }
        [AllowHtml] //helps to add HTML to sidebar
        public string Body { get; set; }

    }
}