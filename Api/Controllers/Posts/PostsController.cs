using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Posts;

[Route("api/posts")]
public class PostsController(IResponseControler responseControler) : BaseAutenticateController(responseControler)
{
    [HttpPost, Route("getTable")]
    public async Task GetRequest(CancellationToken cancellationToken)
    {
        responseControler.AddMessageSuccesso("Requisição feita com sucesso!");
    }
}
