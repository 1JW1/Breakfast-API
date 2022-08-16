using Breakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    // tells Framework that when IBreak.. is the constructor then use Break.. as implementation of interface
    // AddSingleton() - when ibreak... is called create break.. obj
    //changed to AddScoped() and service to static so that dictionary isnt created every time
    builder.Services.AddScoped<IBreakfastService, BreakfastService>();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}


