using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2024.BD.Data;
using Proyecto2024.BD.Data.Entity;

namespace Proyecto2024.Server.Controllers
{
    [ApiController]
    [Route("api/CDocumentos")]
    public class CDocumentosControllers : ControllerBase
    {
        private readonly Context context;

        public CDocumentosControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CDocumento>>> Get()
        {
            return await context.CDocumentos.ToListAsync();
        }
        [HttpGet("{id:int")]//api/CDocumentos/2
        public async Task<ActionResult<CDocumento>> Get(int id)
        {
            var pepe = await context.CDocumentos.FirstOrDefaultAsync(x => x.Id == id);
            if (pepe ==null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("{cod}")]//api/CDocumentos/DNI
        public async Task<ActionResult<CDocumento>> GetByCod(string cod)
        {
            var pepe = await context.CDocumentos.FirstOrDefaultAsync(x => x.Codigo == cod);
            if (pepe == null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("Existe/{id:int}")] //api/CDocumento/existe/2
        public async Task<ActionResult<bool>> Existe(int id)
        {
            
            var existe = await context.CDocumentos.AnyAsync(x => x.Id == id);
            return existe;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(CDocumento entidad)
        {
            try
            {
                context.CDocumentos.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id: int}")] //api/CDocumento/2
        public async Task<ActionResult> Put(int id, [FromBody] CDocumento entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }
            var pepe = await context.CDocumentos
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if ( (pepe == null))
            {
                return NotFound("No existe la clase de documento buscado.");

            }
            pepe.Codigo = entidad.Codigo;
            pepe.Nombre = entidad.Nombre;
            pepe.Activo = entidad.Activo;

            try
            {
                context.CDocumentos.Update(pepe);

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id : int}")]//api/CDocumento/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.CDocumentos.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"La clase de documento{id} no existe.");
            }
            CDocumento EntidadABorrar = new CDocumento();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
