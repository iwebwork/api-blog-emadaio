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
    public Task<bool> AnyAsync(string nome, CancellationToken cancellationToken)
    {
        return context.TipoPost.AnyAsync(s => s.Nome == nome, cancellationToken);
    }
}
