using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuaLiveJA.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150, MinimumLength = 5,
        ErrorMessage = " O nome deve ter no minimo 5 caracteres e um máximo de 50 caracteres ")]
        [Column("Nome", TypeName = "varchar (50)")]
        public string Nome { get; set; }
        public Contato  Contato { get; set; }
        public Iasd  Iasd { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [StringLength(50, MinimumLength = 5,
         ErrorMessage = " O nome deve ter no minimo 5 caracteres e um máximo de 50 caracteres ")]
        [Column("Login", TypeName = "varchar (50)")]
        public string Login { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [Column("Senha", TypeName = "varchar (30)")]
        public string Senha { get; set; }
        [Column("IsAdmim", TypeName = "varchar (30)")]
        public bool IsAdmim { get; set; }
        public List<Evento> Eventos { get; set; }

        public Usuario()
        {

        }

        public Usuario(string nome, Contato contato, Iasd iasd, string login, string senha, bool isAdmim, List<Evento> eventos)
        {
            Nome = nome;
            Contato = contato;
            Iasd = iasd;
            Login = login;
            Senha = senha;
            IsAdmim = isAdmim;
            Eventos = eventos;
        }
    }
}
