using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Infraestrutura.Selenium;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Anuncios;

[Route("api/anuncios")]
public class HomeController(IResponseControler responseControler,
    IAnuncioRepository repository,
    SeleniumBase seleniumBase) : BaseAutenticateController(responseControler)
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

    [HttpPost, Route("selectTipo/{tipo}")]
    public async Task GetPorTipoRequest(int tipo, CancellationToken cancellationToken)
    {
        responseControler.SetResponseData(await repository.GetPorTipoAsync((Anuncio.ETipo)tipo, cancellationToken));
        responseControler.AddMessageSuccesso("Requisição feita com sucesso!");
    }

    [HttpPost, Route("insert")]
    public async Task InsertAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        if (await repository.AnyAsync(requestViewModel.Name, requestViewModel.Tipo, cancellationToken))
        {
            responseControler.AddMessageErro("Existe um anuncio com o mesmo nome e tipo cadastrado!");
            return;
        }

        Anuncio model = new(name: requestViewModel.Name,
            tipo: requestViewModel.Tipo,
            corpo: requestViewModel.Corpo,
            liberado: requestViewModel.Liberado);


        await repository.InsertAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Anuncio inserido com sucesso!");
    }

    [HttpPost, Route("edit")]
    public async Task EditAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(requestViewModel.Id.Value, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("O Anuncio informado não existe!");
            return;
        }

        model.Update(name: requestViewModel.Name,
            tipo: requestViewModel.Tipo,
            corpo: requestViewModel.Corpo,
            liberado: requestViewModel.Liberado);

        await repository.UpdateAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Anuncio editado com sucesso!");
    }
}
