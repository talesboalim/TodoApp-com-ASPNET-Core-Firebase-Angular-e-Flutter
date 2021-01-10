using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

namespace Todo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));

            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));

            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/curstobalta-7196";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/curstobalta-7196",
                        ValidateAudience = true,
                        ValidAudience = "curstobalta-7196",
                        ValidateLifetime = true
                    };
                });
            //   var firebaseConfig = {
            //     apiKey: "AIzaSyASt9Ynme00TiNmgn9SXg-pXKLVuud9cOM",
            //     authDomain: "curstobalta-7196.firebaseapp.com",
            //     projectId: "curstobalta-7196",
            //     storageBucket: "curstobalta-7196.appspot.com",
            //     messagingSenderId: "648277883864",
            //     appId: "1:648277883864:web:15ed058055b024ff1f50ad"
            //   };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
