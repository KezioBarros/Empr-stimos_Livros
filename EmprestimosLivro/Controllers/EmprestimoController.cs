using EmprestimosLivro.Data;
using EmprestimosLivro.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosLivro.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _db;

        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimosModels> emprestimos = _db.Emprestimos;
            return View(emprestimos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModels emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimo == null)
            {
                return NotFound();
            }
            return View(emprestimo);
        }

        [HttpGet]
        public IActionResult Excluir(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            EmprestimosModels emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);
            if(emprestimo == null)
            {
                return NotFound();
            }
            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModels emprestimos)
        {
           if(ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimos);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Index");               
            }
            return View();
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModels emprestimo)
        {
            if(ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();
                TempData["MensagemSucesso"] = "Edição realizado com sucesso!";
                return RedirectToAction("Index");               
            }
            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimosModels emprestimo)
        {
            if(emprestimo == null)
            {
                return NotFound();
            }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            TempData["MensagemSucesso"] = "Remoção realizado com sucesso!";

            return RedirectToAction("Index");
        }
    }
}