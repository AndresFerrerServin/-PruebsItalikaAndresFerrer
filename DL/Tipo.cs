using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Tipo
    {
        public Tipo()
        {
            Motocicleta = new HashSet<Motocicletum>();
        }

        public int IdTipo { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Motocicletum> Motocicleta { get; set; }
    }
}
