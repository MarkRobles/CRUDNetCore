using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDNetCore.Models
{
    public partial class SubCategoria
    {
        public SubCategoria()
        {
            Producto = new HashSet<Producto>();
        }

        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Elige una Categoria")]
        public Guid Idcategoria { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe un código")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [StringLength(5, ErrorMessage = "La longitud del código debe ser de 5 caracteres ")]
        public string Codigo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe una descripción")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [MinLength(2, ErrorMessage = "El tamaño minimo para una descripcion es de 2 caracteres")]
        [MaxLength(40, ErrorMessage = "El tamaño maximo para una descripción es de 40 caracteres")]
        public string Descripcion { get; set; }

        public Categoria IdcategoriaNavigation { get; set; }
        public ICollection<Producto> Producto { get; set; }
    }
}
