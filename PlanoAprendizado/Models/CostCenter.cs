using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class CostCenter
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Centro de Custo:")]
        public string Name { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
