using Aplicacao.Aplicacoes;
using Aplicacao.Interfaces;
using Dominio.Interfaces;
using Dominio.Interfaces.Genericos;
using Dominio.Interfaces.InterfacesDeServicos;
using Dominio.Servicos;
using Infraestrutura.Configuracoes;
using Infraestrutura.Repositorio;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using WebAPIClientes.AuthToken;

namespace WebAPIClientes
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
            services.AddDbContext<ContextoClientesPersonalite>(options =>
            {
                if (!options.IsConfigured)
                    options.UseSqlServer(Configuration.GetConnectionString("DB_Clientes_Personalite"));
            });

            services.AddHttpClient();

            services.AddSingleton(Configuration);

            //Adicao dos Singletons com Interfaces e Repositorios
            services.AddTransient(typeof(IGenericos<>), typeof(RepositorioGenerico<>));
            services.AddTransient<ICliente, RepositorioCliente>();

            //Adicao dos Singletons com Interfaces e Servicos
            services.AddTransient<IServicoCliente, ServicoCliente>();
            services.AddTransient<IServicoHttp, ServicoHttp>();

            //Adicao dos Singletons com Interfaces e Aplicacoes
            services.AddTransient<IAplicacaoCliente, AplicacaoCliente>();

            services.AddControllers();

            ConfigureAuthentication(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exame Asp.Net core + SQL server -> API de Clientes", Version = "v1" });
            });
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(option =>
                        {
                            option.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = false,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,

                                ValidIssuer = Configuration.GetSection("AutenticacaoConfig:TokenIssuer").Value,
                                IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetSection("AutenticacaoConfig:TokenSecret").Value)
                            };

                            option.Events = new JwtBearerEvents
                            {
                                OnAuthenticationFailed = context =>
                                {
                                    Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                                    return Task.CompletedTask;
                                },
                                OnTokenValidated = context =>
                                {
                                    Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                                    return Task.CompletedTask;
                                }
                            };
                        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exame Asp.Net core + SQL server -> API de usuário v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
