using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDNetCore.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Inventario = new HashSet<Inventario>();
        }

        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe una descripción")]
        [MinLength(2, ErrorMessage = "El tamaño minimo para una descripcion es de 2 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [MaxLength(40, ErrorMessage = "El tamaño maximo para una descripción es de 40 caracteres")]
        public string Descripcion { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe un código")]
        [StringLength(5, ErrorMessage = "La longitud del código debe ser de 5 caracteres ")]
        public string Codigo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Elige una subCategoria")]
        public Guid IdsubCategoria { get; set; }

        public SubCategoria IdsubCategoriaNavigation { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
    }
}
