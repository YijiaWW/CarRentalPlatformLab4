using Maintenance.WebAPI.Middleware;
using Maintenance.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register service
builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.MapGet("/", () => "Maintenance.WebAPI is running!");

app.Run();
