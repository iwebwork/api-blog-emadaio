﻿using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Posts;

[Route("api/posts")]
public class HomeController(IResponseControler responseControler,
    IPostRepository repository,
    ITipoPostRepository tipoPostRepository) : BaseAutenticateController(responseControler)
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
        if (await repository.AnyAsync(requestViewModel.Name, requestViewModel.TipoPostId, cancellationToken))
        {
            responseControler.AddMessageErro("Existe um post com o mesmo nome e tipo cadastrado!");
            return;
        }

        var tipoPost = await tipoPostRepository.GetAsync(requestViewModel.TipoPostId, cancellationToken);

        if (tipoPost == null)
        {
            responseControler.AddMessageErro("O tipo de post informado não existe!");
            return;
        }

        Post model = new(name: requestViewModel.Name,
            title: requestViewModel.Title,
            date: requestViewModel.Date,
            image: requestViewModel.Image,
            tipoPostId: tipoPost.Id,
            corpo: requestViewModel.Corpo,
            liberado: requestViewModel.Liberado);


        await repository.InsertAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Post inserido com sucesso!");
    }

    [HttpPost, Route("edit")]
    public async Task EditAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(requestViewModel.Id.Value, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("O post informado não existe!");
            return;
        }

        var tipoPost = await tipoPostRepository.GetAsync(requestViewModel.TipoPostId, cancellationToken);

        if (tipoPost == null)
        {
            responseControler.AddMessageErro("O tipo de post informado não existe!");
            return;
        }

        model.Update(name: requestViewModel.Name,
            title: requestViewModel.Title,
            date: requestViewModel.Date,
            image: requestViewModel.Image,
            tipoPostId: tipoPost.Id,
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

        try
        {
            string mimeType = "image/png"; // "image/jpeg", "image/gif" e etc;
            byte[] imageBytes = GetImagemFromBase64(model.Image);
            return File(imageBytes, mimeType);
        }
        catch (FormatException)
        {
            return BadRequest("Formato Base64 inválido (após remoção do prefixo).");
        }
    }

    private static byte[] GetImagemFromBase64(string imageBase64)
    {
        string base64DataWithPrefix = imageBase64;

        // Extrai apenas os dados Base64
        string base64DataOnly = base64DataWithPrefix.Substring(base64DataWithPrefix.IndexOf(',') + 1);

        return Convert.FromBase64String(base64DataOnly);
    }
}
