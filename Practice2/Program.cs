using Serilog;
using UPB.BusinessLogic.Managers;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetSection("Logging").GetSection("FilePaths").GetSection("LogPath").Value)
    .CreateLogger();

Log.Information("Initialazing the server");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<PatientManager>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = builder.Configuration.GetSection("Logging").GetSection("Strings").GetSection("SwaggerTitle").Value
    }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (app.Environment.IsEnvironment("QA"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
