using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.NetworkInformation;

namespace Ping_Program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> servers = new Dictionary<string, string>()
        {
            {"usEast1", "34.192.0.54"}
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PingTester(Dictionary<string, string> servers)
        {
            Ping pingSender = new Ping();
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 10 seconds for a reply.
            int timeout = 10000;
            PingOptions options = new PingOptions(64, true);


            foreach (KeyValuePair<string, string> pair in servers)
            {
                // Find the names of each TextBox to alter the contents
                string elipseInd = pair.Key + "Ind";
                string statusBox = pair.Key + "Stat";
                string pingBox = pair.Key + "Ping";

                statusBox.Text = "Testing..."
                PingReply reply = pingSender.Send(pair.Value, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    status
                }
                else
                {

                }
            }
        }
    }
}
