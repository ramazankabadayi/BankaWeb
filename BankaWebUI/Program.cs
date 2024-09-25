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

        // Rolleri ve kullan�c�lar� olu�turma i�lemi
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var context = scope.ServiceProvider.GetRequiredService<BankaWebDbContext>();

            // Rolleri ve kullan�c�lar� olu�turma i�lemi
            await CreateRolesAndUsersIfNeeded(roleManager, userManager, context); // context ekleniyor
        }

        // Static dosyalar� kullan
        app.UseStaticFiles();

        // Kimlik do�rulama ve yetkilendirme i�lemleri
        app.UseAuthentication();
        app.UseAuthorization();

        // Ba�lang�� sayfas� i�in route
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            // pattern: "{controller=Account}/{action=SelectRole}/{id?}");

        app.Run();
    }

    // Rolleri ve kullan�c�lar� olu�turma i�lemi
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

        // Admin, Banka �al��an� ve M��teri kullan�c�lar�n� olu�tur
        await CreateAdminUser(userManager, context);
        await CreateEmployeeUser(userManager, context);
        await CreateCustomerUser(userManager, context); // Customer ekleme i�lemi burada yap�lacak
    }

    // Admin kullan�c�y� ekler ve gerekirse Customer tablosuna da ekler
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
                // Admin kullan�c�s� ba�ar�l� bir �ekilde eklenirse, Customer tablosuna da ekle
                //var newCustomer = new Customer
                //{
                //    Id = newAdmin.Id, // IdentityUser'�n ID'si
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

    // Banka �al��an� ekleme i�lemi
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
                // Banka �al��an� kullan�c�y� Customer tablosuna ekleyin
                //var newCustomer = new Customer
                //{
                //    Id = newEmployee.Id,
                //    FirstName = "Banka",
                //    LastName = "�al��an�",
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

    // M��teri ekleme i�lemi
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
                // M��teri bilgilerini Customer tablosuna ekleyin
                var customerEntity = new Customer
                {
                    Id = newCustomer.Id,
                    FirstName = "M��teri",
                    LastName = "Ad�",
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
