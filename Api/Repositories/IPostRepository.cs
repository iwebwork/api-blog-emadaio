using Api.Contexts;
using Api.Controllers.Posts;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories;

public interface IPostRepository : IRelationalRepository<Post, ResponseViewModel, SqliteDbContext>
{
    /// <summary>
    /// Retorna se entidade existe de acordo com o nome e tipo informado.
    /// </summary>
    Task<bool> AnyAsync(string name, Post.ETipo tipo, CancellationToken cancellationToken);
}
