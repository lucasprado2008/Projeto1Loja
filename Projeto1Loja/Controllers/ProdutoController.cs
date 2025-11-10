using Microsoft.AspNetCore.Mvc;

namespace Projeto1Loja.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
