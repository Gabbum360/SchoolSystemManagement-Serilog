using Authentication_Layer.Auth_Models;
using Business_Logic_Layer.Interfaces;
using BusinessLogicLayer;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Models;
using Business_Logic_Layer;
using Infrastructure.Interfaces;
using Infrastructure.ThirdParty;
using Business_Logic_Layer.Student_Logics.Commands;
using Business_Logic_Layer.Student_Logics.Queries;

namespace SMS_Seperation_Of_Concerns
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
            //Student std = new Student("", "Gabriel", 34, "male", "China");
            
            services.AddDbContext<SchoolSystemDbContext>(X => X.UseSqlServer(Configuration.GetConnectionString("CoreServiceDb")));
            // services.AddResponseCaching();
            services.AddControllers();
            services.AddRazorPages();
            //another pattern of the neXt line is already used...
            /*            services.AddAuthentication("BasicAuthentication").AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            */

            //the following services adds the interfaces to the project where its required and used...
            services.AddScoped<IGetStudents, GetStudentsQuery>();
            services.AddScoped<IGetStudent, GetStudentByIdQuery>();
            services.AddScoped<IRegister, RegisterStudentCommand>();
            services.AddScoped<IUpdate, UpdateStudentCommand>();
            services.AddScoped<IDelete, DeleteStudentByIdCommand>();
            /*services.AddScoped<IGetStudents, GetStudentsQuery>();
            services.AddScoped<IGetStudent, GetStudentByIdQuery>();
            services.AddScoped<IRegister, RegisterStudentCommand>();
            services.AddScoped<IUpdate, UpdateStudentCommand>();
            services.AddScoped<IDelete, DeleteStudentByIdCommand>();*/
            services.AddScoped<ITeacher, Teachers_Logic>();
            services.AddScoped<ISchoolClass, SchoolClass_Logic>();
            services.AddScoped<IHttpService, HttpServices>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SMS_Seperation_Of_Concerns", Version = "v1" });
            });

            //AddCors for Policy
            services.AddCors(X =>
            {
                X.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());


            });
            //for identity
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<SchoolSystemDbContext>()
                .AddDefaultTokenProviders();

            //adding authentication
            services.AddAuthentication(X =>
            {
                X.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                X.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            //Adding Jwt Bearer
            .AddJwtBearer(X =>
            {
                X.SaveToken = true;
                X.RequireHttpsMetadata = false;
                X.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudiece"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:IssuerSigningKey"]))
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SMS_Seperation_Of_Concerns v1"));
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                /*endpoints.MapRazorPages();*/
            });
        }
    }
}
