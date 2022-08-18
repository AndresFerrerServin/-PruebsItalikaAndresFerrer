namespace ML
{
    public class Motocicleta
    {
        public int IdMotocicleta { get; set; }
        public string SKU { get; set; }
        public string Fert { get; set; }
        public string Modelo { get; set; }
        public string NúmeroDeSerie { get; set; }
        public ML.Tipo Tipo  { get; set; }
        public string Fecha { get; set; }
        public string Imagen { get; set; }
        public List<object> Motocicletas { get; set; }

    }
}