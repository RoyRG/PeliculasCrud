using API.ENTIDADES.Entidades;
using API.ENTIDADES.Modelos;
using API.SERVICIOS.Servicios;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.SERVICIOS.AutoMapper
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<Categoria, CategoriaModelo>()
                .ForMember(c => c.Id, e => e.MapFrom(s => s.categoriaId))
                .ForMember(c => c.Nombre, e => e.MapFrom(s => s.Nombre))
                .ForMember(c => c.FechaCreacion, e => e.MapFrom(s => s.FechaCreacion))
                .ReverseMap();
            CreateMap<Pelicula, PeliculaModelo>().ReverseMap();
            CreateMap<Usuario, UsuarioModelo>().ReverseMap();
        }
    }
}
