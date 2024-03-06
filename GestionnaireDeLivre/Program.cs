using GestionnaireDeLivre;
using GestionnaireDeLivre.Model;
using GestionnaireDeLivre.Repositorys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(Repository<>));
builder.Services.AddScoped(typeof(AppDbContext));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Pour les API
     });



var serviceProvider = new ServiceCollection()
    .AddDbContext<AppDbContext>()
    .BuildServiceProvider();

// Obtention de l'instance de AppDbContext
var context = serviceProvider.GetService<AppDbContext>();
var seeder = new DataSeeder(context);
seeder.Seed();

// Après le seeding
var tester = new DataTester(context);
tester.TestSeeding();


app.Run();
