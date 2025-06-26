using Infraestrutura.Models;

namespace Api.Models;

public class Menu(string label, Guid tipoPostId, string url, string path) : BaseModel()
{
    public enum ELiberado : short
    {
        Sim = 1,
        Nao = 2
    }

    public enum EIndex : short
    {
        Sim = 1,
        Nao = 2
    }

    public string Label { get; private set; } = label;
    public Guid TipoPostId { get; private set; } = tipoPostId;
    public virtual TipoPost TipoPost { get; private set; }
    public string Url { get; private set; } = url;
    public string Path { get; private set; } = path;
    public ELiberado Liberado { get; private set; } = ELiberado.Nao;
    public EIndex Index { get; private set; } = EIndex.Nao;

    public void Update(string label, TipoPost tipoPost, string url, string path)
    {
        base.Update();

        Label = label;
        TipoPost = tipoPost;
        TipoPostId = tipoPost.Id;
        Url = url;
        Path = path;
    }

    public void LiberarMenu()
    {
        Liberado = ELiberado.Sim;
    }

    public void BloquearMenu()
    {
        Liberado = ELiberado.Nao;
    }

    public void IsPrincipal()
    {
        Index = EIndex.Sim;
    }

    public void IsNotPrincipal()
    {
        Index = EIndex.Nao;
    }
}
