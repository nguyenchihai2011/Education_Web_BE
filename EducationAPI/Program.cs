using EducationAPI.Implement;
using EducationAPI.Implement.Repositories;
using EducationAPI.Interfaces;
using EducationAPI.Interfaces.Repositories;
using EducationAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<ILectureRepository, LectureRepository>()
    .AddScoped<IAdminRepository, AdminRepository>()
    .AddScoped<IStudentRepository, StudentRepository>()
    .AddScoped<ICourseRepository, CourseRepository>()
    .AddScoped<ISectionRepository, SectionRepository>()
    .AddScoped<ILessonRepository, LessonRepository>()
    .AddScoped<IQuizRepository, QuizRepository>()
    .AddScoped<IAnswerRepository, AnswerRepository>()
    .AddScoped<IPromotionRepository, PromotionRepository>()
    .AddScoped<INotifycationRepository, NotifycationRepository>()
    .AddScoped<ICommentRepository, CommentRepository>()
    .AddScoped<IRatingRepository, RatingRepository>()
    .AddScoped<ICartRepository, CartRepository>();



// Add services to the container.
builder.Services.AddDbContext<EducationAPI.Context.ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);
builder.Services.AddIdentity<AppUserEntity, IdentityRole>().AddEntityFrameworkStores<EducationAPI.Context.ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(Program));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
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
                    Id="Bearer"
                }
            },
            new string[]{ }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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
