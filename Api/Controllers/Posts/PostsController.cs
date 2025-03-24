using Api.Repositories;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Posts;

[Route("api/posts")]
public class PostsController(IResponseControler responseControler,
    IPostRepository repository) : BaseAutenticateController(responseControler)
{
    [HttpPost, Route("getTable")]
    public async Task GetRequest(CancellationToken cancellationToken)
    {
        responseControler.SetResponseData(await repository.GetTableAsync(cancellationToken));
        responseControler.AddMessageSuccesso("Requisição feita com sucesso!");
    }
}
