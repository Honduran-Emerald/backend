using AspNetCore.Identity.Mongo;
using Emerald.Application.Infrastructure;
using Emerald.Application.Infrastructure.ActionFilter;
using Emerald.Application.Infrastructure.Middleware;
using Emerald.Application.Infrastructure.OperationFilter;
using Emerald.Application.Models.Quest.Component;
using Emerald.Application.Models.Quest.Module;
using Emerald.Application.Services;
using Emerald.Application.Services.Factories;
using Emerald.Domain.Models.UserAggregate;
using Emerald.Domain.Repositories;
using Emerald.Domain.Services;
using Emerald.Infrastructure;
using Emerald.Infrastructure.Repositories;
using Emerald.Infrastructure.Services;
using Emerald.Infrastructure.ViewModelStash;
using Expo.Server.Client;
using FirebaseAdmin;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Emerald.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<QuestPlayService>()
                    .AddScoped<QuestCreateService>();

            services.AddScoped<ComponentModelFactory>()
                    .AddScoped<MementoModelFactory>()
                    .AddScoped<ModuleModelFactory>()
                    .AddScoped<UserModelFactory>()
                    .AddScoped<QuestModelFactory>()
                    .AddScoped<ResponseEventModelFactory>()
                    .AddScoped<TrackerModelFactory>()
                    .AddScoped<TrackerNodeModelFactory>()
                    .AddScoped<QuestPrototypeModelFactory>()
                    .AddScoped<ChatMessageModelFactory>()
                    .AddScoped<ChatModelFactory>();

            services.AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IModuleRepository, ModuleRepository>()
                    .AddScoped<IComponentRepository, ComponentRepository>()
                    .AddScoped<IQuestRepository, QuestRepository>()
                    .AddScoped<ITrackerRepository, TrackerRepository>()
                    .AddScoped<IQuestPrototypeRepository, QuestPrototypeRepository>()
                    .AddScoped<IImageIndexService, ImageIndexService>()
                    .AddScoped<IChatMessageRepository, ChatMessageRepository>()
                    .AddScoped<IChatRepository, ChatRepository>();

            services.AddScoped<QuestViewModelStash>();

            services.AddScoped<IJwtAuthentication, JwtAuthentication>()
                    .AddScoped<IUserService, UserService>()
                    .AddSingleton<IMongoDbContext, MongoDbContext>()
                    .AddSingleton<IImageService, ImageService>();

            if (env.IsProduction())
            {
                services.AddSingleton<ISafeSearchService, SafeSearchService>();

                services.AddSingleton<IMessagingService, ExpoMessagingService>()
                        .AddSingleton<PushApiClient>();

                // services.AddSingleton<IMessagingService, FirebaseMessagingService>()
                //         .AddSingleton(FirebaseApp.Create());
            }
            else
            {
                services.AddSingleton<ISafeSearchService, DevelopmentSafeSearchService>();
                services.AddSingleton<IMessagingService, DevelopmentMessagingService>();
            }

            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        options.SerializerSettings.Converters.Add(new ObjectIdJsonConverter());
                        options.SerializerSettings.AddComponentPrototypeConverter();
                        options.SerializerSettings.AddModulePrototypeConverter();
                    });

            services.AddMediatR(
                Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                    .Select(a => Assembly.Load(a))
                    .Concat(new[] { Assembly.GetExecutingAssembly() })
                    .ToArray());

            services
                .AddSwaggerGenNewtonsoftSupport()
                .AddSwaggerGen(options =>
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
                options.IncludeXmlComments(xmlPath, true);

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
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "";
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            if (env.IsDevelopment() == false)
            {
                services.AddScoped(context => Logger.Factory.Get())
                        .AddLogging(logging => logging.AddKissLog());
            }
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            logger.LogCritical(env.EnvironmentName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseKissLogMiddleware(options =>
            {
                options.InternalLog = message => Debug.WriteLine(message);
                options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(
                    Configuration["KissLog.OrganizationId"],
                    Configuration["KissLog.ApplicationId"]))
                {
                    ApiUrl = Configuration["KissLog.ApiUrl"]
                });
            });

            app.UseUserValidation();
            app.UseQuestSyncMiddleware();
            app.UseDomainExceptionHandlerMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Honduran Emerald");
            });
        }
    }
}
