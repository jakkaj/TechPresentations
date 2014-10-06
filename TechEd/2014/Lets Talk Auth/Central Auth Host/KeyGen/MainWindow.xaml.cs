using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace KeyGen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateKeys_OnClick(object sender, RoutedEventArgs e)
        {
            _generateKeys();
        }

         void _generateKeys()
        {
            var result = GenerateRsaKeys();


            var pub = Convert.ToBase64String(Encoding.UTF8.GetBytes(result.PublicKeyOnly));
            var pri = Convert.ToBase64String(Encoding.UTF8.GetBytes(result.PublicAndPrivateKey));


            System.Console.WriteLine("Public Key: {0}", pub);
            System.Console.WriteLine("Private Key: {0}", pri);

            txtPublicBox.Text = pub;
            txtPrivateBox.Text = pri;
        }


        private static RsaKeyGenerationResult GenerateRsaKeys()
        {
            var myRSA = new RSACryptoServiceProvider(2048);
            var publicKey = myRSA.ExportParameters(true);
            var result = new RsaKeyGenerationResult();
            result.PublicAndPrivateKey = myRSA.ToXmlString(true);
            result.PublicKeyOnly = myRSA.ToXmlString(false);
            return result;
        }

        public class RsaKeyGenerationResult
        {
            public string PublicKeyOnly { get; set; }
            public string PublicAndPrivateKey { get; set; }
        }
    }
}
