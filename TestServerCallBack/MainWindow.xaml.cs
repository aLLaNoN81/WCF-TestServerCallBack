using System;
using System.ServiceModel;
using System.Windows;

namespace TestServerCallBack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _wcfServiceCallback;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _wcfServiceCallback = new ServiceHost(new WCFService.ServiceCallback());
                _wcfServiceCallback.Credentials.WindowsAuthentication.AllowAnonymousLogons = true;
                _wcfServiceCallback.Open();
            }
            catch (Exception)
            {
            }
        }
    }
}