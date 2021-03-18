using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.Serviceklassen.Core
{
    // ENUMS
    public enum PackingUnits { Piece, Dozen, Gross }

    public class Product
    {
        // CONSTS
        private const decimal ExchangeRate = 1.19M;
        // PRIVATE FIELDS
        private int stock;
        private string code;
        private decimal priceEuro;
        // PROPS
        public string Code
        {
            get { return code; }
            set 
            {
                if (value == "")
                    value = Guid.NewGuid().ToString();
                code = value.ToUpper(); 
            }
        }
        public string Description { get; set; }
        public PackingUnits Packing { get; set; }
        public int Stock
        {
            get { return stock; }
            set 
            {
                if (value < 0)
                    value = 0;
                stock = value; 
            }
        }
        public decimal PriceEuro
        {
            get { return priceEuro; }
            set {
                if (value < 0) value = 0;
                priceEuro = value; 
            }
        }
        public decimal PriceDollar
        {
            get
            {
                return priceEuro * ExchangeRate;
            }
        }
        public decimal StockValueEuro
        { 
            get
            {
                return stock * priceEuro;
            }
        }
        public decimal StockValueDollar
        {
            get
            {
                return stock * priceEuro * ExchangeRate;
            }
        }

        // CONSTRUCTS
        public Product()
        { }
        public Product(string code, string description, PackingUnits packing, int stock, decimal priceEuro)
        {
            Code = code;
            Description = description;
            Packing = packing;
            Stock = stock;
            PriceEuro = priceEuro;
        }
        // OVERRIDE TOSTRING
        public override string ToString()
        {
            return $"{Code} - {Description}";
        }

    }
}
