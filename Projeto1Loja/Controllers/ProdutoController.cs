using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Projeto1Loja.Models;
using Projeto1Loja.Repositorio;

namespace Projeto1Loja.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoController(ProdutoRepositorio ProdutoRepositorio)
        {
            _produtoRepositorio = ProdutoRepositorio;
        }

        public IActionResult ListarProduto()
        {
            return View(_produtoRepositorio.TodosProdutos());
        }

        public IActionResult CadastrarProduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _produtoRepositorio.CadastrarProduto(produto);
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult EditarProduto(int idProduto)
        {
            var produto = _produtoRepositorio.ObterProduto(idProduto);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int idProduto, [Bind("idProduto, nome, preco,descricao,quantidade")] Produto produto)
        {
            if (idProduto != produto.idProduto)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (_produtoRepositorio.AtualizarProduto(produto))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao Editar.");
                    return View(produto);
                }
            }
            return View(produto);
        }

        public IActionResult TelaExcluirProduto(int idProduto)
        {
            var produto = _produtoRepositorio.ObterProduto(idProduto);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult ExcluirProduto(int idProduto)
        {
            _produtoRepositorio.Excluir(idProduto);
            return RedirectToAction(nameof(Index));
        }

    }
}
