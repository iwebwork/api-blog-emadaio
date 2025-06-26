using Api.Contexts;
using Api.Controllers.Menu;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IMenuRepository : IRelationalRepository<Menu, ResponseViewModel, PostgresDbContext>
{
    /// <summary>
    /// Retorna se entidade existe de acordo com o label, url e path informado.
    /// </summary>

    Task<bool> AnyAsync(string label, string url, string path, CancellationToken cancellationToken);
}
