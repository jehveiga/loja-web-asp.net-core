using AutoMapper;
using LojaGeek.App.Configurations;
using LojaGeekWeb.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LojaGeek.App.Data;

// O builder � respons�vel por fornecer os m�todos de controle
// dos servi�os e demais funcionalidades na configura��o da App
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


// Daqui para baixo � conte�do que vinha dentro do m�todo
// ConfigureServices() na antiga Startup.cs
// Nesta �rea adicionamos servi�os ao pipeline
// ConfigureServices
builder.Services.AddIdentityConfiguration(builder.Configuration);

// Adicionando a tela de erro de banco de dados (para desenvolvimento)
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Add o servi�o de contexto da camada de Dados
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<LojaGeekDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Essa � a nova forma de adicionar o MVC ao projeto
// N�o se usa mais services.AddMvc();
builder.Services.AddControllersWithViews();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Adicionando o servi�o do AutoMapper a cole��o de servi�os da Aplica��o, ir� ter refer�ncia pelo assembly  
// procurando a heran�a do perfil de mapeamento 'Profile'

builder.Services.AddMvcConfiguration();

builder.Services.ResolveDependencies();

// Gerando a APP
var app = builder.Build();

// Daqui para baixo � conte�do que vinha dentro do m�todo Configure() na antiga Startup.cs
// Aqui se configura comportamentos do request dentro do pipeline

// Configura��o conforme os ambientes
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();

}

// Redirecionamento para HTTPs
app.UseHttpsRedirection();

// Uso de arquivos est�ticos (ex. CSS, JS)
app.UseStaticFiles();

// Adicionando suporte a rota
app.UseRouting();

// Autenticacao e autoriza��o (Identity)
app.UseAuthentication();
app.UseAuthorization();


// Adicionando a extens�o do m�todo de configura��o da globaliza��o local escolhida na aplica��o
app.UseGlobalizationConfig();

// Note a ligeira mudan�a na declara��o da rota padr�o 
// No caso de precisar mapear mais de uma rota duplique o c�digo abaixo
// Rota padr�o
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

// Adicionando suporte a componentes Razor (ex. Telas do Identity)
builder.Services.AddRazorPages();

// Essa linha precisa sempre ficar por �ltimo na configura��o do request
app.Run();


