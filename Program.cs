using Ap1_P1_JairoCamilo;
using Ap1_P1_JairoCamilo.Components;
using Ap1_P1_JairoCamilo.DAL;
using Ap1_P1_JairoCamilo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//Inyección del contexto
var ConStr = builder.Configuration.GetConnectionString("ConStr");
builder.Services.AddDbContext<Contexto>(c => c.UseSqlite(ConStr));

//Inyección de bootstrap
builder.Services.AddBlazorBootstrap();

//Inyección del service
builder.Services.AddScoped<ArticulosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
