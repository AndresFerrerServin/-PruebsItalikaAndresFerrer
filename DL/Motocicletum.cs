using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Motocicletum
    {
        public int IdMotocicleta { get; set; }
        public string? Sku { get; set; }
        public string? Fert { get; set; }
        public string? Modelo { get; set; }
        public int? IdTipo { get; set; }
        public string? NúmeroDeSerie { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Imagen { get; set; }

        public virtual Tipo? IdTipoNavigation { get; set; }

        public string? Nombre { get; set; }
    }
}
