using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;

namespace IdentityServerHost.Quickstart.Certificate {
    public class CommonCertificateProvider
    {
        public static X509Certificate2 GetCertificate(IConfiguration configuration)
        {
            string certPath = @"/cert/identity.carpenoctem.local.pfx";
            string certPwd = configuration.GetValue<string>("IdentityCertificatePwd");
            var cert = new X509Certificate2(certPath, certPwd);
            return cert;
        }
    }
}