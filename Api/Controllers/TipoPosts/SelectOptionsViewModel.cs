using Infraestrutura.ViewModels;

namespace Api.Controllers.TipoPost;

public class SelectOptionsViewModel : BaseViewModel
{
    public string Label { get; set; }
    public Guid Value { get; set; }
}
