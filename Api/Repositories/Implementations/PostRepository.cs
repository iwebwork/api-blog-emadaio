using Api.Contexts;
using Api.Controllers.Posts;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class PostRepository(PostgresDbContext context) :
    RelationalRepository<Post, ResponseViewModel, PostgresDbContext>(context), IPostRepository
{
    public async Task<bool> AnyAsync(string name, Guid tipoPostId, CancellationToken cancellationToken)
    {
        return await context.Post.AnyAsync(s => s.Name == name && s.TipoPostId == tipoPostId, cancellationToken);
    }

    public override async Task<List<ResponseViewModel>> GetTableAsync(CancellationToken cancellationToken)
    {
        return await context.Post.Select(s => new ResponseViewModel
        {
            Id = s.Id,
            Corpo = s.Corpo,
            Date = s.Date,
            Image = s.Image,
            Liberado = s.Liberado,
            LiberadoNome = s.Liberado.ToString(),
            Name = s.Name,
            TipoPostId = s.TipoPost.Id,
            TipoNome = s.TipoPost.Nome,
            Title = s.Title
        }).ToListAsync(cancellationToken);
    }
}
