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
    }
}
