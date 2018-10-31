using System;
using System.Collections.Generic;

namespace CRUDNetCore.Models
{
    public partial class SubCategoria
    {
        public SubCategoria()
        {
            Producto = new HashSet<Producto>();
        }

        public Guid UnqGensubCategoriaKey { get; set; }
        public Guid UnqGencategoriaLink { get; set; }
        public string VchCodigo { get; set; }
        public string VchDescripcion { get; set; }

        public Categoria UnqGencategoriaLinkNavigation { get; set; }
        public ICollection<Producto> Producto { get; set; }
    }
}
