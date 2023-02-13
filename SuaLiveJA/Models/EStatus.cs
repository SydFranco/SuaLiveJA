using System.ComponentModel;

namespace SuaLiveJA.Models
{
    public enum EStatus
    {
        [Description("Rascunho")]
        Rascunho,
        [Description("Em Aprovação")]
        EmAprovacao,
        [Description("Publicado")]
        Publicado
    }
}
