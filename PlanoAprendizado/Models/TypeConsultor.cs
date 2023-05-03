using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class TypeConsultor
    {
        public int Id { get; set; }
        [DisplayName("Tipo:")]
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Tarifa:")]
        public float Fee { get; set; }
    }
}
