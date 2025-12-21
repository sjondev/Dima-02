namespace Dima.Core;

public static class Configuration
{
    public const int DefaultStatusCode = 200;
    public const int DefaultPageNumber = 1;
    public const int DefaultPageSize = 25;
    
    public static string ConnectionString  { set; get; } = string.Empty;
    
    public static string BackendUrl { set; get; } = string.Empty;
    public static string FrontendUrl { set; get; } = string.Empty;
}