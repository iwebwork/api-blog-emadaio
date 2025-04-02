using Api.Models;
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

    [HttpPost, Route("select/{id}")]
    public async Task GetRequest(Guid id, CancellationToken cancellationToken)
    {
        responseControler.SetResponseData(await repository.GetAsync(id, cancellationToken));
        responseControler.AddMessageSuccesso("Requisição feita com sucesso!");
    }

    [HttpPost, Route("insert")]
    public async Task InsertAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(requestViewModel.Name, requestViewModel.Tipo, cancellationToken))
        {
            responseControler.AddMessageErro("Existe um post com o mesmo nome e tipo cadastrado!");
            return;
        }

        Post model = new(name: requestViewModel.Name,
            title: requestViewModel.Title,
            date: requestViewModel.Date,
            image: requestViewModel.Image,
            tipo: requestViewModel.Tipo,
            corpo: requestViewModel.Corpo,
            liberado: requestViewModel.Liberado);


        await repository.InsertAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Post inserido com sucesso!");
    }

    [HttpPost, Route("edit")]
    public async Task EditAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        if (!await repository.AnyAsync(requestViewModel.Id.Value, cancellationToken))
        {
            responseControler.AddMessageErro("O post informado não existe!");
            return;
        }

        Post model = new(id: requestViewModel.Id.Value,
            name: requestViewModel.Name,
            title: requestViewModel.Title,
            date: requestViewModel.Date,
            image: requestViewModel.Image,
            tipo: requestViewModel.Tipo,
            corpo: requestViewModel.Corpo,
            liberado: requestViewModel.Liberado);


        await repository.UpdateAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Post editado com sucesso!");
    }

    [HttpPost, Route("liberarPost/{id}")]
    public async Task LiberarPostAsync(Guid id, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(id, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("Post não foi encontrado!");
            return;
        }

        model.LiberarPost();

        await repository.UpdateAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Post editado com sucesso!");
    }

    [HttpGet, Route("getImagem/{id}")]
    public async Task<IActionResult> GetImagemAsync(Guid id, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(id, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("Post não foi encontrado!");
            return NotFound();
        }

        if (string.IsNullOrEmpty(model.Image))
        {
            responseControler.AddMessageErro("Imagem não foi encontrada!");
            return NotFound();
        }

        string base64DataWithPrefix = model.Image;

        string mimeType = "image/png"; // "image/jpeg", "image/gif" e etc;

        // Extrai apenas os dados Base64
        string base64DataOnly = base64DataWithPrefix.Substring(base64DataWithPrefix.IndexOf(',') + 1);

        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64DataOnly);
            return File(imageBytes, mimeType);
        }
        catch (FormatException)
        {
            return BadRequest("Formato Base64 inválido (após remoção do prefixo).");
        }
    }
}
