using Infraestrutura.Models;

namespace Api.Models;

public class TipoPost(string nome) : BaseModel()
{
    public string Nome { get; private set; } = nome;

    public void Update(string nome)
    {
        base.Update();

        Nome = nome;
    }
}
