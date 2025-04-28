﻿using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class BasketItemDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        [Range(0,Double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1, 99)]
        public int Quantity { get; set; }

    }
}