var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios al contenedor.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// 2. Configurar CORS para que Angular pueda conectarse
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// 3. Configurar el pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Esto habilita la interfaz visual de Swagger para probar la API
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
}

app.UseHttpsRedirection();

// IMPORTANTE: UseCors debe ir después de UseRouting y antes de UseAuthorization
app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

app.Run();