using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VolksmondAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VolksmondAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VolksmondAPIContext") ?? throw new InvalidOperationException("Connection string 'VolksmondAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<VolksmondAPIContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}
app.UseCors(
        options => options.WithOrigins("http://localhost:3000").AllowAnyMethod()
    );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
