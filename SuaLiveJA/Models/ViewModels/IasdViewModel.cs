using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuaLiveJA.Models.ViewModels
{
    public class IasdViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public int Telefone { get; set; }
        public IasdViewModel()
        {

        }
    }
}
