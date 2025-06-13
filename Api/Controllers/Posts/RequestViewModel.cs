using Api.Models;
using Infraestrutura.ViewModels;
using static Api.Models.Post;

namespace Api.Controllers.Posts;

public class RequestViewModel : BaseViewModel
{
    public string Name { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string? Image { get; set; }
    public Guid TipoPostId { get; set; }
    public string Corpo { get; set; }
    public ELiberado Liberado { get; set; } = Post.ELiberado.Sim;
}
