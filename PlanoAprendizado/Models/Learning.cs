using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanoAprendizado.Models
{
    public class Learning
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Data:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Turma/Cícrulo:")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Cícrulo:")]
        public Circle Circle { get; set; }
        [DisplayName("Tema:")]
        public int ThemeId { get; set; }
        [DisplayName("Tema:")]
        public Theme Theme { get; set; }
        [DisplayName("Oportunidade de Aprendizado:")]
        public string OportunityLearning { get; set; } = string.Empty;
        [DisplayName("Ação de Aprendizado:")]
        public string LearningAction { get; set; } = string.Empty;
        [DisplayName("Data Mensuração:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime MeasurementDate { get; set; }
        [DisplayName("Forma de Mensuração:")]
        public float MeasurementForm { get; set; }
        [DisplayName("Resultado:")]
        public string Result { get; set; }
        [DisplayName("Comentários:")]
        public string Comment { get; set; }
        [DisplayName("Status:")]
        public Status Status { get; set; }

        public List<PersonLearn> PeopleLearns { get; set; }

        [NotMapped]
        [DisplayName("Mentorado:")]
        public int MentoradoId { get; set; }
        [NotMapped]
        public Person MentoradoPerson { get; set; }
        [NotMapped]
        [DisplayName("Mentor:")]
        public int MentorId { get; set; }
        [NotMapped]
        public Person MentorPerson { get; set; }

    }
    public enum Status
    {
        BRANCO,
        CONCLUIDO
    }
}
