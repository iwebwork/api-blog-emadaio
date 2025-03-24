using Api.Contexts;
using Api.Controllers.Posts;
using Api.Models;
using Infraestrutura.Repository.Interfaces;

namespace Api.Repositories;

public interface IPostRepository : IRelationalRepository<Post, ResponseViewModel, SqliteDbContext>
{
}
