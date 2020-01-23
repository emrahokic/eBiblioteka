using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace eBiblioteka.DesktopWPF.Helper
{
    public static class ToastNotifikacija
    {
        private const String APP_ID = "eBiblioteka";
        public static void Noti(string Title, string Message, string From, string Type,string Image)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);
            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
           
           
            stringElements[1].AppendChild(toastXml.CreateTextNode(Title));
            stringElements[2].AppendChild(toastXml.CreateTextNode(Message));
            IXmlNode xmlNode = toastXml.SelectSingleNode("/toast");

            ((XmlElement)xmlNode).SetAttribute("launch", "{\"type\":\"toast\",\"param1\":\"12345\",\"param2\":\"67890\"}");

            using (WebClient wc = new WebClient())
            {
                using (Stream s = wc.OpenRead(Image))
                {
                    using (Bitmap bmp = new Bitmap(s))
                    {
                        string slikaName = new Guid().ToString();
                        string url = "file:///" + Path.GetFullPath("" + Title + ".png");
                        bmp.Save(Path.GetFullPath("" + Title + ".png"));

                        String imagePath = url;
                        // Specify the absolute path to an image
                        XmlNodeList imageElements = toastXml.GetElementsByTagName("image");
                        imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;
                        
                        ToastNotification toast = new ToastNotification(toastXml);
                        ToastNotificationManager.CreateToastNotifier(APP_ID + "-" + Type).Show(toast);
                    }
                }
            }

            
        }
    }
}
