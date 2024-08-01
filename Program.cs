using WagerMate.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Middleware to redirect to /login if the root URL is accessed
bool checklogin = false;
if (checklogin == false) {
    app.Use(async (context, next) => {

        if (context.Request.Path == "/") {
            context.Response.Redirect("/login");
        }
        else {
            await next.Invoke();
        }

    });
}
//set cecklogin to false to land on the loginpage

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();