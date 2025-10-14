using ConformiTrain.Entities;
using ConformiTrain.Models;
using ConformiTrain.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConformiTrain.Controllers
{
    [Route("api/treinamentos")]
    [ApiController]
    public class TreinamentosController : ControllerBase
    {
        private readonly ConformiTrainDbContext _context;

        public TreinamentosController(ConformiTrainDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get(string pesquisar = "", int pagina = 0, int tamanho = 3)
        {
            if (pagina < 0) pagina = 0;
            if (tamanho <= 0) tamanho = 3;

            var query = _context.Treinamentos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisar))
            {
                query = query.Where(t => t.Titulo.Contains(pesquisar) || t.Descricao.Contains(pesquisar));
            }

            var totalRegistros = query.Count();

            var treinamentos = query
                .OrderBy(t => t.Id)
                .Skip(pagina * tamanho)
                .Take(tamanho)
                .ToList()
                .Select(TreinamentoViewModel.FromEntity);

            var resultado = new
            {
                Total = totalRegistros,
                Pagina = pagina,
                Tamanho = tamanho,
                Dados = treinamentos
            };

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var treinamento = _context.Treinamentos.Find(id);

            if (treinamento == null)
                return NotFound();

            return Ok(TreinamentoViewModel.FromEntity(treinamento));
        }

        [HttpPost]
        public IActionResult Post(CadastrarTreinamentoInputModel model)
        {
            if (model == null)
                return BadRequest();

            var treinamento = new Treinamento(
                model.Titulo,
                model.Descricao,
                model.CargaHoraria,
                model.DataVencimentoPadrao,
                model.Obrigatorio);

            _context.Treinamentos.Add(treinamento);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = treinamento.Id }, TreinamentoViewModel.FromEntity(treinamento));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AtualizarTreinamentoInputModel model)
        {
            if (model == null || model.IdTreinamento != id)
                return BadRequest();

            var treinamento = _context.Treinamentos.Find(id);
            if (treinamento == null)
                return NotFound();

            treinamento.Atualizar(
                model.Titulo,
                model.Descricao,
                model.CargaHoraria,
                model.Obrigatorio,
                model.DataVencimentoPadrao ?? treinamento.DataVencimentoPadrao.GetValueOrDefault()
            );

            _context.Treinamentos.Update(treinamento);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var treinamento = _context.Treinamentos.Find(id);

            if (treinamento == null)
                return NotFound();

            _context.Treinamentos.Remove(treinamento);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}/status")]
        public IActionResult AtualizarStatus(int id, AtualizarStatusTreinamentoInputModel model)
        {
            if (model == null)
                return BadRequest();

            var treinamento = _context.Treinamentos.Find(id);

            if (treinamento == null)
                return NotFound();

            switch (model.NovoStatus)
            {
                case Enums.StatusTreinamento.Ativo:
                    treinamento.Ativo();
                    break;
                case Enums.StatusTreinamento.Inativo:
                    treinamento.Inativo();
                    break;
                case Enums.StatusTreinamento.Concluido:
                    treinamento.Concluido();
                    break;
                default:
                    return BadRequest("Status inválido");
            }

            _context.Treinamentos.Update(treinamento);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
