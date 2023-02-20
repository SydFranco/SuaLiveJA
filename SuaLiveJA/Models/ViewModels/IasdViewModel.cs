using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuaLiveJA.Models.ViewModels
{
    public class IasdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Contato Contato { get; set; }
        public List<SelectListItem> ContatosSelect { get; set; }
        public IasdViewModel()
        {

        }
    }
}
