using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraversalCoreProject.Models; // CustomIdentityValidator için eklendi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BusinessLayer.Concrete;
using BusinessLayer.Abstract;

namespace TraversalCoreProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // bu method, uygulamanin servislerini yapilandirmak icin kullanilir.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(); // burada amac identity yapilandirmasini tanimlamak hemde proje seviyesinde bir authentication uygulamak

            // identity yapilandirmasi eklendi
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 6; // Minimum karakter sayýsý
                options.Password.RequireNonAlphanumeric = true; // Özel karakter zorunlu
                options.Password.RequireDigit = true; // Rakam zorunlu
                options.Password.RequireLowercase = true; // Küçük harf zorunlu
                options.Password.RequireUppercase = true; // Büyük harf zorunlu
            })
            .AddEntityFrameworkStores<Context>()
            .AddErrorDescriber<CustomIdentityValidator>() // Türkçe hata mesajlarý
            .AddDefaultTokenProviders();

            // cookie ayarlari (login olmayanlari login sayfasina gondermek icin)
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/SignIn/"; // kullanici giris yapmadiysa buraya gidecek
                options.AccessDeniedPath = "/Error/AccessDenied"; // yetki yoksa // yetki suan hic yok.. ekleneccek..
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));

            }); // kullanici authorize olmak icin mutlaka giris yapsin

            services.AddMvc();
            services.AddControllersWithViews();

            services.AddScoped<IReservationDal, EfReservationDal>();
            services.AddScoped<IReservationService, ReservationManager>();

            services.AddScoped<ICommentDal, EfCommentDal>();
            services.AddScoped<ICommentService, CommentManager>();
        }

        // bu method, HTTP request pipeline'i yapilandirmak icin kullanilir.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // varsayilan olarak HSTS (HTTP Strict Transport Security) kullanilir
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // kimlik dogrulama
            app.UseAuthorization();  // Yetkilendirme

            app.UseEndpoints(endpoints => //her sinifin kullanabilecegi endpointler tanimlanir
            {
                // Area route önce gelmeli
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                // Default route
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseStaticFiles();
        }
    }
}
