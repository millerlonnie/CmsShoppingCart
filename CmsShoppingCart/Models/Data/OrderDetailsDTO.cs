using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CmsShoppingCart.Models.Data
{
    [Table("tblOrderDetails")]
    public class OrderDetailsDTO
    {
        [Key]
        public int Id { get; set; }     //primary key
        public int OrderId { get; set; } //foreign key
        public int UserId { get; set; }  //foreign key
        public int ProductId { get; set; } //foreign key
        public int Quantity { get; set; }  //foreign key

        [ForeignKey("OrderId")]
        public virtual OrderDTO Orders { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDTO Users { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
         


    }
}