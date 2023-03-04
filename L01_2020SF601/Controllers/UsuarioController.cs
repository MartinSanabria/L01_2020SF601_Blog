using L01_2020SF601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SF601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly blogContext _blogContext;

        public UsuarioController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult showUser()
        {
            List<Usuario> listUser = (from db in _blogContext.usuarios
                                      select db).ToList();
            if (listUser.Count == 0) { return NotFound(); }
            return Ok(listUser);
        }
        /// <summary>
        /// Recuperar al usuario mediante su nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {
            Usuario? usuario = (from e in _blogContext.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("getbynombre/{nombre}/{apellido}")]
        public IActionResult getUserName(string nombre, string apellido)
        {
                Usuario? usuario = (from db in _blogContext.usuarios
                               where db.nombre == nombre && db.apellido == apellido
                               select db).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Obtener una lista de usuarios a los cuales se les
        /// asigna un rol especifico
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("getbyrol/{rol}")]
        public IActionResult getUserRol(int rol)
        {
            List<Usuario> userListRol = (from db in _blogContext.usuarios
                                         where db.rolId == rol
                                         select db).ToList();

            if (userListRol.Any())
            {
                return Ok(userListRol);
            }

            return NotFound();
        }

        /// <summary>
        /// Añadir un nuevo usuario a la tabla usuarios
        /// </summary>
        /// <param name="userNew"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("add")]
        public IActionResult newUser([FromBody] Usuario userNew)
        {
            try
            {
                _blogContext.usuarios.Add(userNew);
                _blogContext.SaveChanges();
                return Ok(userNew);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Acutalizar usuario mediante id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userModf"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult UserUpdate(int id, [FromBody] Usuario userModf)
        {
            Usuario? usuario = (from db in _blogContext.usuarios
                                where db.usuarioId== id
                                select db).FirstOrDefault();

            if(usuario == null)
            {
                return NotFound();
            }

            usuario.nombreUsuario = userModf.nombreUsuario;
            usuario.clave = userModf.clave;
            usuario.nombre = userModf.nombre;
            usuario.apellido = userModf.apellido;

            _blogContext.usuarios.Entry(usuario).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(usuario);


        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult UserDelete(int id)
        {
            Usuario? usuarioD = (from db in _blogContext.usuarios
                                where db.usuarioId==id
                                select db).FirstOrDefault();
            if(usuarioD == null) { return NotFound(); }
            _blogContext.usuarios.Attach(usuarioD);
            _blogContext.usuarios.Remove(usuarioD);
            _blogContext.SaveChanges();

            return Ok();
        }

    }
}
