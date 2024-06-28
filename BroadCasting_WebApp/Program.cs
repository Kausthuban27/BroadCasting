using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorStrap;
using BroadCasting_WebApp.Services.HttpServices;
using BroadCasting_WebApp.Models;
using BroadCasting_WebApp.Services;
using Microsoft.AspNetCore.ResponseCompression;
using SignalRHub;
using SignalRHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorStrap();
builder.Services.AddHttpServices();
builder.Services.AddCors(options => options.AddPolicy("AllowAny", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddSignalR();
builder.Services.AddSingleton<IHubContextAccessor, HubContextAccessor>();

builder.Services.Configure<BroadCastAPIConfig>(builder.Configuration.GetSection("DataApi"));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();
app.UseCors("AllowAny");
app.UseRouting();

app.UseEventHubContext();

app.MapBlazorHub();
app.MapHub<EventHub>("/eventHub");
app.MapFallbackToPage("/_Host");
app.Run();

