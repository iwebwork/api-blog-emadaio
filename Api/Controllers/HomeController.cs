using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Ola mundo";
        }
    }
}
