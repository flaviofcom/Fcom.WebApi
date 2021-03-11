using Fcom.WebApi.Data;
using Fcom.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fcom.WebApi.Controllers
{
    [ApiController]
    [Route("v1/clientes")]
    public class ClienteController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Cliente>>> GetAll([FromServices] DataContext context)
        {
            var clientes = await context.Clientes.ToListAsync();

            return clientes;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Cliente>> GetById([FromServices] DataContext context, [FromRoute] Guid Id)
        {
            var cliente = await context.Clientes
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(c => c.Id == Id);

            return cliente;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Cliente>> Post([FromServices] DataContext context, [FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.Id == Guid.Empty)
                    cliente.Id = Guid.NewGuid();

                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();
                return cliente;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put([FromServices] DataContext context, Guid id, [FromBody] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.Clientes.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromServices] DataContext context, [FromRoute] Guid Id)
        {
            var cliente = await context.Clientes
                                       .FirstAsync(c => c.Id == Id);

            if (cliente == null)
            {
                return NotFound();
            }

            context.Clientes.Remove(cliente);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return NoContent();
        }

    }
}
