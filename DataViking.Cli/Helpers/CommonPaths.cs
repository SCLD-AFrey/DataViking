namespace DataViking.Cli.Helpers;

public class CommonPaths
{
    private static readonly string AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".dataviking");
    private static readonly string AppLogPath = Path.Combine(AppDataPath, "logs");

    public static void CreateCommonPaths()
    {
        if(!Directory.Exists(AppDataPath)) {
            Directory.CreateDirectory(AppDataPath);
        }

        if (!Directory.Exists(AppLogPath))
        {
            Directory.CreateDirectory(AppDataPath);
        }

    }
}