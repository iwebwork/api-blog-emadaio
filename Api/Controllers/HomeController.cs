using Infraestrutura.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/test")]
public class HomeController(IResponseControler responseController) : BaseController(responseController)
{
    [HttpGet]
    public void Index()
    {
        responseController.AddMessageSuccesso("Deu");
    }
}
