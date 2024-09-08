
using BookBarn_API_Layer.Services;
using BookBarn_DataLayer.DBContext;
using BookBarn_DataLayer.Respositories;
using BookBarn_DomainLayer.Respositories;
using Microsoft.EntityFrameworkCore;

namespace BookBarn_API_Layer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            string conStr = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<BookCatalogDbContext>(options =>
            {
                options.UseSqlServer(conStr);
            });

            builder.Services.AddTransient<IBookRepository, BookRepository>();
            builder.Services.AddTransient<IAuthorRepository, AuthorRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<BookServices>();
            builder.Services.AddTransient<AuthorServices>();
            builder.Services.AddTransient<CategoryServices>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }); 

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowAllOrigins");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
