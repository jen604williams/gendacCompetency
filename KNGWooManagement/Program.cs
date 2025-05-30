var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//get apiKey from appsettings
var apiKey = builder.Configuration["ApiSettings:ApiKey"];

//API calls: register HttpClient 
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://assessments.gendac.co.za");
});

builder.Services.AddScoped<OrdersService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
