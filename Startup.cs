using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Service;
using MyCompany.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MyCompany
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
       //����������� ��������
        public void ConfigureServices(IServiceCollection services)
        {
            //���������� ������ �� appsettings.json
            Configuration.Bind("Project", new Config());
            //��������� ��������� ������������ � �������������(MVC).
            services.AddControllersWithViews(x=>
            {
                x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea")); 
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
            //������ ������� ����: ������ ������������� � asp.net core 3.0

            //���������� ������ ���������� ���������� � �������� ��������
            services.AddTransient<ITextFieldsRepository, EFTextFieldsRepository>();
            services.AddTransient<IServiceItemsRepository, EFServiceItemsRepository>();
            services.AddTransient<IVehicleFleetItemsRepository, EFVehicleFleetItemsRepository>();
            services.AddTransient<DataManager>();

            //����������� �������� ��
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString));

            //����������� Identity ������� 
            services.AddIdentity<IdentityUser, IdentityRole>(opts => 
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //����������� authentication cookie
            services.ConfigureApplicationCookie(options => 
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            //����������� �������� ����������� ��� Admin area
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //������� ����������� middleware ����� �����!!!

            //� �������� ���������� ��� ����� ������ ��������� ���������� �� �������.
            if (env.IsDevelopment())
                    app.UseDeveloperExceptionPage();
            
 
            //���������� ��������� ��������� ������ � ���������� (css, js � �.�.)
            app.UseStaticFiles();
            //���������� ������� ������������� 
            app.UseRouting();

            //���������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //������������ ������ �������� (���������)
            app.UseEndpoints(endpoints =>
            {
                //����� ������� ��� ������ ������.
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                  endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");      
            });
        }
    }
}
