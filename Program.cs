using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using World.Api.Common;
using World.Api.Data;
using World.Api.Repository;
using World.Api.Repository.IRepository;


var builder = WebApplication.CreateBuilder(args);


#region Register JWT token

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

#endregion

// Add services to the container.

#region Configure CORS

builder.Services.AddCors(options =>
{
options.AddPolicy("CustomPolicy",x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

#endregion

#region Connection string

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options=>options.UseSqlServer(connectionString));

#endregion

#region configure Automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region DI configuration
builder.Services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddTransient<ICountryRepository, CountryRepository>();
builder.Services.AddTransient<IStatesRepository, StatesRepository>();
#endregion

#region configure Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.File("Logs/Log.txt",rollingInterval:RollingInterval.Day);
    if(context.HostingEnvironment.IsProduction() == false)
    {
        config.WriteTo.Console();
    }
});
#endregion

#region Configure Authentication
//var = _jwtsetting = Configuration.GetSection("JWTSetting");
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CustomPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
