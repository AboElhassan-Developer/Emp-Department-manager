using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using ProjectMVC.Models;
using ProjectMVC.Repository;

namespace ProjectMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<MVCContext>(options=>
            options.UseSqlServer(builder.Configuration.GetConnectionString("cs"))
            );

           




            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option=>
            {

                option.Password.RequiredLength = 4;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
            }
            )
                .AddEntityFrameworkStores<MVCContext>();

            //Custom Service "Register"
            builder.Services.AddScoped<IDepartmentRepositroy,DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>();
             builder.Services.AddScoped<IProjectRepository, ProjectRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
