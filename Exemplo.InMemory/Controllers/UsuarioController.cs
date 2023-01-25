using Exemplo.InMemory.Context;
using Exemplo.InMemory.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Exemplo.InMemory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsuarioController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet("ids")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            var result = _context.Usuarios.AsNoTracking().ToList();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Post([FromServices] ApiContext context, Usuario usuario)
        {
            context.Usuarios.Add(usuario);
            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete([FromServices] ApiContext context, int id)
        {
            if (id <= 0)
                return NotFound();

            var result = context.Usuarios.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            if (result == default)
                return NotFound();

            context.Usuarios.Remove(result);
            context.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Update([FromServices] ApiContext context, Usuario usuario)
        {
            if (usuario.Id <= 0)
                return NotFound();

            var result = context.Usuarios.AsNoTracking().Where(x => x.Id == usuario.Id).FirstOrDefault();

            if (result == default)
                return NotFound();

            context.Usuarios.Update(usuario);
            context.SaveChanges();

            return Accepted();
        }

        [HttpGet("busca-usuario-e-nome")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult GetIdAndName([FromServices] ApiContext context, [FromQuery] string nome, [FromQuery] string email)
        {
            var result = context.Usuarios
                .AsNoTracking()
                .Where(x => x.Nome == nome && x.Email.Equals(email))
                .ToList();

            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetId([FromServices] ApiContext context, [FromRoute] int id)
        {
            if (id <= 0)
                return NotFound();

            var result = context.Usuarios.AsNoTracking().Where(x => x.Id == id).FirstOrDefault();

            if (result == default)
                return NotFound();

            return Ok(result);
        }
    }
}