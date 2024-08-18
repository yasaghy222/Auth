using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        WebApplication app = builder.Build();
        {
            app.UseHttpsRedirection();
            app.Run();
        }
    }
}