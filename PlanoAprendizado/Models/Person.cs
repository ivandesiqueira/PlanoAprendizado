using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PlanoAprendizado.Models
{
    public class Person : IdentityUser<int>
    {
        
        
        [DisplayName("Turma/Círculo:")]
        public int? CircleId { get; set; }
        [DisplayName("Turma/Círculo:")]
        public Circle Circle { get; set; }
        [DisplayName("Tipo:")]
        public TypePerson Type { get; set; }
        [DisplayName("Nome:")]
        public string Name { get; set; }
        [DisplayName("CPF:")]
        public string CPF { get; set; }
        [DisplayName("Email:")]
        public string Email { get; set; }
        [DisplayName("Telefone:")]
        public string Phone { get; set; }
        [DisplayName("Data Nascimento:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateBorn { get; set; }
        [DisplayName("Grau de Estudo:")]
        public NivelStudy NivelStudy { get; set; }
        [DisplayName("Universidade:")]
        public string University { get; set; }
        [DisplayName("Data de Formação:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime GraduateDate { get; set; }
        [DisplayName("Data Registro:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateRegister { get; set; }
        [DisplayName("Empresa:")]
        public string Enterprise { get; set; }
        [DisplayName("Indicação")]
        public bool Recommendation { get; set; }
        [DisplayName("Estudando")]
        public bool IsStudying { get; set; }

        public List<Learning> Learnings { get; set; }
        public List<Feedback>Feedbacks { get; set; }
        public List<ActualState> ActualStates { get; set; }
        public List<Expense> Expenses { get; set; }

    }

    public enum TypePerson
    {
        Mentorado,
        Mentor
    }

    public enum NivelStudy
    {
        EnsinoFundamentalIncompleto,
        EnsinoFundamentalCompleto,
        EnsinoMedioIncompleto,
        EnsinoMedioCompleto,
        EnsinoSuperiorIncompleto,
        EnsinoSuperiorCompleto
    }

}
