using MQTTnet.AspNetCore;
using Karluna.Business.ServerSide.TCU.Servers.Grpc.Services;
using Karluna.Business.ServerSide.TCU.Servers.MQTT.Broker;
using MQTTnet;
using MQTTnet.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpLogging;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Karluna.Business.Domain.Mqtt;
using Karluna.Business.Client.MQTT.Subscriber;
using Microsoft.EntityFrameworkCore;
using Karluna.Data.DbContext;
using Microsoft.AspNetCore.Identity;
using System;
using Karluna.Data.DbContext.DbServices;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Karluna.Business.ServerSide.Management;
using Karluna.DAL.Interface;
using Karluna.DAL.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Karluna.Entities.Entities;

namespace Karluna.API
{
    public class StartUp
    {//Generic host document. All services configure in one class
        Broker _broker;
        
        public IConfiguration configRoot
        {
            get;
        }
        public StartUp(IConfiguration configuration)
        {
            configRoot = configuration;
            _broker = new Broker();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var corsEndpoint = configRoot.GetValue<string>("CorsEndpoint");

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddTransient<IRepStockBrand, RepStockBrand>();
            services.AddTransient<IRepStockProduct, RepStockProduct>();
            services.AddTransient<IRepStockProductVersion, RepStockProductVersion>();
            services.AddTransient<IRepStockSubCategory, RepStockSubCategory>();
            services.AddTransient<IRepStockCategory, RepStockCategory>();

            services.AddScoped<KtsDbContext>();
            services.AddDbContext<KtsDbContext>(options =>
                options.UseNpgsql(configRoot.GetConnectionString("PostgresConnectionString"))
            );
            services.AddGrpc();
            services.AddIdentity<User, UserRole>()
            .AddEntityFrameworkStores<KtsDbContext>()
            .AddDefaultTokenProviders();
            //Connections with tcp. Certificate can use in communication
            services.AddMqttServer(server => _broker.CreateMqttServer()).AddMqttConnectionHandler();
            services.AddControllers();
            services.AddHostedService<MigrationService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("stock", new OpenApiInfo { Title = "Stock" });
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configRoot["JWT:Secret"])),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    
                    //ValidateIssuer = true, // true olarak değiştirildi
                    //ValidateAudience = true, // true olarak değiştirildi
                    //ValidateLifetime = true,
                    //ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configRoot["JWT:Secret"])),
                    //ClockSkew = TimeSpan.Zero,
                };
            });
            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                 .AllowAnyMethod()
                                 .AllowAnyHeader()
                                 .WithExposedHeaders("Access-Control-Allow-Origin");
                    });
            });

            //services.AddSingleton<IHostedService, Subscriber>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            //MqttManagement.Create().ConfigureMqttServer();
            //var services = app.ApplicationServices;
            //var services = app.ApplicationServices;
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<TCUServer>();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/karluna/swagger.json", "Karluna");
            });

            app.UseMqttServer(server => {
                server.InterceptingPublishAsync += _broker.interceptingPublishEvent;
                server.InterceptingSubscriptionAsync += _broker.interceptingSubscriptionEvent;
            });
        }
    }
}
