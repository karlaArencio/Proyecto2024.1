using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto2024.BD.Data;
using Proyecto2024.BD.Data.Entity;

namespace Proyecto2024.Server.Controllers
{

    public class UsuariosControllers : ControllerBase
    {
        public readonly Context context;

        public UsuariosControllers(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.ToListAsync();
        }
        [HttpGet("{id:int")]//api/Usuarios/
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var pepe = await context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);
            if (pepe == null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("{NomU}")]//api/Usuarios/DNI
        public async Task<ActionResult<Usuario>> GetByCod(string NomU)
        {
            var pepe = await context.Usuarios
                .FirstOrDefaultAsync(x => x.NombreUsuario == NomU);
            if (pepe == null)
            {
                return NotFound();
            }
            return pepe;
        }

        [HttpGet("Existe/{id:int}")] //api/Usuarios/existe/2
        public async Task<ActionResult<bool>> Existe(int id)
        {

            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            return existe;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Usuario entidad)
        {
            try
            {
                context.Usuarios.Add(entidad);
                await context.SaveChangesAsync();
                return entidad.Id;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id: int}")] //api/Usuarios/2
        public async Task<ActionResult> Put(int id, [FromBody] Usuario entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos Incorrectos");
            }
            var pepe = await context.Usuarios
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            if ((pepe == null))
            {
                return NotFound("No existe la clase de documento buscado.");

            }
            pepe.NombreUsuario = entidad.NombreUsuario;
            pepe.Contraseña = entidad.Contraseña;
            pepe.Mail = entidad.Mail;
            pepe.Activo = entidad.Activo;

            try
            {
                context.Usuarios.Update(pepe);

                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id : int}")]//api/Usuarios/2
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound($"La clase de documento{id} no existe.");
            }
            Usuario EntidadABorrar = new Usuario();
            EntidadABorrar.Id = id;

            context.Remove(EntidadABorrar);
            await context.SaveChangesAsync();
            return Ok();
        }

       
    }
}
