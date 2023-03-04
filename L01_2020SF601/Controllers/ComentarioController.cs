using L01_2020SF601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020SF601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly blogContext _blogContext;

        public ComentarioController(blogContext blogContext)
        {
            _blogContext = blogContext;
        }


        [HttpGet]
        [Route("getall")]
        public IActionResult showComments()
        {
            List<Comentario> listcomments = (from db in _blogContext.comentarios
                                            select db).ToList();
            if (listcomments.Count == 0) { return NotFound(); }
            return Ok(listcomments);
        }

        /// <summary>
        /// filtrado por id del comentario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbyid/{id}")]
        public IActionResult get(int id)
        {
            Comentario? comentario = (from e in _blogContext.comentarios
                                        where e.cometarioId == id
                                        select e).FirstOrDefault();
            if (comentario == null)
            {
                return NotFound();
            }
            return Ok(comentario);
        }

        /// <summary>
        /// filtrado por id de la publicacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getpublicbyid/{id}")]
        public IActionResult getpublicId(int id)
        {
            List<Comentario> comentarios = (from e in _blogContext.comentarios
                                             where e.publicacionId == id
                                             select e).ToList();
            if (comentarios.Any())
            {
                return Ok(comentarios);
            }

            return NotFound();
        }

        /// <summary>
        /// Buscar por filtro de acuerdo al comentario
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("findcomment")]
        public IActionResult findDescription(string filtro)
        {
            List<Comentario> lComentario = (from db in _blogContext.comentarios
                                                where db.comentario.Contains(filtro)
                                                select db).ToList();
            if (lComentario.Any())
            {
                return Ok(lComentario);
            }
            return NotFound();
        }

        /// <summary>
        /// Añadir un nuevo comenario
        /// </summary>
        /// <param name="nPublicacion"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        public IActionResult AddPost([FromBody] Comentario nComment)
        {
            try
            {
                _blogContext.comentarios.Add(nComment);
                _blogContext.SaveChanges();
                return Ok(nComment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult updatePost(int id, [FromBody] Comentario nComentario)
        {
            Comentario? comentarioUp = (from db in _blogContext.comentarios
                                          where db.cometarioId == id
                                          select db).FirstOrDefault();
            if (comentarioUp == null) { return NotFound(); }

            comentarioUp.publicacionId = nComentario.cometarioId;
            comentarioUp.comentario = nComentario.comentario;
            comentarioUp.usuarioId = nComentario.usuarioId;
            _blogContext.comentarios.Entry(comentarioUp).State = EntityState.Modified;
            _blogContext.SaveChanges();
            return Ok(comentarioUp);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult deletePost(int id)
        {
            Comentario? comDet = (from db in _blogContext.comentarios
                                           where db.cometarioId == id
                                           select db).FirstOrDefault();
            if (comDet == null) { return NotFound(); }
            _blogContext.comentarios.Attach(comDet);
            _blogContext.comentarios.Remove(comDet);
            _blogContext.SaveChanges();

            return Ok();
        }
    }
}
