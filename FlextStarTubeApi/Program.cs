using FlextStarTubeApi;
using FlextStarTubeApi.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

 void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    // Configuração do CORS
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });
    services.AddSwaggerGen(); // Configuração do Swagger, se necessário
}


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Adiciona serviços ao contêiner de injeção de dependência.
builder.Services.AddControllers();

// Configuração do DbContext com MySQL (ou Pomelo.EntityFrameworkCore.MySql)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21)))); // Verifique a versão correta do MySQL

// Adiciona o Swagger para documentar sua API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o pipeline de requisição HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexStarTube API v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

