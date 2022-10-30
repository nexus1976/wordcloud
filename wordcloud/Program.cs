using Microsoft.EntityFrameworkCore;
using wordcloud.Data;
using wordcloud.Domain;
using wordcloud.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
string? commandConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Command");
if (string.IsNullOrWhiteSpace(commandConnectionString))
    throw new Exception("The command connection string could not be found.");
builder.Services.AddDbContext<CommandContext>(options => options.UseNpgsql(commandConnectionString));
builder.Services.AddScoped<ICommandContext, CommandContext>();
builder.Services.AddSingleton<IModelMappingService, ModelMappingService>();
builder.Services.AddScoped<IDomainServices, DomainServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
