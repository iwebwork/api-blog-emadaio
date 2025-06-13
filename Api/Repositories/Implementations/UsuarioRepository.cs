using Api.Contexts;
using Api.Controllers.Usuarios;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;

namespace Api.Repositories.Implementations;

public class UsuarioRepository(PostgresDbContext context) :
    RelationalRepository<Usuario, ResponseViewModel, PostgresDbContext>(context), IUsuarioRepository
{
}
