using ConformiTrain.Models;
using ConformiTrain.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConformiTrain.Controllers
{
    [Route("api/historico")]
    [ApiController]
    public class HistoricoTreinamentosController : ControllerBase
    {
        private readonly ConformiTrainDbContext _context;

        public HistoricoTreinamentosController(ConformiTrainDbContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IActionResult Get(string pesquisar = "", int pagina = 0, int tamanho = 3)
        {
            var historicos = _context.HistoricoTreinamentos
                .Where(h => pesquisar == "" || h.Treinamento.Titulo.Contains(pesquisar))
                .OrderBy(h => h.DataParticipacao) // opcional: ordenar por data de participação
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToList();

            var model = historicos.Select(h => new HistoricoTreinamentoViewModel
            {
                Titulo = h.Treinamento.Titulo,
                TreinamentoConcluido = h.Concluido,
                DataParticipacao = h.DataParticipacao
            });

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var historico = _context.HistoricoTreinamentos
                .Include(h => h.Treinamento)
                .SingleOrDefault(h => h.Id == id);

            if (historico == null)
                return NotFound($"Histórico de treinamento com ID {id} não encontrado.");

            var model = new HistoricoTreinamentoViewModel
            {
                Titulo = historico.Treinamento.Titulo,
                TreinamentoConcluido = historico.Concluido,
                DataParticipacao = historico.DataParticipacao
            };

            return Ok(model);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, AtualizarHistoricoInputModel model)
        {
            var historico = _context.HistoricoTreinamentos.SingleOrDefault(h => h.Id == id);

            if (historico == null)
                return NotFound();

            historico.Atualizar(model.DataInicio, model.DataFim, model.Concluido);

            _context.HistoricoTreinamentos.Update(historico);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
