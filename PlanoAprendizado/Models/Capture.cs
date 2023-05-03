using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class Capture
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Conta:")]
        public string Name { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
