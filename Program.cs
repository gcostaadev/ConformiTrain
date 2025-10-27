using ConformiTrain.ExceptionHandler;
using ConformiTrain.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();


//builder.Services.AddDbContext<ConformiTrainDbContext>(o => o.UseInMemoryDatabase("ConformiTrainDb"));
var connectionString = builder.Configuration.GetConnectionString("ConformiTrain");
builder.Services.AddDbContext<ConformiTrainDbContext>(o =>o.UseOracle(connectionString));


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

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Tenta obter o DbContext
        var context = services.GetRequiredService<ConformiTrain.Persistence.ConformiTrainDbContext>();

        // Aplica quaisquer migrations pendentes.
        // Cria a base de dados se ela n�o existir.
        context.Database.Migrate();

        Console.WriteLine("----> Migrations aplicadas com sucesso."); // Log para confirma��o
    }
    catch (Exception ex)
    {
        // Regista qualquer erro durante a migra��o
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "----> Ocorreu um erro durante a aplica��o das migrations.");
        // Considerar parar a aplica��o se as migrations falharem: throw;
    }
}

app.Run();
