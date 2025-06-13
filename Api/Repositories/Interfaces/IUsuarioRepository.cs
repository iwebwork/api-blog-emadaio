using Api.Contexts;
using Api.Controllers.Usuarios;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IUsuarioRepository : IRelationalRepository<Usuario, ResponseViewModel, PostgresDbContext>
{
}
