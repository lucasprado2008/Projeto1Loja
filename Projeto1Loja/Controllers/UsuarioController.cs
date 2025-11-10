using Microsoft.AspNetCore.Mvc;
using Projeto1Loja.Models;
using Projeto1Loja.Repositorio;

namespace Projeto1Loja.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(UsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(String email, String senha)
        {
            var Usuario = _usuarioRepositorio.ProcurarUsuario(email);
            if (Usuario != null && Usuario.senha == senha)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Email / Senha inválidos");
            return View();
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.CadastrarUsuario(usuario);
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
