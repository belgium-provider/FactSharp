using System.Collections;
using FactSharp;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var handler = new HttpClientHandler
{
    UseProxy = true,
    // Autres configurations de proxy
};

DotNetEnv.Env.Load();
foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
{
    builder.Configuration[de.Key.ToString() ?? string.Empty] = de.Value?.ToString();
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

/*
 * CORS POLICY => MUST BE CONFIGURED FOR YOUR OWN CAS => THIS IS A SAMPLE DEMO.
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            //authorize access from api gateway
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        });
});

//----------------------------------------------------
//┌────────────────────────────────────┐
//│ WEFACT API CONFIGURATION. 
//└────────────────────────────────────┘ 
//----------------------------------------------------
builder.Services.AddWeFactApi(options =>
{
    options.ApiKey = Environment.GetEnvironmentVariable("WE_FACT") ?? string.Empty;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();