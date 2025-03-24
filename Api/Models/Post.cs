using Infraestrutura.Models;
using static Api.Models.Post;

namespace Api.Models;

public class Post(string name, string title, DateTime date, string image, ETipo tipo, string corpo, bool liberado) : BaseModel
{
    public enum ETipo : short
    {
        Noticias = 1,
        Reviews = 2
    }

    public string Name { get; private set; } = name;
    public string Title { get; private set; } = title;
    public DateTime Date { get; private set; } = date;
    public string Image { get; private set; } = image;
    public ETipo Tipo { get; private set; } = tipo;
    public string Corpo { get; private set; } = corpo;
    public bool Liberado { get; private set; } = liberado;
}
