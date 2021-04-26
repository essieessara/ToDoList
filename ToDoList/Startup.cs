using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToDoList.Repositories;
using ToDoList.Repositories.ToDoItemRepos;
using ToDoList.Repositories.UserRepos;
using ToDoList.Services.AccountMangmentService;
using ToDoList.Services.DataManagementService;
using ToDoList.Services.ToDoServices;
using ToDoList.Services.UserServices;

namespace ToDoList
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoListContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<ToDoListContext>(options =>
            //options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDoList", Version = "v1" });
            });


            services.AddScoped<IToDoItemRepo, ToDoItemDapperRepo>();
            services.AddScoped<IToDoItemService, ToDoItemService>();
            services.AddScoped<IUserRepo, UserDapperRepo>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountManagmentService, AccountManagmentService>();
            services.AddScoped<IDataManagementService, DataManagementService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
