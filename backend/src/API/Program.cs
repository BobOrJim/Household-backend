using API.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<HouseholdDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// For Entity Framework for identity
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

// For Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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
app.MapControllers();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HouseholdDbContext>();
    
        //Jimmy: 221026. Har lite trassel vid övergång till riktig db, så jag har tmp inaktiverat här
        //await SeedHouseholdDbContext.SeedAsync(context);
}


app.Run();




