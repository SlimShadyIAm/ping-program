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
            {"US East 1", "23.23.255.255"},
            {"US East 2", "13.58.0.253"},
            {"US West 1", "13.56.63.251"},
            {"US West 2", "34.208.63.251"},
            {"US Central 1", "35.182.0.251"},
            {"EU West 1", "34.248.60.213"},
            {"EU West 2", "35.176.0.252"},
            {"EU Central 1", "35.156.63.252"},
            {"Asia Northeast 1", "13.112.63.251"},
            {"Asia Northeast 2", "13.124.63.251"},
            {"Asia Southeast 1", "13.228.0.251"},
            {"Asia Southeast 2", "13.54.63.252"},
            {"Asia South", "13.126.0.252"}
        };

        public MainWindow()
        {
            InitializeComponent();

            SomeGrid.RowDefinitions.Add(new RowDefinition());
            SomeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SomeGrid.ColumnDefinitions[0].Width = new GridLength(1.5, GridUnitType.Star);

            SomeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SomeGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            SomeGrid.ColumnDefinitions.Add(new ColumnDefinition());
            SomeGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);

            TextBlock serverTitle = new TextBlock();
            serverTitle.Name = "serverTitle";
            serverTitle.Text = "Server Location";
            serverTitle.Padding = new Thickness(10, 5, 10, 5);
            serverTitle.FontWeight = FontWeights.Bold;
            serverTitle.HorizontalAlignment = HorizontalAlignment.Center;
            SomeGrid.Children.Add(serverTitle);
            Grid.SetColumn(serverTitle, 0);
            Grid.SetRow(serverTitle, 0);

            TextBlock statusTitle = new TextBlock();
            statusTitle.Name = "statusTitle";
            statusTitle.Text = "Status";
            statusTitle.Padding = new Thickness(10, 5, 10, 5);
            statusTitle.FontWeight = FontWeights.Bold;
            statusTitle.HorizontalAlignment = HorizontalAlignment.Center;
            SomeGrid.Children.Add(statusTitle);
            Grid.SetColumn(statusTitle, 1);
            Grid.SetRow(statusTitle, 0);

            TextBlock latencyTitle = new TextBlock();
            latencyTitle.Name = "latencyTitle";
            latencyTitle.Text = "Latency";
            latencyTitle.Padding = new Thickness(10, 5, 10, 5);
            latencyTitle.FontWeight = FontWeights.Bold;
            latencyTitle.HorizontalAlignment = HorizontalAlignment.Center;
            SomeGrid.Children.Add(latencyTitle);
            Grid.SetColumn(latencyTitle, 2);
            Grid.SetRow(latencyTitle, 0);
        }

        protected void PingTester(Dictionary<string, string> servers)
        {
            Ping pingSender = new Ping();
            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            // Wait 10 seconds for a reply.
            int timeout = 10000;
            PingOptions options = new PingOptions(64, true);

            int row = 1;

            foreach (KeyValuePair<string, string> pair in servers)
            {
                PingReply reply = pingSender.Send(pair.Value, timeout, buffer, options);

                SomeGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock thisServer = new TextBlock();
                thisServer.Text = pair.Key;
                thisServer.HorizontalAlignment = HorizontalAlignment.Center;
                thisServer.Margin = new Thickness(0, 5, 0, 5);
                thisServer.Padding = new Thickness(5, 5, 5, 5);
                SomeGrid.Children.Add(thisServer);
                Grid.SetColumn(thisServer, 0);
                Grid.SetRow(thisServer, row);

                Ellipse thisEllipse = new Ellipse();
                thisEllipse.Width = 10;
                thisEllipse.Height = 10;
                thisEllipse.Margin = new Thickness(10, 10, 10, 10);
                thisEllipse.HorizontalAlignment = HorizontalAlignment.Left;
                SomeGrid.Children.Add(thisEllipse);
                Grid.SetColumn(thisEllipse, 1);
                Grid.SetRow(thisEllipse, row);

                TextBox statusBox = new TextBox();
                statusBox.Text = "Waiting...";
                statusBox.Padding = new Thickness(5, 5, 5, 5);
                statusBox.Margin = new Thickness(10, 5, 10, 5);
                statusBox.MinWidth = 75;
                statusBox.MaxWidth = 75;
                statusBox.HorizontalAlignment = HorizontalAlignment.Right;
                SomeGrid.Children.Add(statusBox);
                Grid.SetColumn(statusBox, 1);
                Grid.SetRow(statusBox, row);

                TextBox latencyBox = new TextBox();
                latencyBox.Text = "0 ms";
                latencyBox.Margin = new Thickness(0, 5, 0, 5);
                latencyBox.Padding = new Thickness(10, 5, 10, 5);
                statusBox.MinWidth = 75;
                statusBox.MaxWidth = 75;
                latencyBox.HorizontalAlignment = HorizontalAlignment.Center;
                SomeGrid.Children.Add(latencyBox);
                Grid.SetColumn(latencyBox, 2);
                Grid.SetRow(latencyBox, row);

                if (reply.Status == IPStatus.Success)
                {
                    thisEllipse.Fill = new SolidColorBrush(Colors.Green);
                    statusBox.Text = "Success!";
                    latencyBox.Text = reply.RoundtripTime + " ms";
                }
                else
                {
                    thisEllipse.Fill = new SolidColorBrush(Colors.Red);
                    statusBox.Text = "Failed!";
                }

                row++;
            }
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            PingTester(servers);
        }
    }
}
