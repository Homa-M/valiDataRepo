using Serilog;



namespace ValiData.Logger
{
    public class TestLog
    {
        public static void log()
        {
            Log.Logger = new LoggerConfiguration()
                                    .WriteTo.Console()
                                    .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day)
                                    .CreateLogger();

            try
            {
                Log.Information("Starting application...");
                Log.Warning("This is a warning");
                Log.Error("Something went wrong");

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application crashed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
