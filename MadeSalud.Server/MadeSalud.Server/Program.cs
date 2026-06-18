using MadeSalud.BD.Datos;
using MadeSalud.Repositorio.IRepositorios;
using MadeSalud.Repositorio.Repositorios;
using MadeSalud.Server.Components;
using MadeSalud.Server.Components.Account;
using MadeSalud.Servicio.ServiciosHttp;
using MadeSalud.Shared.ENUM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache(options =>
{
   options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();




builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MadeSalud API",
        Version = "v1",
        Description = "API de MadeSalud",
    });
});


var connectionString = builder.Configuration.GetConnectionString("ConSqlServer") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IPacienteRepositorio, PacienteRepositorio>();
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IMedicoRepositorio, MedicoRepositorio>();
builder.Services.AddScoped<ISecretariaRepositorio, SecretariaRepositorio>();
builder.Services.AddScoped<IMedicamentoRepositorio, MedicamentoRepositorio>();

builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();

    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});

builder.Services.AddScoped<IHttpServicio, HttpServicio>();


builder.Services.AddIdentityCore<MiUsuario>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })

    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<MiUsuario>, IdentityNoOpEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MadeSalud API V1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.UseOutputCache();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MadeSalud.Server.Client._Imports).Assembly);

app.MapAdditionalIdentityEndpoints();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<MiUsuario>>();

    foreach (var rol in Enum.GetNames(typeof(RolEnum)))
    {
        if (!await roleManager.RoleExistsAsync(rol))
        {
            await roleManager.CreateAsync(new IdentityRole(rol));
        }
    }

    var emailAdmin = "emilseferrer@hotmail.com";

    var usuario = await userManager.FindByEmailAsync(emailAdmin);

    if (usuario != null)
    {
        usuario.EmailConfirmed = true;
        await userManager.UpdateAsync(usuario);

        if (!await userManager.IsInRoleAsync(usuario, "Secretaria"))
        {
            await userManager.AddToRoleAsync(usuario, "Secretaria");
        }
    }
}

app.Run();