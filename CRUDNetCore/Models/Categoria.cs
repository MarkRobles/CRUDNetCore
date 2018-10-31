using System;
using System.Collections.Generic;

namespace CRUDNetCore.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            SubCategoria = new HashSet<SubCategoria>();
        }

        public Guid UnqGencategoriaKey { get; set; }
        public string VchCodigo { get; set; }
        public string VchDescripcion { get; set; }

        public ICollection<SubCategoria> SubCategoria { get; set; }
    }
}
