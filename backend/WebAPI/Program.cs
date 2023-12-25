using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// **********************
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthorization();
// ********************************



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Creating the context object
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(
        builder.
        Configuration.GetConnectionString("homeConnString"),
        builder => builder.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName))
    );


// Dependency injection for the context object
builder.Services.AddScoped<IApplicationDBContext>(
    provider => provider.GetRequiredService<ApplicationDBContext>()
);

// service for type 'MediatR.ISender' has been registered.
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());


// ***********************************
// It's important to note that the server-side logic for extracting and validating
// the JWT is handled by the authentication middleware,
// and it's part of the ASP.NET Core pipeline.
// The middleware is configured in the program.cs file using app.UseAuthentication().
app.UseAuthentication();
app.UseAuthorization();


//app.UseCustomMiddleware(); // when adding middleware globally


// The below code is necessary when using route specific middleware (sequence matters)
app.UseRouting();

app.Map("/api/Car/addCarCustomMiddleware", config =>
{
    config.UseCustomMiddleware();
    config.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});
// ***********************************




app.MapControllers();


app.Run();