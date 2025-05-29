using Infraestrutura.ViewModels;
using static Api.Models.Anuncio;

namespace Api.Controllers.Anuncios;

public class ResponseViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Corpo { get; set; }
    public ETipo Tipo { get; set; }
    public string TipoNome { get; set; }
    public ELiberado Liberado { get; set; }
    public string LiberadoNome { get; set; }
}
