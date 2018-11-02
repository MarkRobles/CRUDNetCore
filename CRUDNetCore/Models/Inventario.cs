using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDNetCore.Models
{
    public partial class Inventario
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe un SKU")]
        [StringLength(5, ErrorMessage = "La longitud del SKU debe ser de 5 caracteres ")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        public string Sku { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe un número de serie")]
        [MinLength(2, ErrorMessage = "El tamaño minimo para un número de serie es de 2 caracteres")]
        [MaxLength(40, ErrorMessage = "El tamaño maximo para un número de serie es de 40 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        public string NumeroSerie { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe una cantidad")]
        [Range(0, int.MaxValue, ErrorMessage = "Solo puedes escribir números")]
        public int Cantidad { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Elige un Producto")]
        public Guid Idproducto { get; set; }

        public Producto IdproductoNavigation { get; set; }
    }
}
