using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers.TipoPost;

[Route("api/tipoPost")]
public class HomeController(IResponseControler responseControler,
    ITipoPostRepository repository) : BaseAutenticateController(responseControler)
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
        if (await repository.AnyAsync(requestViewModel.Nome, cancellationToken))
        {
            responseControler.AddMessageErro("Existe um tipo de post com o mesmo nome cadastrado!");
            return;
        }

        Models.TipoPost model = new(nome: requestViewModel.Nome);

        await repository.InsertAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Tipo de Post inserido com sucesso!");
    }

    [HttpPost, Route("edit")]
    public async Task EditAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(requestViewModel.Id.Value, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("O tipo de post informado não existe!");
            return;
        }

        model.Update(nome: requestViewModel.Nome);

        await repository.UpdateAsync(model, cancellationToken);
        responseControler.AddMessageSuccesso("Tipo de Post editado com sucesso!");
    }
}
