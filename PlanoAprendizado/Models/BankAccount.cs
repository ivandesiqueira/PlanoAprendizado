using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PlanoAprendizado.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Banco:")]
        public string Name { get; set; }
        [DisplayName("Empresa:")]
        public int EnterpriseId { get; set; }
        [DisplayName("Empresa:")]
        public Enterprise Enterprise { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Saldo Atual:")]
        public float Balance { get; set; }
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        [DisplayName("Saldo Inicial:")]
        public float InitialBalance { get; set; }
        public List<Expense> Expenses { get; set; }

        public void FinancialCalc()
        {
            this.Balance =
                this.InitialBalance +
                this.Expenses.Where(x => x.Type).Sum(x => x.Value) -
                this.Expenses.Where(x => !x.Type).Sum(x => x.Value);
        }
    }
}
