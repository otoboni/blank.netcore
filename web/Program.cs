using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Equivalente ao ConfigureServices ---
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- 2. Equivalente ao Configure (Middleware Pipeline) ---

// Configuração de ambiente
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // Note que o UsePathBase deve vir cedo no pipeline
    app.UsePathBase("/guarder");
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Cabeçalhos encaminhados (Proxy/Load Balancers)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapeamento de rotas (substitui o UseEndpoints)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();