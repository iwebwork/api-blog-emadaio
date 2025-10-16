using Api.Contexts;
using Api.Controllers.Menu;
using Api.Models;
using Infraestrutura.Repository.Interfaces;
using static Api.Models.Menu;

namespace Api.Repositories.Interfaces;

public interface IMenuRepository : IRelationalRepository<Menu, ResponseViewModel, PostgresDbContext>
{
    /// <summary>
    /// Retorna se entidade existe de acordo com o label, url e path informado.
    /// </summary>
    Task<bool> AnyAsync(string label, string url, string path, CancellationToken cancellationToken);

    // TODO: Verificar se será necessario usar uma validationService
    Task ValidateMenu(Menu model, CancellationToken cancellationToken);

    /// <summary>
    /// Buscar Menus 
    /// </summary>
    /// <returns></returns>
    Task<List<ResponseViewModel>> GetTableAsync(ELiberado liberado, CancellationToken cancellationToken);

    /// <summary>
    /// Retornar o menu principal 
    /// </summary>
    /// <returns></returns>
    Task<ResponseViewModel> GetTableMenuIndexAsync(CancellationToken cancellationToken);
}
