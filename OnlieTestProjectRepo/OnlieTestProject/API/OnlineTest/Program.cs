using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineTest.Model;
using OnlineTest.Model.Interfaces;
using OnlineTest.Model.Repository;
using OnlineTest.Services.AutoMapperProfile;
using OnlineTest.Services.Interface;
using OnlineTest.Services.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureJwtAuthService(builder.Services);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    //option.SwaggerDoc("v1", new OpenApiInfo { Title = "JWTDemo", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter jwt access token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddDbContext<OnlineTestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLAuth"), b => b.MigrationsAssembly("OnlineTest.Model"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#region Dependency Injection
builder.Services.AddScoped<IHasherService, HasherService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
builder.Services.AddScoped<IRTokenService, RTokenService>();
builder.Services.AddScoped<IRTokenRepository, RTokenRepository>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<ITechnologyRepository, TechnologyRepository>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<ITestRepository, TestRepository>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IAnswerRepository, AnswerRepository>();
builder.Services.AddScoped<IQuestionAnswerMapRepository, QuestionAnswerMapRepository>();
builder.Services.AddScoped<ITestLinkRepository, TestLinkRepository>();
builder.Services.AddScoped<IAnswerSheetRepository, AnswerSheetRepository>();
#endregion

var app = builder.Build();

#region Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

void ConfigureJwtAuthService(IServiceCollection services)
{
    var config = builder.Configuration.GetSection("JWTConfig");
    var symmetricKeyAsBase64 = config["SecretKey"];
    var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
    var signingKey = new SymmetricSecurityKey(keyByteArray);

    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["Issuer"],
            ValidateAudience = true,
            ValidAudience = config["Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            RoleClaimType = "Role",
            ClockSkew = TimeSpan.Zero
        };
        //var events = new JwtBearerEvents();
        //events.OnAuthenticationFailed = async context =>
        //{
        //    //context.HandleResponse();
        //    context.Response.StatusCode = 401;
        //    context.Response.Headers.Append("UnAuthenticat", "");
        //    await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        //    {
        //        data = "",
        //        status = 401,
        //        message = "You are not Authenticat to use API."
        //    }));
        //};
        //events.OnForbidden = async context =>
        //{
        //    //context.HandleResponse();
        //    context.Response.StatusCode = 403;
        //    context.Response.Headers.Append("UnAuthorized", "");
        //    await context.Response.WriteAsync("403 Forbidden");
        //};
        //options.Events = events;
    });
}