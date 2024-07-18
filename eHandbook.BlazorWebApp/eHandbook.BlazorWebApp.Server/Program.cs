using eHandbook.BlazorWebApp.Server.Components;
using eHandbook.BlazorWebApp.Server.Services;
using eHandbook.BlazorWebApp.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<ManualStateService>();


// Register HttpClient to be used for server-side HTTP calls
builder.Services.AddHttpClient<EHandBookApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDevBaseUrl").Value!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseBlazorFrameworkFiles();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(eHandbook.BlazorWebApp.Client._Imports).Assembly);

app.Run();
