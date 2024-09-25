using BankaWebBL.Managers;
using BankaWebDL;
using BankaWebEL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Servisleri ekleyelim
        builder.Services.AddAutoMapper(typeof(MapperConfig));
        builder.Services.AddScoped<AccountManager>();
        builder.Services.AddScoped<CustomerManager>();
        builder.Services.AddScoped<TransactionManager>();
        builder.Services.AddScoped<CurrencyManager>();
        builder.Services.AddScoped<AccountTypeManager>();

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                        .AddEntityFrameworkStores<BankaWebDbContext>()
                        .AddDefaultTokenProviders();


        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<BankaWebDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Rolleri ve kullanýcýlarý oluþturma iþlemi
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = scope.ServiceProvider.GetRequiredService<BankaWebDbContext>();

            // Rolleri ve kullanýcýlarý oluþturma iþlemi
            await CreateRolesAndUsersIfNeeded(roleManager, userManager, context); // context ekleniyor
        }

        // Static dosyalarý kullan
        app.UseStaticFiles();

        // Kimlik doðrulama ve yetkilendirme iþlemleri
        app.UseAuthentication();
        app.UseAuthorization();

        // Baþlangýç sayfasý için route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            // pattern: "{controller=Account}/{action=SelectRole}/{id?}");

        app.Run();
    }

    // Rolleri ve kullanýcýlarý oluþturma iþlemi
    private static async Task CreateRolesAndUsersIfNeeded(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, BankaWebDbContext context)
    {
        var roles = new List<string> { "Admin", "Employee", "Customer" };

        // Rolleri kontrol et ve yoksa ekle
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Admin, Banka Çalýþaný ve Müþteri kullanýcýlarýný oluþtur
        await CreateAdminUser(userManager, context);
        await CreateEmployeeUser(userManager, context);
        await CreateCustomerUser(userManager, context); // Customer ekleme iþlemi burada yapýlacak
    }

    // Admin kullanýcýyý ekler ve gerekirse Customer tablosuna da ekler
    private static async Task CreateAdminUser(UserManager<IdentityUser> userManager, BankaWebDbContext context)
    {
        var adminEmail = "admin@bankaweb.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            var newAdmin = new IdentityUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newAdmin, "Admin@123");
            if (result.Succeeded)
            {
                // Admin kullanýcýsý baþarýlý bir þekilde eklenirse, Customer tablosuna da ekle
                //var newCustomer = new Customer
                //{
                //    Id = newAdmin.Id, // IdentityUser'ýn ID'si
                //    FirstName = "Admin",
                //    LastName = "Admin",
                //    Email = adminEmail,
                //    PhoneNumber = "555-555-5555",
                //    CreatedDate = DateTime.Now,
                //    IsDeleted = false
                //};

                //context.Customers.Add(newCustomer);
                await context.SaveChangesAsync();
            }
        }
    }

    // Banka Çalýþaný ekleme iþlemi
    private static async Task CreateEmployeeUser(UserManager<IdentityUser> userManager, BankaWebDbContext context)
    {
        var employeeEmail = "employee@bankaweb.com";
        var employeeUser = await userManager.FindByEmailAsync(employeeEmail);
        if (employeeUser == null)
        {
            var newEmployee = new IdentityUser
            {
                UserName = employeeEmail,
                Email = employeeEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newEmployee, "Employee@123");
            if (result.Succeeded)
            {
                // Banka Çalýþaný kullanýcýyý Customer tablosuna ekleyin
                //var newCustomer = new Customer
                //{
                //    Id = newEmployee.Id,
                //    FirstName = "Banka",
                //    LastName = "Çalýþaný",
                //    Email = employeeEmail,
                //    PhoneNumber = "555-555-5556",
                //    CreatedDate = DateTime.Now,
                //    IsDeleted = false
                //};

                //context.Customers.Add(newCustomer);
                await context.SaveChangesAsync();
            }
        }
    }

    // Müþteri ekleme iþlemi
    private static async Task CreateCustomerUser(UserManager<IdentityUser> userManager, BankaWebDbContext context)
    {
        var customerEmail = "customer3@bankaweb.com";
        var customerUser = await userManager.FindByEmailAsync(customerEmail);
        if (customerUser == null)
        {
            var newCustomer = new IdentityUser
            {
                UserName = customerEmail,
                Email = customerEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(newCustomer, "Customer@123");
            if (result.Succeeded)
            {
                // Müþteri bilgilerini Customer tablosuna ekleyin
                var customerEntity = new Customer
                {
                    Id = newCustomer.Id,
                    FirstName = "Müþteri",
                    LastName = "Adý",
                    Email = customerEmail,
                    Address = "Adres",
                    PhoneNumber = "555-555-5557",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                };

                context.Customers.Add(customerEntity);
                await context.SaveChangesAsync();
            }
        }
    }
}
