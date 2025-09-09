using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Container
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IReservationDal, EfReservationDal>();
            services.AddScoped<IReservationService, ReservationManager>();

            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IDestinationDal, EfDestinationDal>();
            services.AddScoped<IDestinationService, DestinationManager>();

            services.AddScoped<IAppUserDal, EfAppUserDal>();
            services.AddScoped<IAppUserService, AppUserManager>();
        }
    }
}
