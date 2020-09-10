using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace eZwolnienia
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = "Pobieranie informacji...";
            string request = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" 
		xmlns:cez=""https://pue.zus.pl:8001/ws/zus.channel.gabinetoweV2:zla"">
           < soapenv:Header/>
           <soapenv:Body>
           </soapenv:Body>
        </soapenv:Envelope>";
            var stringa = SendSoap(request).Replace("&lt;","<");
            resultTextBox.Text = PrettyXml(stringa);

        }   

        public static string SendSoap(string data)
        {
            var client = new WebClient();
            try
            {
                string credentials = "ezla_ag:ezla_ag";
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(credentials));
                client.Headers.Add("Authorization", "Basic " + encoded);
                client.Headers.Add("Content-Type", "text/xml; charset=utf-8");
                client.Headers.Add("SOAPAction", "zus_channel_zla_Binder_pobierzOswiadczenie");
                var response = client.UploadString("https://pue.zus.pl:8001/ws/zus.channel.gabinetoweV2:zla", data);
                return response;
            }
            catch (Exception e)
            {
                if (e is WebException && ((WebException)e).Status == WebExceptionStatus.ProtocolError)
                {
                    WebResponse errResp = ((WebException)e).Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        string text = reader.ReadToEnd();
                        return text;
                    }
                }
            }
            finally
            {
                client.Dispose();
            }
            return "fail";
        }

        static string PrettyXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }
    }
}
