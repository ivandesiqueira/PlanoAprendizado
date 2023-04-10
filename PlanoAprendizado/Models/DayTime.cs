using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace PlanoAprendizado.Models
{
    public class DayTime
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Atividade:")]
        public int ActualStateId { get; set; }
        [DisplayName("Atividade:")]
        public ActualState ActualState { get; set; }    
      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Realizada:")]
        public DateTime Date { get; set; }
        [DisplayName("Horas:")]
        public float Hours { get; set; }
        [DisplayName("Entregue:")]
        public bool Delivered { get; set; }   

    }

   
}

