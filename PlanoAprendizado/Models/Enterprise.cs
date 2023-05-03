using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Nome:")]
        public string Name { get; set; }
        [DisplayName("CNPJ:")]
        public string Cnpj { get; set; }
        [DisplayName("Telefone:")]
        public int Phone { get; set; }
        [DisplayName("Email:")]
        public string Email { get; set; }
        public List<BankAccount>BankAccounts { get; set; }


    }
}
