using Infraestrutura.Models;

namespace Api.Models;

public class Post(string name, string title, DateTime date, string? image, Post.ETipo tipo, string corpo, Post.ELiberado liberado) : BaseModel()
{
    public enum ETipo : short
    {
        Noticias = 1,
        Reviews = 2,
        Curiosidades = 3
    }

    public enum ELiberado : short
    {
        Sim = 1,
        Nao = 2
    }

    public string Name { get; private set; } = name;
    public string Title { get; private set; } = title;
    public DateTime Date { get; private set; } = date;
    public string? Image { get; private set; } = image;
    public ETipo Tipo { get; private set; } = tipo;
    public string Corpo { get; private set; } = corpo;
    public ELiberado Liberado { get; private set; } = liberado;

    public void Update(string name, string title, DateTime date, string? image, ETipo tipo, string corpo, ELiberado liberado)
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
