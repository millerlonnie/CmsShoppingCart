﻿using CmsShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShoppingCart.Models.ViewModels.Shop
{
    public class ProductVM
    {
        //constrcutor
        public ProductVM()
        {

        }

        public ProductVM(Product row)
        {
            Id = row.Id;
            Name = row.Name;
            Slug = row.Slug;
            Description = row.Description;
            Price = row.Price;
            CategoryName = row.CategoryName;
            CategoryId = row.CategoryId;
            ImageName = row.ImageName;
 
       
        }


        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; } //add selectlist of categories
        [Required]
        public int CategoryId { get; set; }
        public string ImageName { get; set; } //add an IEnumerable for images gallaery

        //add scelectlist item for choosing type of categoryName
        public IEnumerable <SelectListItem>  Categories{ get; set; }
        public IEnumerable <string> GalleryImages { get; set; }



    }
}