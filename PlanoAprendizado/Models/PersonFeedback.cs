using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanoAprendizado.Models
{
    public class PersonFeedback
    {
        [Key]
        public int Id { get; set; }
        public TypePerson Type { get; set; }
       
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int FeedbackId { get; set; }
        public Feedback Feedbacks { get; set; }
    }
}
