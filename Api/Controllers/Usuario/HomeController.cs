using Api.Repositories.Interfaces;
using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Usuarios;

[Route("api/Usuario")]
public class HomeController(IResponseControler responseControler,
    IUsuarioRepository repository) : BaseAutenticateController(responseControler)
{

}
