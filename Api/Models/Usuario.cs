using Infraestrutura.Models;

namespace Api.Models;

public class Usuario(string nome, string email, string senha, Guid token) : BaseModel
{
    public string Nome { get; private set; } = nome;
    public string Email { get; private set; } = email;
    public string Senha { get; private set; } = senha;
    public Guid Token { get; private set; } = token;
}
