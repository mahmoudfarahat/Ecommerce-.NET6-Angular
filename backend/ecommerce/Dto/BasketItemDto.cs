﻿using System.ComponentModel.DataAnnotations;

namespace ecommerce.Dto
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue, ErrorMessage ="Price Must be greater than Zero")]

        public decimal Price { get; set; }
        [Required]
        

        public string PictureUrl { get; set; }
        [Required]

        public string Type { get; set; }
        [Required]

        public int Quantity { get; set; }
        [Required]

        public string Brand { get; set; }
    }
}