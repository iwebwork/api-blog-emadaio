using Api.Contexts;
using Api.Controllers.TipoPost;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories.Interfaces;

public interface ITipoPostRepository : IRelationalRepository<TipoPost, ResponseViewModel, PostgresDbContext>
{
    /// <summary>
    /// Retorna se entidade existe de acordo com o nome e tipo informado.
    /// </summary>
    Task<bool> AnyAsync(string name, CancellationToken cancellationToken);
}
