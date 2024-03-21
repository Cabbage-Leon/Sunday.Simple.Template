using Repository.Extension.ServiceExtensions;
using Serilog;
using Sunday.Simple.Template.Common;
using Sunday.Simple.Template.IService;
using Sunday.Simple.Template.Service;

try
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    Log.Information("Initializing in Api:Main...");

    var builder = WebApplication.CreateBuilder(args);

    Log.Information("Program:Main:WebApplication Builder created");
    
    Log.Information("Program:Main:Starting configuring Services...");
    
    builder.Services.AddControllers();
    
    builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
    AutoMapperConfig.RegisterMappings();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.Services.AddSingleton(new AppSettings(builder.Configuration));

    builder.Services.AddDbContext(builder.Configuration);

    builder.Services.AddScoped<IUserService, UserService>();
    
    Log.Information("Program:Main:Services configuration done");
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();

    app.Run();
    
    Log.Information("Program:App configuration done");
}
catch (Exception ex)
{
    Log.Logger.Fatal(ex, "Program:Main:Fatal error in Main");
    Environment.Exit(1);
}