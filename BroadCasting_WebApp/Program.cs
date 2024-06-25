using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorStrap;
using BroadCasting_WebApp.Services.HttpServices;
using BroadCasting_WebApp.Models;
using BroadCasting_WebApp.Services;
using BroadCasting_WebApp.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorStrap();
builder.Services.AddHttpServices();
builder.Services.AddSignalR();
builder.Services.AddResponseCompression();

builder.Services.Configure<BroadCastAPIConfig>(builder.Configuration.GetSection("DataApi"));

var app = builder.Build();
app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapHub<DataHub>("/datahub");
app.MapFallbackToPage("/_Host");

app.Run();
