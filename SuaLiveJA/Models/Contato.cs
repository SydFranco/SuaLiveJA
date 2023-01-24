using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuaLiveJA.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [MaxLength(150)]
        public string Endereco { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        public int Telefone { get; set; }

        public Contato()
        {

        }

        public Contato(string endereco, string email, int telefone)
        {
            Endereco = endereco;
            Email = email;
            Telefone = telefone;
        }
    }
}
