using AbTest.Data.Db;
using AbTest.RequestHandlers;
using AbTest.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
  
builder.Services.AddDbContext<AbTestDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("experiment-db")).EnableSensitiveDataLogging());
builder.Services.AddScoped<ApplicationRepository>();
builder.Services.AddTransient<ExperimentService>();

builder.Services.AddMvc();
builder.Services.AddTransient<ButtonColorHandler>();
builder.Services.AddTransient<PriceHandler>();
builder.Services.AddTransient<ExperimentsReportHandler>();

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
