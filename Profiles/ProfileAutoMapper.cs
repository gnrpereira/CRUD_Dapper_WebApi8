using AutoMapper;
using CrudDapperWebApi8.Dto;
using CrudDapperWebApi8.Models;

namespace CrudDapperWebApi8.Profiles
{
    public class ProfileAutoMapper : Profile
    {
        public ProfileAutoMapper()
        {
            CreateMap<Usuario, ListarUsuarioDto>();
        }
    }
}
