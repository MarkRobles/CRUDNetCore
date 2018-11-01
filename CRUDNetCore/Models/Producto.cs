using System;
using System.Collections.Generic;

namespace CRUDNetCore.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Inventario = new HashSet<Inventario>();
        }

        public Guid UnqGenproductoKey { get; set; }
        public string VchDescripcion { get; set; }
        public string VchCodigo { get; set; }
        public Guid UnqGensubCategoriaLink { get; set; }

        public SubCategoria UnqGensubCategoriaLinkNavigation { get; set; }
        public ICollection<Inventario> Inventario { get; set; }
    }
}
