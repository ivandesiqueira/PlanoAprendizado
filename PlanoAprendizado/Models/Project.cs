using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace PlanoAprendizado.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Projeto:")]
        public string Name { get; set; }
        [DisplayName("Tipo de Contrato:")]
        public TypeTime TypeTime { get; set; }
        [DisplayName("Descrição:")]
        public string Description { get; set; }
        [DisplayName("Empresa:")]
        public string Enterprise { get; set; }
        [DisplayName("Horas Contratadas:")]
        public float Duration { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Valor:")]
        public float Value { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Início:")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Finalização:")]
        public DateTime EndDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Inserção:")]
        public DateTime InsertDate { get; set; }
        [DisplayName("Situação:")]
        public bool Status { get; set; }

        public List<ActualState> ActualStates { get; set; }
        public List<DayTime> DayTimes { get; set; }
    }

    //Se for "PrecoFechado" entra horas e valor
    //Tarifa = Value / Duraction;

    //Se for "Horas" entra qual tarifa
    //Tarifa * Horas
    public enum TypeTime
    {
        Horas,
        PrecoFechado
    }
   
}
