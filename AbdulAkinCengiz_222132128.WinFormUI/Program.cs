using AbdulAkinCengiz_222132128.Business.Abstract;
using AbdulAkinCengiz_222132128.Business.Concrete;
using AbdulAkinCengiz_222132128.Business.Mappings.AutoMapper;
using AbdulAkinCengiz_222132128.Business.Validations.Customers;
using AbdulAkinCengiz_222132128.DataAccess.Abstract;
using AbdulAkinCengiz_222132128.DataAccess.Concrete.EntityFramework;
using AbdulAkinCengiz_222132128.DataAccess.Contexts;
using AbdulAkinCengiz_222132128.DataAccess.Seeds;
using AbdulAkinCengiz_222132128.DataAccess.UnitOfWorks;
using AbdulAkinCengiz_222132128.Entity.Concrete;
using Core.UnitOfWorks;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AbdulAkinCengiz_222132128.WinFormUI;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        
        var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json",optional : false, reloadOnChange:true).Build();

        var services = new ServiceCollection();

        ConfigureServices(services,configuration);
        var serviceProvider = services.BuildServiceProvider();
        IdentitySeed.SeedAsync(serviceProvider).GetAwaiter().GetResult();
        Application.Run(serviceProvider.GetRequiredService<LoginForm>());
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //DbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("AkinMons"),
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name);

                    options.CommandTimeout(220);          // kritik: timeout
                    options.EnableRetryOnFailure(5);
                });
        });
        
        // Identity (AppUser : IdentityUser)
        services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Þifre/policy ayarlarý
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                // Lockout istersen
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Business
        services.AddScoped<IAuthService, AuthManager>();

        // Session
        services.AddSingleton<ISessionContext, SessionContext>();

        // DataAccess katmanlarýndaki servisler
        services.AddScoped<IAppUserDal, EfAppUserDal>();
        services.AddScoped<ITableDal, EfTableDal>();
        services.AddScoped<IReservationDal, EfReservationDal>();
        services.AddScoped<ICategoryDal, EfCategoryDal>();
        services.AddScoped<ICustomerDal, EfCustomerDal>();
        services.AddScoped<IOrderDal, EfOrderDal>();
        services.AddScoped<IOrderItemDal, EfOrderItemDal>();
        services.AddScoped<IPaymentDal, EfPaymentDal>();
        services.AddScoped<IProductDal, EfProductDal>();

        //Business katmanlarýndaki servisler
        services.AddScoped<IAppUserService, AppUserManager>();
        services.AddScoped<ITableService, TableManager>();
        services.AddScoped<IReservationService, ReservationManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<ICustomerService, CustomerManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IOrderItemService, OrderItemManager>();
        services.AddScoped<IPaymentService, PaymentManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<IDashboardService, DashboardManager>();


        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MapProfile>();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddValidatorsFromAssemblyContaining<CustomerCreateDtoValidator>();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        services.AddTransient<LoginForm>();
        services.AddTransient<MainForm>();
        services.AddTransient<TableListForm>();
        services.AddTransient<OrderForm>();
        services.AddTransient<ReservationForm>();
        services.AddTransient<ReservationActionForm>();
        services.AddTransient<TableManagementForm>();
        services.AddTransient<ProductAndCategoryManagementForm>();
        services.AddTransient<CategoryCreateForm>();
        services.AddTransient<CategoryUpdateForm>();
        services.AddTransient<PaymentForm>();
        services.AddTransient<OrderPaymentForm>();
    }
}