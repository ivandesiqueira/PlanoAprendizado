using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanoAprendizado.Models
{
    public class PersonLearn
    {
        [Key]
        public int Id { get; set; }
        public TypePerson Type { get; set; }
       
        public int PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
        public int LearningId { get; set; }
        public Learning Learning { get; set; }
        

    }
}
