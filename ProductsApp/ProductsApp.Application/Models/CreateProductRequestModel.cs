﻿namespace ProductsApp.Application.Models
{
    public class CreateProductRequestModel
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int NumberOfPieces { get; set; }
    }
}
