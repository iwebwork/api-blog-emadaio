using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Menu;

[Route("api/menu")]
public class HomeController(IResponseControler responseControler,
    IMenuRepository repository,
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
        if (await repository.AnyAsync(label: requestViewModel.Label,
            url: requestViewModel.Url,
            path: requestViewModel.Path,
            cancellationToken: cancellationToken))
        {
            responseControler.AddMessageErro("Existe um menu com os mesmos: Label/Url/Path cadastrado!");
            return;
        }

        var tipoPost = await tipoPostRepository.GetAsync(requestViewModel.TipoPostId, cancellationToken);

        if (tipoPost == null)
        {
            responseControler.AddMessageErro("O Tipo de Post informado não foi encontrado!");
            return;
        }

        Models.Menu model = new(label: requestViewModel.Label,
            tipoPostId: tipoPost.Id,
            url: requestViewModel.Url,
            path: requestViewModel.Path);

        if (requestViewModel.Liberado == Models.Menu.ELiberado.Sim)
        {
            model.LiberarMenu();
        }

        if (requestViewModel.Index == Models.Menu.EIndex.Sim)
        {
            model.IsPrincipal();
        }

        await repository.ValidateMenu(model, cancellationToken);

        if (responseControler.ResponseModel.IsValid)
        {
            await repository.InsertAsync(model, cancellationToken);
            responseControler.AddMessageSuccesso("Menu inserido com sucesso!");
        }
    }

    [HttpPost, Route("carga")]
    public async Task InsertListAsync(List<RequestViewModel> listRequestViewModel, CancellationToken cancellationToken)
    {
        foreach (var item in listRequestViewModel)
        {
            await InsertAsync(item, cancellationToken);

            if (!responseControler.ResponseModel.IsValid)
            {
                break;
            }
        }

        if (responseControler.ResponseModel.IsValid)
        {
            responseControler.AddMessageSuccesso("Carga Menu finalizado!");
        }
    }

    [HttpPost, Route("edit")]
    public async Task EditAsync(RequestViewModel requestViewModel, CancellationToken cancellationToken)
    {
        var model = await repository.GetAsync(requestViewModel.Id.Value, cancellationToken);

        if (model == null)
        {
            responseControler.AddMessageErro("O menu informado não existe!");
            return;
        }

        var tipoPost = await tipoPostRepository.GetAsync(requestViewModel.TipoPostId, cancellationToken);

        if (tipoPost == null)
        {
            responseControler.AddMessageErro("Existe um menu informado não foi encontrado!");
            return;
        }

        model.Update(label: requestViewModel.Label,
            tipoPost: tipoPost,
            url: requestViewModel.Url,
            path: requestViewModel.Path);

        if (requestViewModel.Index == Models.Menu.EIndex.Sim)
        {
            model.IsPrincipal();
        }

        if (requestViewModel.Liberado == Models.Menu.ELiberado.Sim)
        {
            model.LiberarMenu();
        }

        await repository.ValidateMenu(model, cancellationToken);

        if (responseControler.ResponseModel.IsValid)
        {
            await repository.UpdateAsync(model, cancellationToken);
            responseControler.AddMessageSuccesso("Menu editado com sucesso!");
        }

    }
}
