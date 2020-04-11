using System;
using System.ServiceModel;
using System.Windows;
using TestServerCallBack.WCFService;

namespace TestServerCallBack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _wcfServiceCallback;
        private ServiceCallback _serviceType;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _serviceType = new ServiceCallback();
                _wcfServiceCallback = new ServiceHost(_serviceType);
                _wcfServiceCallback.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                _wcfServiceCallback.Open();
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _serviceType.BroadcastMessage("PORCA MADONNA!!!!");
        }
    }
}