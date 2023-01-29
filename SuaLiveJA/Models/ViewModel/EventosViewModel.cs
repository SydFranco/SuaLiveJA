using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuaLiveJA.Models.ViewModel
{
    public class EventosViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data_Hora { get; set; }
        public string Link_URL { get; set; }
        public string Post { get; set; }
        public Secao Secao { get; set; }

        public List<SelectListItem> SecoesSelect { get; set; }
    }
}
