using Infraestrutura.ViewModels;

namespace Api.Controllers.Usuarios;

public class RequestViewModel : BaseViewModel
{
    public string? UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
