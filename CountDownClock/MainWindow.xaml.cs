using System;
using System.Configuration;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace CountDownClock
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DateTime targetTime;

        public MainWindow()
        {
            targetTime
                = DateTime.ParseExact(ConfigurationManager.AppSettings["TargetTime"],
                    ConfigurationManager.AppSettings["TimeFormat"], CultureInfo.CurrentCulture);

            var timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            timer.Tick += TimerTickAction;
            timer.Start();
        }

        private void TimerTickAction(object a, EventArgs eventArgs)
        {
            var timeLeft = targetTime - DateTime.Now;
            textbox.Text =
                GetPlural("{0} Day{1}, ", timeLeft.Days)
                + GetPlural("{0} Hour{1}, ", timeLeft.Hours)
                + GetPlural("{0} Minute{1}, ", timeLeft.Minutes)
                + GetPlural("{0} Second{1}, ", timeLeft.Seconds);
        }

        private static string GetPlural(string template, int value)
        {
            return string.Format(template, value, value != 1 && value != -1 ? "s" : "");
        }
    }
}