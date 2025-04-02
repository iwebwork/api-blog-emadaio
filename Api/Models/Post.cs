using Infraestrutura.Models;

namespace Api.Models;

public class Post : BaseModel
{
    public enum ETipo : short
    {
        noticias = 1,
        reviews = 2
    }

    public enum ELiberado : short
    {
        Sim = 1,
        Nao = 2
    }


    public string Name { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public string? Image { get; private set; }
    public ETipo Tipo { get; private set; }
    public string Corpo { get; private set; }
    public ELiberado Liberado { get; private set; }

    public Post(Guid id, string name, string title, DateTime date, string? image, ETipo tipo, string corpo, ELiberado liberado) : base(id)
    {
        Id = id;
        Name = name;
        Title = title;
        Date = date;
        Image = image;
        Tipo = tipo;
        Corpo = corpo;
        Liberado = liberado;
    }

    public Post(string name, string title, DateTime date, string? image, ETipo tipo, string corpo, ELiberado liberado) : base()
    {
        Name = name;
        Title = title;
        Date = date;
        Image = image;
        Tipo = tipo;
        Corpo = corpo;
        Liberado = liberado;
    }

    public void LiberarPost()
    {
        Liberado = ELiberado.Sim;
    }
}
