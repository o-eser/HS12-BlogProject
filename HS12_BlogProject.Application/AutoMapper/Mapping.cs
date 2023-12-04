using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HS12_BlogProject.Common.Models.DTOs;
using HS12_BlogProject.Common.Models.VMs;
using HS12_BlogProject.Domain.Entities;

namespace HS12_BlogProject.Application.AutoMapper
{
	public class Mapping : Profile
	{
        public Mapping()
        {
            CreateMap<Post,CreatePostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap();
            CreateMap<Post, PostVM>().ReverseMap();
            CreateMap<Post, PostDetailsVM>().ReverseMap();

            CreateMap<Author, CreateAuthorDTO>().ReverseMap();
            CreateMap<Author, AuthorVM>().ReverseMap();
            CreateMap<Author, UpdateAuthorDTO>().ReverseMap();

            CreateMap<Genre, GenreDTO>().ReverseMap();
        }
    }
}
