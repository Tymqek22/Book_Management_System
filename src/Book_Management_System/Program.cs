using Book_Management_System.Interfaces;
using Book_Management_System.Repositories;
using Book_Management_System.Services;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("BookManagementSystem"), b => 
b.MigrationsAssembly("Book_Management_System")));

builder.Services.AddScoped<IBorrowService,BorrowService>();
builder.Services.AddScoped<IReportService,ReportService>();
builder.Services.AddScoped<IDashboardService,DashboardService>();

builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<IBorrowRecordRepository,BorrowRecordRepository>();
builder.Services.AddScoped<IMemberRepository,MemberRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}/{id?}");

app.Run();
