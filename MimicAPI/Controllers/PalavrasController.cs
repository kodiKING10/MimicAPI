using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimicAPI.Helpers;
using MimicAPI.Models;
using MimicAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MimicAPI.Controllers
{
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase
    {
        private readonly IPalavraRepository _palavraRepository;

        public PalavrasController(IPalavraRepository palavraRepository)
        {
            this._palavraRepository = palavraRepository;
        }

        [HttpGet]
        public ActionResult ObterTodas([FromQuery]ObterPalavraUrlQuery query)
        {
            var item = _palavraRepository.ObterPalavras(query);

            if (query.PagNumero > item.Pagination.TotalPaginas)
            {
                return StatusCode(204);
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.Pagination));

            return Ok(item.ToList());
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult ObterPalavra(int id)
        {
            var obj = _palavraRepository.Obter(id);
            if (obj == null)
                return StatusCode(204);
            
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]Palavra palavra)
        {
            _palavraRepository.Cadastrar(palavra);
            return Created($"/api/palavras/{palavra.Id}", palavra);
        }

        [Route("{id}")]
        [HttpPut]
        public ActionResult Atualizar(int id, [FromBody]Palavra palavra)
        {
            var obj = _palavraRepository.Obter(id);
            if (obj == null)
                return StatusCode(204);

            palavra.Id = id;
            _palavraRepository.Atualizar(palavra);

            return StatusCode(204);
        }

        [Route("{id}")]
        [HttpDelete]
        public ActionResult Deletar(int id)
        {
            var palavra = _palavraRepository.Obter(id);
            if (palavra == null)
                return StatusCode(204);

            _palavraRepository.Deletar(id);

            return NoContent();
        }
    }
}
