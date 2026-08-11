using Android.App;
using Android.Content;
using Android.Content.PM;

using Avalonia.Android;

namespace ChronoRover.Android;

[Activity(
    Label = "@string/app_name",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@mipmap/ic_launcher",
    RoundIcon = "@mipmap/ic_launcher",
    Banner = "@drawable/tv_banner",
    MainLauncher = true,
    Exported = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
[IntentFilter(
    [Intent.ActionMain],
    Categories = [Intent.CategoryLauncher, "android.intent.category.LEANBACK_LAUNCHER"])]
public class MainActivity : AvaloniaMainActivity;