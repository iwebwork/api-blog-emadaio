using Api.Contexts;
using Api.Controllers.Anuncios;
using Api.Models;
using Api.Repositories.Interfaces;
using Infraestrutura.Repository.Implementations;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories.Implementations;

public class AnuncioRepository(PostgresDbContext context) :
    RelationalRepository<Anuncio, ResponseViewModel, PostgresDbContext>(context), IAnuncioRepository
{
    public async Task<bool> AnyAsync(string name, Anuncio.ETipo tipo, CancellationToken cancellationToken)
    {
        return await context.Anuncio.AnyAsync(s => s.Name == name && s.Tipo == tipo, cancellationToken);
    }

    public async Task<ResponseViewModel> GetPorTipoAsync(Anuncio.ETipo tipo, CancellationToken cancellationToken)
    {
        return await context.Anuncio
            .Where(w => w.Tipo == tipo
                        && w.Liberado == Anuncio.ELiberado.Sim)
            .Select(s => new ResponseViewModel
            {
                Id = s.Id,
                Corpo = s.Corpo,
                Liberado = s.Liberado,
                LiberadoNome = s.Liberado.ToString(),
                Name = s.Name,
                Tipo = s.Tipo,
                TipoNome = s.Tipo.ToString(),
            }).SingleOrDefaultAsync(cancellationToken);
    }

    public override async Task<List<ResponseViewModel>> GetTableAsync(CancellationToken cancellationToken)
    {
        return await context.Anuncio.Select(s => new ResponseViewModel
        {
            Id = s.Id,
            Corpo = s.Corpo,
            Liberado = s.Liberado,
            LiberadoNome = s.Liberado.ToString(),
            Name = s.Name,
            Tipo = s.Tipo,
            TipoNome = s.Tipo.ToString(),
        }).ToListAsync(cancellationToken);
    }


}
