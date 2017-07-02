using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models.Data
{
    public class Sidebar
    {
        [Key]
        public int Id { get; set; }

        public string Body { get; set; }
    }
}