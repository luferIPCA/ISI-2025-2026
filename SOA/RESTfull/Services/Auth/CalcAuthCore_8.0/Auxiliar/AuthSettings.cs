/*
 * lufer
 * ISI
 * OAuth
 * */

using System.Security.Cryptography;
namespace AuthCore.Helpers;

public static class AuthSettings
{
    //This key must be preserved outside the code...in appsettings.json
    public static string PrivateKey { get; set; } = GeraKey(256);

    /// <summary>
    /// Gera cifra de n bits
    /// </summary>
    /// <param name="nBits"></param>
    /// <returns></returns>
    public static string GeraKey(int nBytes)
    {
        Aes aesAlgorithm = Aes.Create();
        aesAlgorithm.KeySize = 256;
        aesAlgorithm.GenerateKey();
        string keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);
        return keyBase64;
    }
}