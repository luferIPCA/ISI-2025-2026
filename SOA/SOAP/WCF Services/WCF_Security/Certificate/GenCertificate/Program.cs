using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.IO;

namespace GenCertificate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter hostname: ");
            string hostname = Console.ReadLine();

            Console.Write("Enter days until expiration: ");
            int days = int.Parse(Console.ReadLine());

            Console.Write("Enter password, enter to skip: ");
            string password = Console.ReadLine();

            // Generate a new RSA key pair
            RSA rsa = RSA.Create();

            // Create a certificate request with the specified subject and key pair
            CertificateRequest request = new CertificateRequest(
                $"CN={hostname}",
                rsa,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            // Create a self-signed certificate from the certificate request
            X509Certificate2 certificate = request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(days));

            // Export the certificate to a file with password
            byte[] certBytes = string.IsNullOrEmpty(password)
                ? certificate.Export(X509ContentType.Pfx)
                : certificate.Export(X509ContentType.Pfx, password);
            File.WriteAllBytes($"{hostname}.pfx", certBytes);

            Console.WriteLine($"Certificate for {hostname} created successfully and will expire on {certificate.NotAfter}.");
            Console.WriteLine($"Path: {Path.Combine(AppContext.BaseDirectory, hostname)}.pfx");
            Console.ReadKey();
        }
    }
}
