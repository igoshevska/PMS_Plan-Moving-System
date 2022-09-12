using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PMS.Configuration;
using PMS.Data;
using PMS.Data.Repositories;
using PMS.Domain;
using PMS.Services.Implementation;
using PMS.Services.Interface;
using System.Text;

namespace MoveIT
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

            services.AddCors(options => options.AddPolicy("CorsPolicy", p => p.AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()
                                                                            ));

            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = System.TimeSpan.FromDays(365);
            }
);
            services.AddCors();
            
            
            services.AddControllers();

            services.AddScoped<IProposalAndOrder, ProposalAndOrderService>();
            services.AddScoped<IAccount, AccountService>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Role>, Repository<Role>>();
            services.AddScoped<IRepository<PriceProposal>, Repository<PriceProposal>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<Proposal>, Repository<Proposal>>();
            services.AddScoped<IRepository<Proposal>, Repository<Proposal>>();

            services.AddDbContext<DataContext>(options => { });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(AutoMapperConfiguration.Initialize());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:Issuer"],
                    ValidAudience = Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
   

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=OpenApi}/{action=VerifyConnection}").WithMetadata(new AllowAnonymousAttribute());

            });
        }
    }
}
