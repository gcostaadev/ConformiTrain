using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Reflection;
using ConformiTrain.Models;
using ConformiTrain.Persistence;
using Microsoft.EntityFrameworkCore;
using ConformiTrain.Entities;

namespace ConformiTrain.Controllers
{
    [Route("api/funcionarios")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly ConformiTrainDbContext _context;

        public FuncionariosController(ConformiTrainDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Get(string pesquisar = "", int pagina = 0, int tamanho = 3)
        {
            var funcionarios = _context.Funcionarios
                .Where(p => !p.IsDeleted &&
                       (pesquisar == "" ||
                        p.NomeCompleto.Contains(pesquisar) ||
                        p.Cargo.Contains(pesquisar)))
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToList();

            var model = funcionarios.Select(FuncionarioViewModel.FromEntity);

            return Ok(model);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var funcionario = _context.Funcionarios
                .Include(f => f.HistoricoTreinamentos)
                .Include(f => f.Comentarios)
                .SingleOrDefault(f => f.Id == id && !f.IsDeleted);

            if (funcionario == null)
                return NotFound($"Funcionário com ID {id} não encontrado.");

            var model = FuncionarioViewModel.FromEntity(funcionario);
            return Ok(model);
        }


        [HttpPost]
        public IActionResult Post(CadastrarFuncionarioInputModel model)
        {
            var funcionario = new Funcionario(
                model.NomeCompleto,
                model.Email,
                model.Departamento,
                model.Cargo,
                model.DataContratacao
            );

            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = funcionario.Id }, FuncionarioViewModel.FromEntity(funcionario));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AtualizarFuncionarioInputModel model)
        {
            if (id != model.IdFuncionario)
                return BadRequest("O ID informado na URL é diferente do corpo da requisição.");

            var funcionario = _context.Funcionarios.SingleOrDefault(f => f.Id == id);

            if (funcionario == null)
                return NotFound();

            funcionario.AtualizarDados(model.Email, model.Departamento, model.Cargo);

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var funcionario = _context.Funcionarios.SingleOrDefault(f => f.Id == id);

            if (funcionario == null)
                return NotFound();

            funcionario.Inativar();

            _context.SaveChanges();

            return NoContent();
        }



        [HttpGet("{id}/historico")]
        public IActionResult GetHistorico(int id, int pagina = 0, int tamanho = 3)
        {
            var funcionario = _context.Funcionarios
                .Include(f => f.HistoricoTreinamentos)
                    .ThenInclude(h => h.Treinamento)
                .SingleOrDefault(f => f.Id == id);

            if (funcionario == null)
                return NotFound();

            var historico = funcionario.HistoricoTreinamentos
                .OrderByDescending(h => h.DataParticipacao)
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .Select(h => new
                {
                    h.Id,
                    h.DataParticipacao,
                    h.Concluido,
                    h.DataInicioTreinamento,
                    h.DataFimTreinamento,
                    TreinamentoTitulo = h.Treinamento.Titulo
                })
                .ToList();

            return Ok(historico);
        }


        [HttpPost("{id}/participar/{treinamentoId}")]
        public IActionResult RegistrarParticipacao(int id, int treinamentoId)
        {
            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null)
                return NotFound("Funcionário não encontrado.");

            var treinamento = _context.Treinamentos.Find(treinamentoId);
            if (treinamento == null)
                return NotFound("Treinamento não encontrado.");

            // Cria um novo histórico usando o construtor público, que seta os campos necessários
            var historico = new HistoricoTreinamento(funcionario.Id, treinamento.Id, DateTime.UtcNow);

            _context.HistoricoTreinamentos.Add(historico);
            _context.SaveChanges();

            return Ok("Participação registrada com sucesso.");
        }


        [HttpPut("{id}/foto-do-funcionario")]
        public IActionResult FotoDoFuncionario(int id, IFormFile file)
        {

            var descricao = $"File: {file.FileName}, Size: {file.Length}";


            return Ok(descricao);

        }

    }

}
