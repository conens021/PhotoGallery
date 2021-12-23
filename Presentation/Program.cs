using BLL.Helpers;
using BLL.Mappers.ErrorHandling;
using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.Attributes;
using Presentation.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();


var key = "jdaskjdkajdczxjke";

builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddAuthentication(builder => {
    builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(builder => {
        builder.RequireHttpsMetadata = false;
        builder.SaveToken = true;
        builder.TokenValidationParameters = new TokenValidationParameters() {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped<AuthorizationHelper>();
builder.Services.AddScoped<JwtAuthenticationManager>();

builder.Services.AddScoped<GalleryService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PhotoService>();

builder.Services.AddScoped<FileTypeValidation>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGalleryRepository, GalleryRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-dev");
}
else {

    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
