using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Ecommerce.Models
{
    public class Product 
    {
        [DisplayName("Id")]
        public int ProductID { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        [DisplayName("Estoque")]
        public int Stock { get; set; }
        [DisplayName("Categoria")]
        public string Category { get; set; }
        [DisplayName("Está ativo?")]
        public bool IsActive { get; set; }

    }
}
