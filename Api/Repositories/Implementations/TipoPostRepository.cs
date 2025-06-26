using Api.Contexts;
using Api.Controllers.TipoPost;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class TipoPostRepository(PostgresDbContext context) :
    RelationalRepository<TipoPost, ResponseViewModel, PostgresDbContext>(context), ITipoPostRepository
{
    public async Task<bool> AnyAsync(string nome, CancellationToken cancellationToken)
    {
        return await context.TipoPost.AnyAsync(s => s.Nome == nome, cancellationToken);
    }

    public override async Task<List<ResponseViewModel>> GetTableAsync(CancellationToken cancellationToken)
    {
        return await context.TipoPost.Select(s => new ResponseViewModel
        {
            Id = s.Id,
            Nome = s.Nome
        }).ToListAsync(cancellationToken);
    }
}
