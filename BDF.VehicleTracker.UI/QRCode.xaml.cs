using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BDF.VehicleTracker.UI
{
    /// <summary>
    /// Interaction logic for QRCode.xaml
    /// </summary>
    public partial class QRCode : Window
    {
        public QRCode()
        {
            InitializeComponent();
        }

        private void BtnEncode_Click(object sender, RoutedEventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(txtInfo.Text);

            IntPtr hbitmap = qrcode.GetHbitmap();

            ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(hbitmap,
                                                                            IntPtr.Zero,
                                                                            Int32Rect.Empty,
                                                                            BitmapSizeOptions.FromEmptyOptions());
            imgCode.Source = imageSource;
            qrcode.Save(DateTime.Now.ToLongDateString() + ".png", ImageFormat.Png);
        }

        private void BtnDecode_Click(object sender, RoutedEventArgs e)
        {
            string info;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".png";
            ofd.Multiselect = false;
            ofd.Title = "Select your media";

            bool? result = ofd.ShowDialog();

            if (result == true)
            {
                QRCodeBitmapImage img = new QRCodeBitmapImage(new Bitmap(ofd.FileName));
                QRCodeDecoder decoder = new QRCodeDecoder();
                txtInfo.Text = (decoder.Decode(img)).ToString();

                var image = new System.Windows.Controls.Image
                {
                    Source = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute))
                };

                imgCode.Source = image.Source;

                wbInfo.Navigate(txtInfo.Text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wbInfo.Navigate(txtInfo.Text);
        }
    }
}
