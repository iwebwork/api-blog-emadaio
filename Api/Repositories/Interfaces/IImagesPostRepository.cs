using Api.Contexts;
using Api.Models;
using Infraestrutura.Repository.Interfaces;
using Infraestrutura.ViewModels;

namespace Api.Repositories.Interfaces;

public interface IImagesPostRepository : IRelationalRepository<ImagesPost, BaseViewModel, SqliteDbContext>
{
}
