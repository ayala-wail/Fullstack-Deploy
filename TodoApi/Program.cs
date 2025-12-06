using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ✅ הרשאת CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// ✅ הוספת DbContext
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("ToDoDB"),
    new MySqlServerVersion(new Version(8, 0, 36))));

// ✅ הוספת Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// ✅ הפעלת Swagger (בזמן פיתוח)
//if (app.Environment.IsDevelopment())
//{
   app.UseSwagger();
   app.UseSwaggerUI();
//}

// ✅ הפעלת CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());



// ---------------- ROUTES ----------------

// 🟢 שליפת כל המשימות
app.MapGet("/items", async (ToDoDbContext db) =>
{
    Console.WriteLine("📥 נשלפה רשימת המשימות");
    var items = await db.Items.ToListAsync();
    return Results.Ok(items);
});

// 🟡 הוספת משימה חדשה
app.MapPost("/items", async (ToDoDbContext db, Item newItem) =>
{    Console.WriteLine($"🆕 נוספה משימה חדשה: {newItem.Name}");
    db.Items.Add(newItem);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{newItem.Id}", newItem);
});

// 🔵 עדכון משימה קיימת
app.MapPut("/items/{id}", async (ToDoDbContext db, int id, Item updatedItem) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    item.Name = updatedItem.Name;
    item.IsComplete = updatedItem.IsComplete;
    await db.SaveChangesAsync();
    return Results.Ok(item);
});

// 🔴 מחיקת משימה
app.MapDelete("/items/{id}", async (ToDoDbContext db, int id) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();

    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapGet("/", () => "SERVER IS RUNNING");

app.Run();
