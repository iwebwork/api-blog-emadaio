using Api.Contexts;
using Api.Models;
using Infraestrutura.Controllers;
using Infraestrutura.Repository.Interfaces;
using Infraestrutura.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Posts;

[Route("Posts")]
public class PostsController(IResponseControler responseControler,
    IRelationalRepository<Post, ResponseViewModel, SqliteDbContext> repository,
    IValidationBase<Post, RequestViewModel, SqliteDbContext> validation) : CrudController<Post, RequestViewModel, ResponseViewModel, SqliteDbContext>(responseControler, repository, validation)
{

}
