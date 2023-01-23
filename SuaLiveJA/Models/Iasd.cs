using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuaLiveJA.Models
{
    [Table("Iasd")]
    public class Iasd
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [MaxLength(50)]
        [Column("Nome", TypeName = "varchar (50)")]
        public string Nome { get; set; }
        public Contato Contato { get; set; }

        public Iasd()
        {

        }

        public Iasd(string nome, Contato contato)
        {
            Nome = nome;
            Contato = contato;
        }
    }
}
