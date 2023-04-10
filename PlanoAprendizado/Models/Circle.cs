using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class Circle
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome:")]
        public string Name { get; set; } 
        
        public List<Person>Person { get; set; } 
        public List<Learning>Learnings { get; set; }
    }
}
