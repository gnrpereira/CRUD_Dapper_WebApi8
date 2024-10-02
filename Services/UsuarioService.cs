using AutoMapper;
using CrudDapperWebApi8.Dto;
using CrudDapperWebApi8.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudDapperWebApi8.Services
{
    public class UsuarioService : IUsuarioInterface
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ResponseModel<List<ListarUsuarioDto>>> BuscarUsuarios()
        {
            ResponseModel<List<ListarUsuarioDto>> response = new ResponseModel<List<ListarUsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var usuariosBanco = await connection.
                    QueryAsync<Usuario>("SELECT * FROM Usuarios");

                if (usuariosBanco.Count() == 0)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }


                //Transformação Mapper
                var usuarioMapeado = _mapper.Map<List<ListarUsuarioDto>>(usuariosBanco);


                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuários localizados com sucesso!";

            }

            return response;
        }

        public async Task<ResponseModel<ListarUsuarioDto>> BuscarUsuarioPorId(int idUsuario)
        {
            ResponseModel<ListarUsuarioDto> response = new ResponseModel<ListarUsuarioDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var usuariosBanco = await connection.
                    QueryFirstOrDefaultAsync<Usuario>("SELECT * FROM Usuarios WHERE id = @Id", new { Id = idUsuario });

                if (usuariosBanco == null)
                {
                    response.Mensagem = "Nenhum usuário localizado!";
                    response.Status = false;
                    return response;
                }


                //Transformação Mapper
                var usuarioMapeado = _mapper.Map<ListarUsuarioDto>(usuariosBanco);


                response.Dados = usuarioMapeado;
                response.Mensagem = "Usuário localizado com sucesso!";


            }

            return response;

        }

        public async Task<ResponseModel<List<ListarUsuarioDto>>> CriarUsuario(CriarUsuarioDto criarUsuarioDto)
        {
            ResponseModel<List<ListarUsuarioDto>> response = new ResponseModel<List<ListarUsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {

                var usuariosBanco = await connection.
                    ExecuteAsync("INSERT INTO Usuarios (NomeCompleto, Email, Cargo, Salario, CPF, Senha, Situacao) "
                    + "values (@NomeCompleto, @Email, @Cargo, @Salario, @CPF, @Senha, @Situacao)", criarUsuarioDto);


                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar o registro!";
                    response.Status = false;
                    return response;
                }


                var usuarios = await ListarUsuarios(connection);

                //Transformação Mapper
                var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";
            }

            return response;
        }

        private static async Task<IEnumerable<Usuario>> ListarUsuarios(SqlConnection connection)
        {
            return await connection.QueryAsync<Usuario>("SELECT * FROM Usuarios");
        }

        public async Task<ResponseModel<List<ListarUsuarioDto>>> EditarUsuario(EditarUsuarioDto editarUsuarioDto)
        {
            ResponseModel<List<ListarUsuarioDto>> response = new ResponseModel<List<ListarUsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.
                    ExecuteAsync("UPDATE Usuarios SET NomeCompleto = @NomeCompleto, Email = @Email, Cargo = @Cargo, Salario = @Salario, CPF = @CPF, Situacao = @Situacao WHERE id = @Id", editarUsuarioDto);


                if (usuariosBanco == 0)
                {
                    response.Mensagem = "Ocorreu um erro ao realizar a edição!";
                    response.Status = false;
                    return response;
                }

                var usuarios = await ListarUsuarios(connection);


                var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";

            }

            return response;
        }

        public async Task<ResponseModel<List<ListarUsuarioDto>>> ExcluirUsuario(int idUsuario)
        {
            ResponseModel<List<ListarUsuarioDto>> response = new ResponseModel<List<ListarUsuarioDto>>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var usuariosBanco = await connection.
                    ExecuteAsync("DELETE FROM Usuarios WHERE id = @Id", new { Id = idUsuario });

                if (usuariosBanco == null)
                {
                    response.Mensagem = "Usuário não foi localizado!";
                    response.Status = false;

                    return response;
                }

                var usuarios = await ListarUsuarios(connection);

                var usuariosMapeados = _mapper.Map<List<ListarUsuarioDto>>(usuarios);

                response.Dados = usuariosMapeados;
                response.Mensagem = "Usuários listados com sucesso!";

            }

            return response;
        }
    }
}
