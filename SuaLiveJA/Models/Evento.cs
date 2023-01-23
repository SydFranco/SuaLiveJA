using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuaLiveJA.Models
{
    [Table("Evento")]
    public class Evento
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [MaxLength(200)]
        [Column("Descricao", TypeName = "nvarchar (200)")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        [Timestamp]
        public DateTime Data_Hora { get; set; }
        [Required(ErrorMessage = "este campo é obrigatório")]
        public string Link_URL { get; set; }
        [Column("Post", TypeName = "varchar (50)")]
        public string post { get; set; }
        public Secao  Secao { get; set; }

        public Evento()
        {

        }

        public Evento(string descricao, DateTime data_Hora, string link_URL, string post, Secao secao)
        {
            Descricao = descricao;
            Data_Hora = data_Hora;
            Link_URL = link_URL;
            this.post = post;
            Secao = secao;
        }
    }
}
