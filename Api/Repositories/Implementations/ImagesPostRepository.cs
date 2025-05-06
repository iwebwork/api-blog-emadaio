using Api.Contexts;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Infraestrutura.ViewModels;

namespace Api.Repositories.Implementations;

public class ImagesPostRepository(PostgresDbContext context) :
    RelationalRepository<ImagesPost, BaseViewModel, PostgresDbContext>(context), IImagesPostRepository
{
}
