using System.Globalization;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Dima.Web;
using Dima.Web.Handlers;
using Dima.Web.Security;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? String.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CookieHandler>();
builder.Services.AddApiAuthorization();
builder.Services.AddMudServices();

builder.Services.AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services
    .AddHttpClient(Configuration.HttpClientName, options => options.BaseAddress = new Uri(Configuration.BackendUrl))
    .AddHttpMessageHandler<CookieHandler>();

builder.Services.AddTransient<IAccountHandler, AccountHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<IReportsHandler, ReportHandler>();

// Força a cultura padrão da aplicação para pt-BR,
// afetando idioma (UI), formatação de datas, números e moeda (R$)
// <BlazorWebAssemblyLoadAllGlobalizationData>true</BlazorWebAssemblyLoadAllGlobalizationData> -> abilitar no .csproj
builder.Services.AddLocalization();
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");

await builder.Build().RunAsync();
