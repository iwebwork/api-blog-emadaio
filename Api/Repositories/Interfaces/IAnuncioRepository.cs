using Api.Contexts;
using Api.Controllers.Anuncios;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories.Interfaces;

public interface IAnuncioRepository : IRelationalRepository<Anuncio, ResponseViewModel, PostgresDbContext>
{
    /// <summary>
    /// Retorna se entidade existe de acordo com o nome e tipo informado.
    /// </summary>
    Task<bool> AnyAsync(string name, Anuncio.ETipo tipo, CancellationToken cancellationToken);

    /// <summary>
    /// Retorna o anuncio com o tipo especifico
    /// </summary>
    Task<ResponseViewModel> GetPorTipoAsync(Anuncio.ETipo tipo, CancellationToken cancellationToken);

}
