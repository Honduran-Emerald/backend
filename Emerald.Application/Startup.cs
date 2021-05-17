using AspNetCore.Identity.Mongo;
using Emerald.Application.Infrastructure.ActionFilter;
using Emerald.Application.Infrastructure.JsonConverter;
using Emerald.Application.Infrastructure.OperationFilter;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.Services;
using Emerald.Infrastructure;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.ViewModelHandlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Emerald.Application
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
            services.AddScoped<QuestPlayService>();

            services.AddScoped<IComponentModelFactory, ComponentModelFactory>()
                    .AddScoped<IMementoModelFactory, MementoModelFactory>()
                    .AddScoped<IModuleModelFactory, ModuleModuleFactory>()
                    .AddScoped<IQuestModelFactory, QuestModelFactory>()
                    .AddScoped<IResponseEventModelFactory, ResponseEventModelFactory>();

            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IModuleRepository, ModuleRepository>()
                    .AddScoped<IComponentRepository, ComponentRepository>()
                    .AddScoped<IQuestRepository, QuestRepository>()
                    .AddScoped<ITrackerRepository, TrackerRepository>();

            services.AddScoped<QuestViewModelStash>();

            services.AddScoped<IJwtAuthentication, JwtAuthentication>()
                    .AddSingleton<IMongoDbContext, MongoDbContext>();

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.Converters.Add(new ObjectIdJsonConverter());
                    });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Honduran Emerald",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using Bearer scheme.",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.OperationFilter<AuthorizeCheckOperationFilter>();
                options.OperationFilter<SyncTokenOperationFilter>();

                options.GeneratePolymorphicSchemas();
            });

            services.AddIdentityMongoDbProvider<User>(options =>
                options.ConnectionString = Configuration["Mongo:ConnectionString"]);

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services
                .Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogCritical(env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseQuestSyncMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Honduran Emerald");
            });
        }
    }
}
