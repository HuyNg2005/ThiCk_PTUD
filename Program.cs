
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLyNhaHang.Data;
using QuanLyNhaHang.Entities;
using QuanLyNhaHang.Repository;
using QuanLyNhaHang.Service.User;
using System.Text;
using QuanLyNhaHang.Service.Booking;
using QuanLyNhaHang.Service.BookingDetail;
using QuanLyNhaHang.Service.BookingDetailService;
using QuanLyNhaHang.Service.Customer;
using QuanLyNhaHang.Service.Room;
using QuanLyNhaHang.Service.ServiceHotel;

namespace QuanLyNhaHang
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           

            //builder.Services.AddScoped<IPasswordHasher< UserEntity>,IPasswordHasher<UserEntity>>();
            builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();

            // Add Generic Repository
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //service
            builder.Services.AddScoped<IBookingDetailServiceService, BookingDetailServiceService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IBookingservice, BookingService>();
            builder.Services.AddScoped<IBookingDetailService, BookingDetailService>();
            builder.Services.AddScoped<IServiceHotelService, ServiceHotelService>();
          
            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.Configure<IISServerOptions>(options => { options.MaxRequestBodySize = int.MaxValue; });
            //http
            builder.Services.AddHttpContextAccessor();

            //AutopMapper
            builder.Services.AddAutoMapper(typeof(Program));

            //JWT config
            var jwtSetting = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSetting["Secret"]);
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSetting["Issuer"],
                        ValidAudience = jwtSetting["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
            builder.Services.AddAuthorization();//phan quyen

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "TodoApp",
                        Version = "v1"
                    });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer {Token}'"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();
     

            app.MapControllers();

            app.Run();
        }
    }
}
