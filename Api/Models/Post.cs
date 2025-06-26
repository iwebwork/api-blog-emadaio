using Infraestrutura.Models;

namespace Api.Models;

public class Post(string name, string title, DateTime date, string? image, Guid tipoPostId, string corpo, Post.ELiberado liberado) : BaseModel()
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
    public Guid TipoPostId { get; private set; } = tipoPostId;
    public virtual TipoPost TipoPost { get; private set; }
    public string Corpo { get; private set; } = corpo;
    public ELiberado Liberado { get; private set; } = liberado;

    public void Update(string name, string title, DateTime date, string? image, Guid tipoPostId, string corpo, ELiberado liberado)
    {
        base.Update();

        Name = name;
        Title = title;
        Date = date;
        Image = image;
        TipoPostId = tipoPostId;
        Corpo = corpo;
        Liberado = liberado;
    }

    public void LiberarPost()
    {
        base.Update();
        Liberado = ELiberado.Sim;
    }
}
