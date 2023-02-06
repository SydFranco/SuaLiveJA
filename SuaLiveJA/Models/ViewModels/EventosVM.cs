using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuaLiveJA.Models.ViewModels
{
    public class EventosVM
    {
       public Secao Secao { get; set; }
       public List<SelectListItem>? SecoesSelect { get; set; }

        public EventosVM()
        {

        }
    }
}
