using HandyControl.Tools;
using System.Globalization;
using System.Windows;

namespace WpfTestApp {
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            base.OnStartup(e);
            ConfigHelper.Instance.SetLang("en-us");
        }
    }
}
