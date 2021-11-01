using Microsoft.EntityFrameworkCore;
using ApiPostgre;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoItemContext>(options => 
options.UseNpgsql("server=myserver.pg.rds.aliyuncs.com;database=todoitems;user id=user1;password=mypassword")
);

//builder.Services.AddDbContext<TodoListContext>(options => options.UseInMemoryDatabase("TodoItems"));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ApiPostgre", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiPostgre v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
