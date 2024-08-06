using Demo.BLL.Mapper;
using Demo.BLL.Service;
using Demo.DAL.Database;
using Microsoft.EntityFrameworkCore;

namespace Demo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            #region Connection String Service

            // Enhancement ConnectionString
            // builder.Configuration. : كده دخل ملف appsetting.json
            // كده مسك بيانات connnection string بس
            var connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");

            // open connection with database
            builder.Services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));

            #endregion



            // service in momery
            //builder.Services.AddSingleton<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped(typeof(IGenericRepossitory<>), typeof(GenericRepossitory<>));
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();



            #region Auto Mapper Service

            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            #endregion


            #endregion


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
