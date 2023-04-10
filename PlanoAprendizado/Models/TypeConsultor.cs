using System.ComponentModel;

namespace PlanoAprendizado.Models
{
    public class TypeConsultor
    {
        public int Id { get; set; }
        [DisplayName("Tipo:")]
        public string Name { get; set; }
        [DisplayName("Tarifa:")]
        public float Fee { get; set; }
    }
}
