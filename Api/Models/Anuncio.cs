using Infraestrutura.Models;

namespace Api.Models;

public class Anuncio(string name, Anuncio.ETipo tipo, string corpo, Anuncio.ELiberado liberado) : BaseModel()
{
    public enum ETipo
    {
        Link = 1,
        Banner = 2
    }

    public enum ELiberado : short
    {
        Sim = 1,
        Nao = 2
    }

    public string Name { get; private set; } = name;
    public ETipo Tipo { get; private set; } = tipo;
    public string Corpo { get; private set; } = corpo;
    public ELiberado Liberado { get; private set; } = liberado;

    public void Update(string name, ETipo tipo, string corpo, ELiberado liberado)
    {
        base.Update();

        Name = name;
        Tipo = tipo;
        Corpo = corpo;
        Liberado = liberado;
    }
}
