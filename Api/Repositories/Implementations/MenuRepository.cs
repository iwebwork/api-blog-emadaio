using Api.Contexts;
using Api.Controllers.TipoPost;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class MenuRepository(PostgresDbContext context) :
    RelationalRepository<Menu, ResponseViewModel, PostgresDbContext>(context), IMenuRepository
{
    public Task<bool> AnyAsync(string label, string url, string path, CancellationToken cancellationToken)
    {
        return context.Menu
            .AnyAsync(s => s.Label == label
                        && s.Url == url
                        && s.Path == path, cancellationToken);
    }
}
