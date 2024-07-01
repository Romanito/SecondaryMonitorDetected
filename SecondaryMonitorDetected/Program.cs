using System;
using System.Drawing;
using System.Management;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SecondaryMonitorDetected.Properties;

namespace SecondaryMonitorDetected
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            NotifyIcon icon = new NotifyIcon
            {
                Icon = Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),
                Text = "Affichage secondaire détecté"
            };

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity where service =\"monitor\"");

            while (true)
            {
                icon.Visible = searcher.Get().Count > Settings.Default.NbMonitors;
                Thread.Sleep(Settings.Default.Interval);
            }
        }
    }
}
