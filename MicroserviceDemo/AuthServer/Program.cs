
using AuthService.Infrastructure;
using AuthService.Infrastructure.DB;
using AuthService.Infrastructure.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// add db context for ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// configure identity in infrastructure
builder.Services.AddIdentityInfrastructure(builder.Configuration);


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

// Seed data
IdentityServerSeed.InitializeIdentityServerConfiguration(app.Services);
RoleSeeding.InitializeRoleConfiguration(app.Services);

//app.MapIdentityApi<IdentityUser>(); // api endpoints for identity
app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthentication();
app.UseIdentityServer();

app.UseAuthorization();

app.MapControllers();

app.Run();
