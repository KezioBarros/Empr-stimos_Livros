using ClosedXML.Excel;
using EmprestimosLivro.Data;
using EmprestimosLivro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados emprestimos";
           
            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));

            dataTable.Columns.Add("Data última atualização", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if(dados.Count > 0)

            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestado, emprestimo.DataUtimaAtualizacao);
                });
            }

            return dataTable;
        }

        [HttpGet]
        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workBook = new XLWorkbook())
            {
                workBook.AddWorksheet(dados, "Dados Empréstimos");
                using (MemoryStream ms = new MemoryStream())
                {
                    workBook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Emprestimo.xls");

                }
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModels emprestimos)
        {
           if(ModelState.IsValid)
            {
                emprestimos.DataUtimaAtualizacao = DateTime.Now;
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
                var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id);

                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                emprestimoDB.Recebedor = emprestimo.Recebedor;
                emprestimoDB.LivroEmprestado = emprestimo.LivroEmprestado;

                _db.Emprestimos.Update(emprestimoDB);
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