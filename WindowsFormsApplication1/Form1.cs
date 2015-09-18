using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        OAuthRequest request = null; 
        public Form1()
        {           
            InitializeComponent();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            request = new OAuthRequest {
                Url = textBox1.Text, 
                Usuario = textBox2.Text, 
                Password = textBox3.Text, 
                DispositivoMovil = textBox4.Text, 
                Chip = textBox5.Text
            };
            LoginUser();
        }

        private async void LoginUser()
        {           
            using (var client = new HttpClient())
            {
               System.Net.ServicePointManager.ServerCertificateValidationCallback +=
               delegate(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                       System.Security.Cryptography.X509Certificates.X509Chain chain,
                                       System.Net.Security.SslPolicyErrors sslPolicyErrors)
               {
                   return true; // **** Always accept
               };

                client.BaseAddress = new Uri(request.Url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                
                var content = new FormUrlEncodedContent(new[] 
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("userName", request.Usuario),
                    new KeyValuePair<string, string>("password", request.Password),
                    new KeyValuePair<string, string>("dm", request.DispositivoMovil),
                    new KeyValuePair<string, string>("chip", request.Chip)
                });
                var result = client.PostAsync("/token", content).Result;
                textBox6.Text = result.Content.ReadAsStringAsync().Result;
            }
        }
    }

    public class OAuthRequest
    {
        public string Url { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string DispositivoMovil { get; set; }
        public string Chip { get; set; }
    }

    public class OAuthResponse
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}
