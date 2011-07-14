using System.Security.Cryptography;
using System;
using System.IO;
using System.Text;
namespace Dashboard365Service
{
    public class RSAUtil
    {
        private static volatile RSAUtil instance;
        private static object syncRoot = new Object();

        private static string publicKey = null;//"<RSAKeyValue><Modulus>6CdsXgYOyya/yQHTO96dB3gEurM2UQDDVGrZoe6RcAVTxAqDDf5LwPycZwtNOx3Cfy44/D5Mj86koPew5soFIz9sxPAHRF5hcqJoG+q+UfUYTHYCsMH2cnqGVtnQiE/PMRMmY0RwEfMIo+TDpq3QyO03MaEsDGf13sPw9YRXiac=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private static string privateKey = null;//"<RSAKeyValue><Modulus>6CdsXgYOyya/yQHTO96dB3gEurM2UQDDVGrZoe6RcAVTxAqDDf5LwPycZwtNOx3Cfy44/D5Mj86koPew5soFIz9sxPAHRF5hcqJoG+q+UfUYTHYCsMH2cnqGVtnQiE/PMRMmY0RwEfMIo+TDpq3QyO03MaEsDGf13sPw9YRXiac=</Modulus><Exponent>AQAB</Exponent><P>/aoce2r6tonjzt1IQI6FM4ysR40j/gKvt4dL411pUop1Zg61KvCm990M4uN6K8R/DUvAQdrRdVgzvvAxXD7ESw==</P><Q>6kqclrEunX/fmOleVTxG4oEpXY4IJumXkLpylNR3vhlXf6ZF9obEpGlq0N7sX2HBxa7T2a0WznOAb0si8FuelQ==</Q><DP>3XEvxB40GD5v/Rr4BENmzQW1MBFqpki6FUGrYiUd2My+iAW26nGDkUYMBdYHxUWYlIbYo6Tezc3d/oW40YqJ2Q==</DP><DQ>LK0XmQCmY/ArYgw2Kci5t51rluRrl4f5l+aFzO2K+9v3PGcndjAStUtIzBWGO1X3zktdKGgCLlIGDrLkMbM21Q==</DQ><InverseQ>GqC4Wwsk2fdvJ9dmgYlej8mTDBWg0Wm6aqb5kjncWK6WUa6CfD+XxfewIIq26+4Etm2A8IAtRdwPl4aPjSfWdA==</InverseQ><D>a1qfsDMY8DSxB2DCr7LX5rZHaZaqDXdO3GC01z8dHjI4dDVwOS5ZFZs7MCN3yViPsoRLccnVWcLzOkSQF4lgKfTq3IH40H5N4gg41as9GbD0g9FC3n5IT4VlVxn9ZdW+WQryoHdbiIAiNpFKxL/DIEERur4sE1Jt9VdZsH24CJE=</D></RSAKeyValue>";

        private static string KeyContainerName = "Dashboard365KeyConainer";

        private RSAUtil() { }

        public static RSAUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new RSAUtil();
                            instance.initialKey();
                        }
                    }
                }

                return instance;
            }
        }

        private void initialKey()
        {
            CspParameters cp = new CspParameters();
            cp.KeyContainerName = KeyContainerName;

            // Create a new instance of RSACryptoServiceProvider that accesses
            // the key container MyKeyContainerName.
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cp);
            privateKey=rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }
       
        public string Decrypt(string base64code)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSA.FromXmlString(privateKey);

                byte[] encryptedData;
                byte[] decryptedData;
                encryptedData = Convert.FromBase64String(base64code);

                //Pass the data to DECRYPT, the private key information 
                //(using RSACryptoServiceProvider.ExportParameters(true),
                //and a boolean flag specifying no OAEP padding.
                decryptedData = RSADecrypt(encryptedData, RSA.ExportParameters(true), false);

                //Display the decrypted plaintext to the console. 
                return ByteConverter.GetString(decryptedData);
            }
            catch (Exception exc)
            {
                //Exceptions.LogException(exc);
                Console.WriteLine(exc.Message);
                return "";
            }
        }
        public string Encrypt(string toEncryptString)
        {
            try
            {
                //Create a UnicodeEncoder to convert between byte array and string.
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //Create byte arrays to hold original, encrypted, and decrypted data.
                byte[] dataToEncrypt = ByteConverter.GetBytes(toEncryptString);
                byte[] encryptedData;

                //Create a new instance of RSACryptoServiceProvider to generate
                //public and private key data.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                RSA.FromXmlString(privateKey);

                //Pass the data to ENCRYPT, the public key information 
                //(using RSACryptoServiceProvider.ExportParameters(false),
                //and a boolean flag specifying no OAEP padding.
                encryptedData = RSAEncrypt(dataToEncrypt, RSA.ExportParameters(false), false);

                string base64code = Convert.ToBase64String(encryptedData);
                return base64code;
            }
            catch (Exception exc)
            {
                //Catch this exception in case the encryption did
                //not succeed.
                //Exceptions.LogException(exc);
                Console.WriteLine(exc.Message);
                return "";
            }
        }
        private byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This only needs
                //toinclude the public key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Encrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                //Exceptions.LogException(e);
                Console.WriteLine(e.Message);
                return null;
            }
        }
        private byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                //Create a new instance of RSACryptoServiceProvider.
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();

                //Import the RSA Key information. This needs
                //to include the private key information.
                RSA.ImportParameters(RSAKeyInfo);

                //Decrypt the passed byte array and specify OAEP padding.  
                //OAEP padding is only available on Microsoft Windows XP or
                //later.  
                return RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                //Exceptions.LogException(e);
                Console.WriteLine(e.Message);

                return null;
            }

        }
    }
}