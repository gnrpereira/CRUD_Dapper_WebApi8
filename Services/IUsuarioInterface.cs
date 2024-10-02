using CrudDapperWebApi8.Dto;
using CrudDapperWebApi8.Models;

namespace CrudDapperWebApi8.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<ListarUsuarioDto>>> BuscarUsuarios();
        Task<ResponseModel<ListarUsuarioDto>> BuscarUsuarioPorId(int idUsuario);
        Task<ResponseModel<List<ListarUsuarioDto>>> CriarUsuario(CriarUsuarioDto criarUsuarioDto);
        Task<ResponseModel<List<ListarUsuarioDto>>> EditarUsuario(EditarUsuarioDto editarUsuarioDto);
        Task<ResponseModel<List<ListarUsuarioDto>>> ExcluirUsuario(int idUsuario);
    }
}
