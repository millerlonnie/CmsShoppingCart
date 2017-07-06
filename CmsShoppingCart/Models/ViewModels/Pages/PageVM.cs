using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Models.ViewModels.Pages
{
    public class PageVM
    {
        //create constructor

        public PageVM()
        {
        }

        public PageVM(Page row) //taking the page DTObject and giivng it a paprameter
        {
            Id = row.Id; //initialse properties to vm
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;


        }

        //The view model must have the same properties as DTO, we can add data anataions e.g reqired for a page

        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Title { get; set; }

        public string Slug { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [AllowHtml]
        public string Body { get; set; }

        public int Sorting { get; set; }

        public bool HasSidebar { get; set; }

         

    }
}