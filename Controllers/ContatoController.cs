using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using Microsoft.AspNetCore.Mvc;
using API.Entities;

namespace API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;
        public ContatoController( AgendaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Create( Contato contato)
        {
            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorID), new { ID = contato.ID}, contato);
        }
        
        [HttpGet("{ID}")]
         public IActionResult ObterPorID(int ID)
        {
            var contato = _context.contatos.Find(ID);
           
            if (contato == null)
                 return NotFound();

             return Ok(contato);
         }

        [HttpGet("ObterPorNome")]
         public IActionResult ObterPorNome(string nome)
         {
            var contatos = _context.contatos.Where(x => x.nome.Contains(nome));
            return Ok(contatos);
        }

        [HttpGet("ObterPorTelefone")]
        public IActionResult ObterPorContato(string Telefone)
        {
            var contatos = _context.contatos.Where(x => x.Telefone.Contains(Telefone));
            return Ok(contatos);
        }

        [HttpPut("{ID}")]
         public IActionResult Atualizar(int ID, Contato contato)
         {
            var contatoBanco = _context.contatos.Find(ID);

            if(contatoBanco == null)
                return NotFound();

            contatoBanco.nome = contato.nome;
            contatoBanco.Telefone = contato.Telefone;
            contatoBanco.Ativo = contato.Ativo;

            _context.contatos.Update(contatoBanco);
            _context.SaveChanges();

            return Ok(contatoBanco);
        }

        [HttpDelete("{ID}")]
        public IActionResult Deletar(int ID)
        {
            var contatoBanco = _context.contatos.Find(ID);

            if(contatoBanco == null)
                return NotFound();

            _context.contatos.Remove(contatoBanco);
            _context.SaveChanges();
            return NoContent();
        }

        
    }
}