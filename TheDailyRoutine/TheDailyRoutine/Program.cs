using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheDailyRoutine.Data;
using TheDailyRoutine.Services.Implementation;
using TheDailyRoutine.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IUserHabitService, UserHabitService>();
builder.Services.AddScoped<IHabitProgressService, HabitProgressService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
