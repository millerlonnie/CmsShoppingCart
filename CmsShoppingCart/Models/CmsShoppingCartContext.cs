using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models
{
    public class CmsShoppingCartContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public CmsShoppingCartContext() : base("name=CmsShoppingCartContext")
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Sidebar> Sidebars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRoleDTO> UserRoles { get; set; }





        //public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.Pages.PageVM> PageVMs { get; set; }

        //public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.SidebarVM> SidebarVMs { get; set; }

        //public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.Shop.CategoryVM> CategoryVMs { get; set; }

        //public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.Shop.ProductVM> ProductVMs { get; set; }

        //public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.Shop.ProductVM> ProductVMs { get; set; }


        //  public System.Data.Entity.DbSet<CmsShoppingCart.Models.ViewModels.Pages.PageVM> PageVMs { get; set; }
        //  public System.Data.Entity.DbSet<CmsShoppingCart.Models.DTO.Product> Products { get; set; }
    }
}
