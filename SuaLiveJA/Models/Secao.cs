using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuaLiveJA.Models
{
    [Table("Secao")]
    public class Secao
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [MaxLength(200)]
        public string Descricao { get; set; }

        public Secao()
        {

        }

        public Secao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
