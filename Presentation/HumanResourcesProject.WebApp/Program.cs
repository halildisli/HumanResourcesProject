using FluentValidation;
using FluentValidation.AspNetCore;
using HumanResourcesProject.Application;
using HumanResourcesProject.Application.DTOs;
using HumanResourcesProject.Application.Email.Configurations;
using HumanResourcesProject.Application.Email.Services;
using HumanResourcesProject.Application.FluentValidation;
using HumanResourcesProject.Application.Repositories;
using HumanResourcesProject.Application.Repositories.AdvanceRepos;
using HumanResourcesProject.Application.Repositories.CompanyRepos;
using HumanResourcesProject.Application.Repositories.DepartmentRepos;
using HumanResourcesProject.Application.Repositories.EmployeeRepos;
using HumanResourcesProject.Application.Repositories.ExpenseRepos;
using HumanResourcesProject.Application.Repositories.JobRepos;
using HumanResourcesProject.Application.Repositories.PermissionRepos;
using HumanResourcesProject.Domain.Entities;
using HumanResourcesProject.Persistance;
using HumanResourcesProject.Persistance.Contexts;
using HumanResourcesProject.Persistance.Repositories.AdvanceRepos;
using HumanResourcesProject.Persistance.Repositories.CompanyRepos;
using HumanResourcesProject.Persistance.Repositories.DepartmentRepos;
using HumanResourcesProject.Persistance.Repositories.EmployeeRepos;
using HumanResourcesProject.Persistance.Repositories.ExpenseRepos;
using HumanResourcesProject.Persistance.Repositories.JobRepos;
using HumanResourcesProject.Persistance.Repositories.PermissionRepos;
using HumanResourcesProject.Persistance.Repositories.PersonRepos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HumanResourcesProjectDbContext>(options =>
{
	options.UseLazyLoadingProxies();
	options.UseSqlServer(builder.Configuration.GetConnectionString("Cloud"));
});
builder.Services.AddIdentity<Person, PersonRole>(options =>
{
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = true;
	
	options.SignIn.RequireConfirmedEmail = true;

	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
	options.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<HumanResourcesProjectDbContext>().AddDefaultTokenProviders();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICompanyReadRepository, CompanyReadRepository>();
builder.Services.AddScoped<ICompanyWriteRepository, CompanyWriteRepository>();

builder.Services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
builder.Services.AddScoped<IDepartmentWriteRepository, DepartmentWriteRepository>();

builder.Services.AddScoped<IJobReadRepository, JobReadRepository>();
builder.Services.AddScoped<IJobWriteRepository, JobWriteRepository>();

builder.Services.AddScoped<IPersonReadRepository, PersonReadRepository>();
builder.Services.AddScoped<IPersonWriteRepository, PersonWriteRepository>();

builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
builder.Services.AddScoped<IEmployeeWriteRepository, EmployeeWriteRepository>();

builder.Services.AddScoped<IAdvanceReadRepository, AdvanceReadRepository>();
builder.Services.AddScoped<IAdvanceWriteRepository, AdvanceWriteRepository>();

builder.Services.AddScoped<IPermissionReadRepository, PermissionReadRepository>();
builder.Services.AddScoped<IPermissionWriteRepository, PermissionWriteRepository>();

builder.Services.AddScoped<IExpenseReadRepository,ExpenseReadRepository >();
builder.Services.AddScoped<IExpenseWriteRepository, ExpenseWriteRepository>();

builder.Services.AddApplicationServices();

//Fluent Validation ekleme//****

//builder.Services.AddTransient<CreateEmployeeDto, CreateEmployeeDtoValidation>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddTransient<IValidator<CreateEmployeeDto>, CreateEmployeeDtoValidation>();
builder.Services.AddTransient<IValidator<EditPersonDto>, EditPersonDtoValidation>();
builder.Services.AddTransient<IValidator<ChangePasswordDto>, ChangePasswordDtoValidation>();
builder.Services.AddTransient<IValidator<Company>, CompanyValidation>();
builder.Services.AddTransient<IValidator<Department>, DepartmentValidation>();
builder.Services.AddTransient<IValidator<Employee>, EmployeeValidation>();
builder.Services.AddTransient<IValidator<Job>, JobValidation>();
builder.Services.AddTransient<IValidator<Person>, PersonValidation>();
builder.Services.AddTransient<IValidator<UpdateEmployeeDto>, UpdateEmployeeDtoValidation>();
builder.Services.AddTransient<IValidator<Expense>, ExpenseValidation>();
builder.Services.AddTransient<IValidator<Permission>, PermissionValidation>();
builder.Services.AddTransient<IValidator<CreateManagerDto>, CreateManagerDtoValidation>();
builder.Services.AddTransient<IValidator<CreateCompanyDto>, CreateCompanyDtoValidation>();



//***/////

/*Email Services*/
builder.Services.AddSingleton
				(builder.Configuration.GetSection
				("EmailConfig").Get<EmailConfig>());

builder.Services.AddScoped<IEmailService, EMailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=SignIn}/{id?}");

app.Run();
