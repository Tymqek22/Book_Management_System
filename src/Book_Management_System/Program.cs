using Book_Management_System.Repositories;
using Book_Management_System.Repositories.Interfaces;
using Book_Management_System.Services;
using Book_Management_System.Services.Interfaces;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("BookManagementSystem"), b => 
b.MigrationsAssembly("Book_Management_System")));

builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IBorrowService,BorrowService>();
builder.Services.AddScoped<IReportService,ReportService>();
builder.Services.AddScoped<IDashboardService,DashboardService>();
builder.Services.AddScoped<ISortingService,SortingService>();

builder.Services.AddTransient<IBookRepository,BookRepository>();
builder.Services.AddTransient<IBorrowRecordRepository,BorrowRecordRepository>();
builder.Services.AddTransient<IMemberRepository,MemberRepository>();

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
    pattern: "{controller=Book}/{action=Index}/{pageNumber=1}");

app.Run();
