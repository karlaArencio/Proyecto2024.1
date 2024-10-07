using Microsoft.AspNetCore.Mvc;
using Proyecto2024.BD.Data.Entity;
using Proyecto2024.BD.Data;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2024.Server.Controllers
{
    public class CitasControllers : ControllerBase
    {
        public readonly Context context;

        public CitasControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cita>>> Get()
        {
            return await context.Citas.ToListAsync();
        }
        [HttpGet("{id:int")]//api/Citas/
        public async Task<ActionResult<Cita>> Get(int id)
        {
            var pepe = await context.Citas
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pepe == null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("{NomU}")]//api/Citas/DNI
        public async Task<ActionResult<Cita>> GetByCod(string NomU)
        {
            var pepe = await context.Citas
                .FirstOrDefaultAsync(x => x.Fecha == FeCita);
            if (pepe == null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("Existe/{id:int}")] //api/Citas/existe/2
        public async Task<ActionResult<bool>> Existe(int id)
        {

            var existe = await context.Citas.AnyAsync(x => x.Id == id);
            return existe;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Cita entidad)
        {
            try
            {
                context.Citas.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id: int}")] //api/Citas/2
        public async Task<ActionResult> Put(int id, [FromBody] Cita entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }
            var pepe = await context.Citas
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if ((pepe == null))
            {
                return NotFound("No existe la clase de documento buscado.");

            }
            pepe.Fecha = entidad.Fecha;
            pepe.Hora = entidad.Hora;
            pepe.Motivo = entidad.Motivo;
            pepe.Lugar = entidad.Lugar;
           

            try
            {
                context.Citas.Update(pepe);

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id : int}")]//api/Citas/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Citas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"La clase de documento{id} no existe.");
            }
            Cita EntidadABorrar = new Cita();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }

       
    
    }
}
