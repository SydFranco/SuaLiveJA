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
        public string Name { get; set; }
        public Contato Contato { get; set; }

        public Iasd()
        {

        }

        public Iasd(string name, Contato contato)
        {
            Name = name;
            Contato = contato;
        }
    }
}
