using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class StatusCurrent
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Projeto:")]
        public string Project { get; set; }
        [DisplayName("Tipo Objeto:")]
        public TypePlan Type { get; set; }
        [DisplayName("Descrição:")]
        public string Description { get; set; } = string.Empty;
        [DisplayName("Horas Planejadas:")]
        public int TimePlanned { get; set; }
        [DisplayName("Valor:")]
        public int Value { get; set; }
        [DisplayName("Pessoa:")]
        public int PersonId { get; set; }
        [DisplayName("Pessoa:")]
        public  Person Person { get; set; }
        [DisplayName("Horas Reais:")]
        public int RealTime { get; set; }
        [DisplayName("Horas Fim:")]
        public int FinishTime { get; set; }
        [DisplayName("Faturado:")]
        public bool Profit { get; set; }
        [DisplayName("Percentual Entregue:")]
        public float DeliveryPer { get; set; }
        [DisplayName("Produtividade:")]
        public float Productivity { get; set; }
    }
}
