using AL_Nibras_Ecom_API.Classes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
 
builder.Services.AddJWT(builder.Configuration);
builder.Services.ConnectionStrings(builder.Configuration);
builder.Services.AddLocalServiceDependencies(); 
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
