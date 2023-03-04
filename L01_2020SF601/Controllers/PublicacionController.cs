using L01_2020SF601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SF601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionController : ControllerBase
    {
        private readonly blogContext _blogContext;

        public PublicacionController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult showPublicaciones()
        {
            List<Publicacion> listPublic = (from db in _blogContext.publicaciones
                                            select db).ToList();
            if (listPublic.Count == 0) { return NotFound(); }
            return Ok(listPublic);
        }

        /// <summary>
        /// Buscar por id de la publicacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {
            Publicacion? publicacion = (from e in _blogContext.publicaciones
                                        where e.publicacionId == id
                                        select e).FirstOrDefault();
            if (publicacion == null)
            {
                return NotFound();
            }
            return Ok(publicacion);
        }

        /// <summary>
        /// Filtar publicacion por id de usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getuserbyid/{id}")]
        public IActionResult getUserId(int id)
        {
            List<Publicacion> publicacion = (from e in _blogContext.publicaciones
                                             where e.usuarioId == id
                                             select e).ToList();
            if (publicacion.Any())
            {
                return Ok(publicacion);
            }

            return NotFound();
        }
        /// <summary>
        /// filtar por titulo o descripcion de la publicacion
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("findDescription")]
        public IActionResult findDescription(string filtro)
        {
            List<Publicacion> lPublicaciones = (from db in _blogContext.publicaciones
                                                where db.titulo.Contains(filtro)|| db.descripcion.Contains(filtro)
                                                select db).ToList();
            if (lPublicaciones.Any())
            {
                return Ok(lPublicaciones);
            }
            return NotFound();
        }

        /// <summary>
        /// Añadir una nueva publicacion
        /// </summary>
        /// <param name="nPublicacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IActionResult AddPost([FromBody] Publicacion nPublicacion)
        {
            try
            {
                _blogContext.publicaciones.Add(nPublicacion);
                _blogContext.SaveChanges();
                return Ok(nPublicacion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult updatePost(int id, [FromBody] Publicacion nPublicacion)
        {
            Publicacion? publicacionUp = (from db in _blogContext.publicaciones
                                          where db.publicacionId == id
                                          select db).FirstOrDefault();
            if (publicacionUp == null) { return NotFound(); }

            publicacionUp.titulo = nPublicacion.titulo;
            publicacionUp.descripcion = nPublicacion.descripcion;
            publicacionUp.usuarioId = nPublicacion.usuarioId;
            _blogContext.publicaciones.Entry(publicacionUp).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(publicacionUp);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult deletePost(int id)
        {
            Publicacion? publicacionDet = (from db in _blogContext.publicaciones
                                           where db.publicacionId == id
                                           select db).FirstOrDefault();
            if (publicacionDet == null) { return NotFound(); }
            _blogContext.publicaciones.Attach(publicacionDet);
            _blogContext.publicaciones.Remove(publicacionDet);
            _blogContext.SaveChanges();

            return Ok();
        }
    }
}
