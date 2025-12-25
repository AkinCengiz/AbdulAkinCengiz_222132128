using System.Reflection;
using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Business.Concrete;
using AbdulAkinCengiz_222132128.Business.Mappings.AutoMapper;
using AbdulAkinCengiz_222132128.Business.Validations.Categories;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.DataAccess.Contexts;
using AbdulAkinCengiz_222132128.DataAccess.UnitOfWorks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using Core.UnitOfWorks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AkinMons"),
        options =>
        {
            options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name);

            options.CommandTimeout(220);          // kritik: timeout
            options.EnableRetryOnFailure(5);
        });
});

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<,>));
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();
builder.Services.AddScoped<IOrderDal, EfOrderDal>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderItemDal, EfOrderItemDal>();
builder.Services.AddScoped<IOrderItemService, OrderItemManager>();
builder.Services.AddScoped<IPaymentDal, EfPaymentDal>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IReservationDal, EfReservationDal>();
builder.Services.AddScoped<IReservationService, ReservationManager>();
builder.Services.AddScoped<ITableDal, EfTableDal>();
builder.Services.AddScoped<ITableService, TableManager>();
builder.Services.AddScoped<IAppUserDal, EfAppUserDal>();
builder.Services.AddScoped<IAppUserService, AppUserManager>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapProfile>();
});
//builder.Services.AddValidatorsFromAssembly(
//    typeof(ReservationManager).Assembly
//);
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateDtoValidator>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
