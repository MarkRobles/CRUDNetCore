using System;
using System.Collections.Generic;

namespace CRUDNetCore.Models
{
    public partial class Inventario
    {
        public Guid UnqInvinventarioKey { get; set; }
        public string VchSku { get; set; }
        public string VchNumeroSerie { get; set; }
        public int IntCantidad { get; set; }
        public Guid UnqGenproductoLink { get; set; }

        public Producto UnqGenproductoLinkNavigation { get; set; }
    }
}
