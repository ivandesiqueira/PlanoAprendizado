using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PlanoAprendizado.Models
{
    public class ActualState
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Círculo/Turma:")]
        public int CircleId { get; set; }
        [DisplayName("Círculo/Turma:")]
        public Circle Circle { get; set; }
        [DisplayName("Projeto:")]
        public int ProjectId { get; set; }
        [DisplayName("Projeto:")]
        public Project Project { get; set; }
        [DisplayName("Tipo:")]
        public TypePlan TypePlan { get; set; }
        [DisplayName("Consultor Nível:")]
        public int TypeConsultorId { get; set; }
        [DisplayName("Consultor Nível:")]
        public TypeConsultor TypeConsultor { get; set; }
        [DisplayName("Descrição:")]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Valor:")]
        public float Value { get; set; }
        [DisplayName("Consultor:")]
        public int PersonId { get; set; }
        [DisplayName("Consultor:")]
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        [DisplayName("Tempo Planejado:")]
        public float TimePlanned { get; set; }
        [DisplayName("Tempo Utilizado:")]
        public float RealTime { get; set; }
        [DisplayName("Entregue:")]
        public bool Delivered { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Valor Final:")]
        public float FinalValue { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        [DisplayName("Produtividade:")]
        public float Productivity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Entrega:")]
        public DateTime Sprint { get; set; }

        public List<DayTime> DayTimes { get; set; }

        public void AttCalculos()


        {
            if (this.DayTimes != null && this.DayTimes.Count > 0)
            {
                this.RealTime = this.DayTimes.Sum(x => x.Hours);
                this.Delivered = this.DayTimes.FirstOrDefault(x => x.Delivered == true) != null;

            }
            if (this.Project.TypeTime == TypeTime.PrecoFechado)
            {
                float tarifa = this.Project.Value / this.Project.Duration;
                this.Value = this.TimePlanned * tarifa;
            }
            else
            {
                this.Value = this.TypeConsultor.Fee * this.TimePlanned;
            }
            if (this.Delivered)
            {
                this.Productivity = this.TimePlanned / this.RealTime;
                if (double.IsInfinity(this.Productivity))
                    this.Productivity = 0;

                if(this.Project.TypeTime == TypeTime.PrecoFechado)
                {
                    float tarifa = this.Project.Value / this.Project.Duration;
                    this.FinalValue = this.TimePlanned * tarifa;
                }
                else
                {
                    this.FinalValue = this.TypeConsultor.Fee * this.RealTime;
                }
            }
            
        }

    }
    public enum TypePlan
    {
        GESTAO,
        ETL,//Extração de dados
        DASH,
        BBP,//Bussiness blueprint
        AULA,
        PREPAULA,//Preparação de aula
        DEV,
        MANUT//Manutenção
    }
}
