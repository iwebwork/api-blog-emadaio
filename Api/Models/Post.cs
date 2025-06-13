using Infraestrutura.Models;

namespace Api.Models;

public class Post(string name, string title, DateTime date, string? image, TipoPost tipoPost, string corpo, Post.ELiberado liberado) : BaseModel()
{
    public enum ELiberado : short
    {
        Sim = 1,
        Nao = 2
    }

    public string Name { get; private set; } = name;
    public string Title { get; private set; } = title;
    public DateTime Date { get; private set; } = date;
    public string? Image { get; private set; } = image;
    public Guid TipoPostId { get; private set; } = tipoPost.Id;
    public virtual TipoPost TipoPost { get; private set; } = tipoPost;
    public string Corpo { get; private set; } = corpo;
    public ELiberado Liberado { get; private set; } = liberado;

    public void Update(string name, string title, DateTime date, string? image, TipoPost tipoPost, string corpo, ELiberado liberado)
    {
        base.Update();

        Name = name;
        Title = title;
        Date = date;
        Image = image;
        TipoPost = tipoPost;
        TipoPostId = tipoPost.Id;
        Corpo = corpo;
        Liberado = liberado;
    }

    public void LiberarPost()
    {
        base.Update();
        Liberado = ELiberado.Sim;
    }
}
