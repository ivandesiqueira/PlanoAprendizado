using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanoAprendizado.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Data:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }
        [DisplayName("Turma/Círculo:")]
        public int CircleId { get; set; }
        [DisplayName("Turma/Círculo:")]
        public Circle Circle { get; set; }  
        [DisplayName("Tema:")]
        public int ThemeId { get; set; }
        [DisplayName("Tema:")]
        public Theme Theme { get; set; }
        [DisplayName("Oportunidade de Aprendizado:")]
        public string OportunityLearning { get; set; } = string.Empty;
        [DisplayName("Nota:")]
        public float Note { get; set; } 
        [DisplayName("Comentário:")]
        public string Comment { get; set; }

        public List<PersonFeedback> PeopleFeedbacks { get; set; }

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
}
