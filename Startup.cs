using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CPopeWebsite.Areas.Identity;
using CPopeWebsite.Data;
using CPopeWebsite.Data.Blog;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CPopeWebsite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton(m =>
            {
                SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
                sql.Open();

                return sql;
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;

                options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri)
                    };
                });
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", c => c.RequireRole("Admin"));
            });



            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddSingleton<BlogPostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var db = new ApplicationDbContext(identityDbContextOptions))
            {
                db.Database.EnsureCreated();
            }

            var admin = userManager.FindByEmailAsync("c.popelewis@gmail.com").GetAwaiter().GetResult();
            if (admin == null)
            {
                userManager.CreateAsync(new IdentityUser("c.popelewis@gmail.com") { Email = "c.popelewis@gmail.com", EmailConfirmed = true }, "1234").GetAwaiter().GetResult();
                admin = userManager.FindByEmailAsync("c.popelewis@gmail.com").GetAwaiter().GetResult();
            }

            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            }

            if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
            {
                userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
