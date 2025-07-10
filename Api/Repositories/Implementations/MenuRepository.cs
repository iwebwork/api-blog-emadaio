using Api.Contexts;
using Api.Controllers.Menu;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class MenuRepository(PostgresDbContext context,
    IResponseControler responseControler) :
    RelationalRepository<Menu, ResponseViewModel, PostgresDbContext>(context), IMenuRepository
{
    public async Task<bool> AnyAsync(string label, string url, string path, CancellationToken cancellationToken)
    {
        return await context.Menu
            .AnyAsync(s => s.Label == label
                        && s.Url == url
                        && s.Path == path, cancellationToken);
    }

    public override async Task<List<ResponseViewModel>> GetTableAsync(CancellationToken cancellationToken)
    {
        return await context.Menu.Select(s => new ResponseViewModel
        {
            Id = s.Id,
            Label = s.Label,
            TipoPostId = s.TipoPostId,
            TipoPostNome = s.TipoPost.Nome,
            Url = s.Url,
            Path = s.Path,
            Liberado = s.Liberado,
            Index = s.Index
        }).ToListAsync(cancellationToken);
    }

    public async Task InsertAsync(Menu model, CancellationToken cancellationToken)
    {
        await ValidateMenu(model, cancellationToken);
        responseControler.SetResponse();

        if (!responseControler.ResponseModel.IsValid)
        {
            return;
        }


        await base.InsertAsync(model, cancellationToken);
    }

    public override async Task UpdateAsync(Menu model, CancellationToken cancellationToken)
    {
        await ValidateMenu(model, cancellationToken);

        responseControler.SetResponse();

        if (!responseControler.ResponseModel.IsValid)
        {
            return;
        }

        await base.UpdateAsync(model, cancellationToken);
    }

    #region Metodos Privados

    // TODO: Verificar se será necessario usar uma validationService
    private async Task ValidateMenu(Menu model, CancellationToken cancellationToken)
    {
        var menus = await context.Menu.ToListAsync(cancellationToken);

        menus.Add(model);

        if (menus.Count() > 1 & (menus.Count(w => w.Index == Menu.EIndex.Sim) != 1))
        {
            responseControler.AddMessageErro("Precisa ter um menu como principal!");
        }

        if (menus.Count(s => model.Label.Contains(s.Label)
                          || model.Url.Contains(s.Url)
                          || model.Path.Contains(s.Path)) > 1)
        {
            responseControler.AddMessageErro("A Label, Url e Path do menu precisam ser unicas!");
        }
    }

    #endregion
}
