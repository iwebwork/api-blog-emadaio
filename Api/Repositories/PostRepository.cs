using Api.Contexts;
using Api.Controllers.Posts;
using Api.Models;
using Infraestrutura.Repository.Implementations;

namespace Api.Repositories;

public class PostRepository(SqliteDbContext context) :
    RelationalRepository<Post, ResponseViewModel, SqliteDbContext>(context), IPostRepository
{
}
