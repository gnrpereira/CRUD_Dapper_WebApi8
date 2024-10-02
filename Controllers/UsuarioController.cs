using CrudDapperWebApi8.Dto;
using CrudDapperWebApi8.Services;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapperWebApi8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if (usuarios.Status = false)
            {
                return NotFound(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int idUsuario)
        {
            var usuario = await _usuarioInterface.BuscarUsuarioPorId(idUsuario);

            if (usuario.Status = false)
            {
                return NotFound(usuario);
            }

            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(CriarUsuarioDto criarUsuarioDto)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(criarUsuarioDto);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(EditarUsuarioDto editarUsuarioDto)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(editarUsuarioDto);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirUsuario(int idUsuario)
        {
            var usuarios = await _usuarioInterface.ExcluirUsuario(idUsuario);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }

            return Ok(usuarios);
        }
    }
}
