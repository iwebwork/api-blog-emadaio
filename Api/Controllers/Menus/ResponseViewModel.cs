using Infraestrutura.ViewModels;
using static Api.Models.Menu;

namespace Api.Controllers.Menu;

public class ResponseViewModel : BaseViewModel
{
    public string Label { get; set; }
    public Guid TipoPostId { get; set; }
    public string Url { get; set; }
    public string Path { get; set; }
    public ELiberado Liberado { get; set; }
    public EIndex Index { get; set; }
}
