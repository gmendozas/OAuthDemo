using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class Ssl
    {
        private static readonly string[] TrustedHosts = new[] {
          "bcbeta193.compartamos.mx", 
          "bcbeta193.compartamox.mx:444"
        };

        public static void EnableTrustedHosts()
        {
            ServicePointManager.ServerCertificateValidationCallback =
            (sender, certificate, chain, errors) =>
            {
                if (errors == SslPolicyErrors.None)
                {
                    return true;
                }

                var request = sender as HttpWebRequest;
                if (request != null)
                {
                    return TrustedHosts.Contains(request.RequestUri.Host);
                }

                return false;
            };
        }
    }
}
