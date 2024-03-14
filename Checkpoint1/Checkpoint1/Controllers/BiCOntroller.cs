using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace _2TDSB.Controllers
{
    public class LivroController : Controller
    {
        private readonly IRepositorioLivros _repositorio;
        private readonly Biblioteca _biblioteca;

        public LivroController(IRepositorioLivros repositorio, Biblioteca biblioteca)
        {
            _repositorio = repositorio;
            _biblioteca = biblioteca;
        }

        public IActionResult Index()
        {
            List<Livro> livros = _repositorio.ListarLivros();
            return View("Index", livros);
        }

        public IActionResult Detalhes(string isbn)
        {
            Livro livro = _repositorio.BuscarLivro(isbn);
            if (livro == null)
            {
                return NotFound(); // Retorna 404 se o livro não for encontrado
            }
            return View(livro);
        }

        public IActionResult Emprestimo(string isbn, int usuarioId)
        {
            Livro livro = _repositorio.BuscarLivro(isbn);
            List<Usuario> usuarios = _biblioteca.GetUsuariosRegistrados() ?? new List<Usuario>();



            if (livro == null || usuarios == null)
            {
                return NotFound(); // Retorna 404 se o livro ou usuário não forem encontrados
            }

            if (!_biblioteca.VerificarDisponibilidade(livro))
            {
                TempData["Message"] = "O livro não está disponível para empréstimo.";
                return RedirectToAction("Index");
            }

            Emprestimo emprestimo = new Emprestimo
            {
                LivroEmprestado = livro,
                Usuario = usuarios,
                DataEmprestimo = DateTime.Now,
                DataDevolucao = DateTime.Now.AddDays(7)
            };

            // usuarios.HistoricoEmprestimos.Add(emprestimo); // Adiciona o empréstimo ao histórico do usuário

            TempData["Message"] = "Livro emprestado com sucesso.";
            return RedirectToAction("Index");
        }

        public IActionResult Devolucao(string isbn, int usuarioId, List<Emprestimo> emprestimos)
        {
            Livro livro = _repositorio.BuscarLivro(isbn);
            Usuario? usuarios = _biblioteca.GetUsuariosRegistrados().Find(u => u.ID == usuarioId);

            if (livro == null || usuarios == null)
            {
                return NotFound(); // Retorna 404 se o livro ou usuário não forem encontrados
            }

            List<Emprestimo> emprestimosUsuario = usuarios!.HistoricoEmprestimos;


            if (emprestimos == null)
            {
                TempData["Message"] = "O usuário não possui esse livro emprestado.";
                return RedirectToAction("Index");
            }

            // Lógica para calcular multa, se necessário
            double multa = CalcularMulta(emprestimos);

            if (multa > 0)
            {
                TempData["Message"] = $"O usuário deve pagar uma multa de {multa:C2} antes de devolver o livro.";
                return RedirectToAction("Index");
            }

            usuarios.HistoricoEmprestimos.RemoveAll(e => emprestimos.Contains(e));
            TempData["Message"] = "Livro devolvido com sucesso.";
            return RedirectToAction("Index");
        }

        private double CalcularMulta(List<Emprestimo> emprestimos)
        {
            throw new NotImplementedException();
        }

        private double CalcularMulta(Emprestimo emprestimo)
        {
            // Lógica para calcular a multa com base na data de devolução
            // Exemplo: R$ 0,50 por dia de atraso
            TimeSpan atraso = DateTime.Now - emprestimo.DataDevolucao;
            if (atraso.TotalDays > 0)
            {
                return atraso.TotalDays * 0.50;
            }
            return 0;
        }
    }
}
