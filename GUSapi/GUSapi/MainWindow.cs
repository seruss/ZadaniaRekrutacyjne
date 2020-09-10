using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GUSapi
{
    public partial class MainWindow : Form
    {

        private GUSServiceReference.UslugaBIRzewnPublClient client;
        private string sessionID = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            client = new GUSServiceReference.UslugaBIRzewnPublClient();
            try
            {
                sessionID = client.Zaloguj(ConfigurationManager.AppSettings["UserKey"]);
            }
            catch (Exception ex)
            {
                textBoxResult.Text = ex.Message;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {

            GUSServiceReference.ParametryWyszukiwania parametry = new GUSServiceReference.ParametryWyszukiwania();
            parametry.Nipy = textBoxInput.Text;

            using (new OperationContextScope(client.InnerChannel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["sid"] = sessionID;
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                textBoxResult.Text = client.DaneSzukajPodmioty(parametry);
            }

        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Wyloguj(sessionID);
        }
    }
}
