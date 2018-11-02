using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDNetCore.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            SubCategoria = new HashSet<SubCategoria>();
        }

        public Guid Id { get; set; }
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe un código")]
        [StringLength(5, ErrorMessage = "La longitud del código debe ser de 5 caracteres ")]
        public string Codigo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Escribe una descripción")]
        [DataType(DataType.Text, ErrorMessage = "Debes escribir texto")]
        [MinLength(2, ErrorMessage = "El tamaño minimo para una descripcion es de 2 caracteres")]
        [MaxLength(40, ErrorMessage = "El tamaño maximo para una descripción es de 40 caracteres")]
        public string Descripcion { get; set; }

        public ICollection<SubCategoria> SubCategoria { get; set; }
    }
}
