using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using HS12_BlogProject.Application.AutoMapper;
using HS12_BlogProject.Application.Services.AppUserService;
using HS12_BlogProject.Application.Services.AuthorService;
using HS12_BlogProject.Application.Services.GenreService;
using HS12_BlogProject.Application.Services.PostService;
using HS12_BlogProject.Domain.Repositories;
using HS12_BlogProject.Infrastructure.Repositories;

namespace HS12_BlogProject.Application.IoC
{
    public class DependencyResolver : Module  //System.Reflection dan geleni almıyoruz. Autofac ten geleni alıyoruz.
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerDependency();

            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerLifetimeScope();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerDependency();

            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerDependency();

            builder.RegisterType<AppUserService>().As<IAppUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AppUserRepository>().As<IAppUserRepository>().InstancePerDependency();

            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerDependency();

            builder.RegisterType<Mapper>().As<Mapper>().InstancePerLifetimeScope();

            #region AutoMapper
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                //Register Mapper Profile
                cfg.AddProfile<Mapping>(); /// AutoMapper klasörünün altına eklediğimiz Mapping classını bağlıyoruz.
            }
            )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
			//
			//builder.Register(context =>
			//{
			//	// Bu, AutoMapper yapılandırmasını yapar.
			//	var config = new MapperConfiguration(cfg =>
			//	{
			//		// AutoMapper profillerini kaydet
			//		cfg.AddProfile<Mapping>();
			//	});
			//	return config;
			//}).AsSelf().SingleInstance();

			//builder.Register(c =>
			//{
			//	// Bu, IMapper'ı oluşturur ve yapılandırmayı kullanır.
			//	var context = c.Resolve<IComponentContext>();
			//	var config = context.Resolve<MapperConfiguration>();
			//	return config.CreateMapper(context.Resolve);
			//}).As<IMapper>().InstancePerLifetimeScope();

			#endregion

			base.Load(builder);
        }
    }
}
