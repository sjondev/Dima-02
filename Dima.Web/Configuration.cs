using MudBlazor;
using MudBlazor.Utilities;

namespace Dima.Web;

public static class Configuration 
{
    public static string HttpClientName = "dima";
    public static string BackendUrl { get; set; } = "https://localhost:5001";
    
    public static MudTheme ThemeOne = new()
    {
        Typography =
        {
            Default = new DefaultTypography()
            {
                FontFamily = ["Raleway", "sans-serif"]
            }
        },
        PaletteLight = new PaletteLight
        {
            Primary = new MudColor("#1EFA2D"),
            AppbarBackground = new MudColor("#1EFA2D"),
            PrimaryContrastText = new MudColor("#000000"),
            Secondary = Colors.LightGreen.Darken3,
            Background = Colors.Gray.Lighten4,
            DrawerBackground = Colors.Green.Darken4,
            TextPrimary = Colors.Shades.Black,
            DrawerText = Colors.Shades.White,
            AppbarText = Colors.Shades.Black,
        },
        PaletteDark = new PaletteDark
        {
            Primary = Colors.LightGreen.Accent3,
            Secondary = Colors.LightGreen.Darken3,
            AppbarBackground = Colors.LightGreen.Accent3,
            AppbarText = Colors.Shades.Black,
            PrimaryContrastText = new MudColor("#000000"),
        }
    };
}