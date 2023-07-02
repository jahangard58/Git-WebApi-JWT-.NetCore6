using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SecendWebAppi.DataBaseContextModel;
using SecendWebAppi.Repository;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Step 1 Set ConnectionString
builder.Services.AddDbContext<dbContextEF>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConection")));

//Step 5  Set Repository
builder.Services.AddScoped<MeesagesRepository, MeesagesRepository>();
builder.Services.AddScoped<TruckRepository, TruckRepository>();

//Step 2 Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});



//Step 3 Set JWT => Jeson Web Token
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(c =>
{
    c.RequireHttpsMetadata = false; //Https Amalyate Authoraize Ham  Anjam Beshe
    c.SaveToken = true;

    c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        RequireExpirationTime = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenConfigTokenUser:Key"])),
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JsonWebTokenConfigTokenUser:EncriptionKey"])),
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JsonWebTokenConfigTokenUser:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JsonWebTokenConfigTokenUser:Audience"]
        

    };

    c.Events = new JwtBearerEvents
    {
        OnTokenValidated = async (context) =>
         {
             //step 1 first login by user in PostMan then test Authorize 
             //Check User if Not Active then UnAuthorize
             var db = context.HttpContext.RequestServices.GetRequiredService<dbContextEF>();

             var claims = (context.Principal.Identity as ClaimsIdentity).Claims;
             var userUid = Guid.Parse(claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value);
             var UserActive = await db.Users.Where(f => f.uid == userUid)
             .Select(f => f.IsActive).
             FirstOrDefaultAsync();
             if (!UserActive)
             {
                 context.Fail("User Is Not Active");
             }
         },
    };
});



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Description = "وب سرویس علی جهانگرد",
        Title = "علی جهانگرد",
        Version = "نسخه ابتدایی"
        //,
        //Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Name = "پشتیبانی علی جهانگرد", Email = "email@ema", Url = new Uri("https://www.google.com") }


    });
    //JWT Init
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //options.RoutePrefix = string.Empty;

        });

}



app.UseHttpsRedirection();

//step 4 JWT
app.UseAuthentication();

//step 4 JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
