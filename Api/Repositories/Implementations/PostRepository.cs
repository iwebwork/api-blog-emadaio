using Api.Contexts;
using Api.Controllers.Posts;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class PostRepository(SqliteDbContext context) :
    RelationalRepository<Post, ResponseViewModel, SqliteDbContext>(context), IPostRepository
{
    public Task<bool> AnyAsync(string name, Post.ETipo tipo, CancellationToken cancellationToken)
    {
        return context.Posts.AnyAsync(s => s.Name == name && s.Tipo == tipo, cancellationToken);
    }

    public override Task<List<ResponseViewModel>> GetTableAsync(CancellationToken cancellationToken)
    {
        return context.Posts.Select(s => new ResponseViewModel
        {
            Id = s.Id,
            Corpo = s.Corpo,
            Date = s.Date,
            Image = s.Image,
            Liberado = s.Liberado,
            LiberadoNome = s.Liberado.ToString(),
            Name = s.Name,
            Tipo = s.Tipo,
            TipoNome = s.Tipo.ToString(),
            Title = s.Title
        }).ToListAsync(cancellationToken);
    }
}
