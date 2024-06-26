using JWTService.Application.Interfaces;
using JWTService.Infrastructure.Extensions;
using JWTService.Application.Implementations;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var conn = builder.Configuration.GetSection("ConnectionStrings:Default").Value;
builder.Services.AddInfrastructureService(conn);
builder.Services.AddScoped<IUserAction, UserAction>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
