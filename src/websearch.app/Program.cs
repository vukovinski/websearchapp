var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc(opt => opt.EnableEndpointRouting = false);
builder.Services.AddTransient<FrontierDbContextFactory>();
builder.Services.AddTransient<GoogleDataService>();
builder.Services.AddTransient<DataService>();
builder.Services.AddGoogleApiClients();
builder.Services.AddCors(opt => 
{
    opt.AddPolicy("APP_API_PAIR", p =>
    {
        p.AllowAnyMethod()
         .AllowAnyHeader()
         .SetIsOriginAllowed(o => o == "https://localhost:44481");
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMvc();
app.UseCors("APP_API_PAIR");
app.MapFallbackToFile("index.html");

app.Run();
