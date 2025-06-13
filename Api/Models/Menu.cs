using Infraestrutura.Models;
using static Api.Models.Menu;

namespace Api.Models;

public class Menu(string label, TipoPost tipoPost, string url, string path, ELiberado liberado, EIndex index) : BaseModel
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
    public Guid TipoPostId { get; private set; } = tipoPost.Id;
    public virtual TipoPost TipoPost { get; private set; } = tipoPost;
    public string Url { get; private set; } = url;
    public string Path { get; private set; } = path;
    public ELiberado Liberado { get; private set; } = liberado;
    public EIndex Index { get; private set; } = index;

    public void Update(string label, TipoPost tipoPost, string url, string path, ELiberado liberado, EIndex index)
    {
        base.Update();

        Label = label;
        TipoPost = tipoPost;
        TipoPostId = tipoPost.Id;
        Url = url;
        Path = path;
        Liberado = liberado;
        Index = index;
    }

}
