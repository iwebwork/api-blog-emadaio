using Infraestrutura.Models;

namespace Api.Models;

public class UsuarioToken(string token, DateTime expiration, Guid usuarioId) : BaseModel((Guid.NewGuid()))
{
    public string Token { get; private set; } = token;
    public DateTime Expiration { get; private set; } = expiration;
    public Guid UsuarioId { get; private set; } = usuarioId;

    public virtual Usuario Usuario { get; private set; }


    public void Update(string token, DateTime expiration)
    {
        base.Update();

        Token = token;
        Expiration = expiration;
    }
}
