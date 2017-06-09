using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace Licensing
{
    public static class License
    {
        public static Credential _Credential;
        public class Credential
        {
            public void Create(string id, string pass)
            {
                Id = id;
                PassHash = ID.sha256(pass);
            }
            public bool Check(string pass)
            {
                if (ID.sha256(pass) == PassHash)
                {
                    return true;
                }
                else
                    return false;
            }
            public string MID;
            public string MHASH;
            public string Id;
            public string PassHash;
            public string RequestResponse;
        }
        private static string _Hash = "";
        private static string _ID = "";
        public static void CreateLogin(string url, ref Credential cred)
        {
            
            cred.MID=_ID = ID.GetId();
            cred.MHASH=_Hash = ID.sha256(_ID);
            cred.RequestResponse = HTTP.Post(url, "MID=" + cred.MHASH + "&CMD=" + "0"+ "&ID=" + cred.Id + "&p=" + cred.PassHash); //0 is ID create
            if (!cred.RequestResponse.ToUpper().Contains("OK"))
            {
                System.Windows.Forms.MessageBox.Show("Error Requesting License Check");
            }
        }
        public static void CheckLogin(string url, ref Credential cred)
        {

            cred.MID = _ID = ID.GetId();
            cred.MHASH = _Hash = ID.sha256(_ID);
            cred.RequestResponse = HTTP.Post(url, "MID=" + cred.MHASH + "&CMD=" + "1" + "&ID=" + cred.Id + "&p=" + cred.PassHash); //1 is ID check
            if (!cred.RequestResponse.ToUpper().Contains("OK"))
            {
                System.Windows.Forms.MessageBox.Show("Error Requesting License Check");
            }
        }
        public static class ID
        {
            public static string GetActiveMAC()
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        if (nic.GetIPProperties().GatewayAddresses.Count > 0)
                        {
                            //HAS GATEWAY
                            return nic.GetPhysicalAddress().ToString();
                        }
                    }
                }
                return "";
            }
            public static string GetHostName()
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                return computerProperties.HostName;
            }
            public static string GetHostDomain()
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                return computerProperties.DomainName;
            }
            public static string GetId()
            {
                return GetActiveMAC() + "<:>" + GetHostName() + "<:>" + GetHostDomain();
            }
            public static string sha256(string input)
            {
                System.Security.Cryptography.SHA256Managed crypt = new System.Security.Cryptography.SHA256Managed();
                System.Text.StringBuilder hash = new System.Text.StringBuilder();
                byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input), 0, Encoding.UTF8.GetByteCount(input));
                foreach (byte theByte in crypto)
                {
                    hash.Append(theByte.ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
    public static class HTTP
    {
        public static Exception EXP;
        public delegate object GetAction(string cmd);
        public delegate void PostBack(string response);
        public static object Get(string url, GetAction action)
        {
            try
            {
                WebClient wc = new WebClient();
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)SecurityProtocolType.Tls | (System.Net.SecurityProtocolType)SecurityProtocolType.Ssl3 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls11 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                string cmd = wc.DownloadString(url);
                wc.Dispose();
                return action?.Invoke(cmd);
            }
            catch { return null; }
        }
        public static string GetString(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)SecurityProtocolType.Tls | (System.Net.SecurityProtocolType)SecurityProtocolType.Ssl3 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls11 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                string cmd = wc.DownloadString(url);
                wc.Dispose();
                return cmd;
            }
            catch { }
            return "ex";
        }
        public static string GetFile(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)SecurityProtocolType.Tls | (System.Net.SecurityProtocolType)SecurityProtocolType.Ssl3 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls11 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                var tmp = Path.GetTempPath() + Path.GetFileName(url);

                wc.DownloadFile(url, tmp);
                wc.Dispose();
                return tmp;
            }
            catch { return null; }
        }
        public static string Post(string url, string Parameters)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)SecurityProtocolType.Tls | (System.Net.SecurityProtocolType)SecurityProtocolType.Ssl3 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls11 | (System.Net.SecurityProtocolType)SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                var req = System.Net.WebRequest.Create(url);
                //req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
                req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;

                var aa = 0;
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
                req.ContentLength = bytes.Length;
                System.IO.Stream os = req.GetRequestStream();
                os.Write(bytes, 0, bytes.Length);
                os.Close();
                System.Threading.Thread.Sleep(100);
                System.Net.WebResponse resp = req.GetResponse();
                if (resp == null)
                    return null;
                var sr = new System.IO.StreamReader(resp.GetResponseStream());
                return sr.ReadToEnd().Trim();
            }
            catch (Exception ex) { EXP = ex; }
            return "error";
        }
        public static void AsyncPost(string url, string Parameters, PostBack Callback)
        {
            System.Threading.Thread ap = new System.Threading.Thread(new System.Threading.ThreadStart(() => {
                try
                {
                    var req = System.Net.WebRequest.Create(url);
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.Method = "POST";
                    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
                    req.ContentLength = bytes.Length;
                    System.IO.Stream os = req.GetRequestStream();
                    os.Write(bytes, 0, bytes.Length);
                    os.Close();
                    System.Threading.Thread.Sleep(100);
                    System.Net.WebResponse resp = req.GetResponse();
                    if (resp == null)
                        Callback?.Invoke("nodata");
                    var sr = new System.IO.StreamReader(resp.GetResponseStream());
                    Callback?.Invoke(sr.ReadToEnd().Trim());
                    sr.Close();
                }
                catch { Callback?.Invoke("error"); }
            }));
            ap.Start();

        }
        public class UploadFile
        {
            public UploadFile()
            {
                ContentType = "application/octet-stream";
            }
            public string Name { get; set; }
            public string Filename { get; set; }
            public string ContentType { get; set; }
            public Stream Stream { get; set; }
        }

        public static byte[] UploadFiles(string url, UploadFile[] files, System.Collections.Specialized.NameValueCollection values)
        {
            var request = WebRequest.Create(url);
            request.Method = "POST";
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", System.Globalization.NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                // Write the values
                foreach (string name in values.Keys)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                // Write the files
                foreach (var file in files)
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    file.Stream.CopyTo(requestStream);
                    buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);
            }

            using (var response = request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var stream = new MemoryStream())
            {
                responseStream.CopyTo(stream);
                return stream.ToArray();
            }
        }
        public static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors error)
        {
            if (error == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            Console.WriteLine("X509Certificate [{0}] Policy Error: '{1}'",
                cert.Subject,
                error.ToString());
            return false; //set to true to bypass check
        }
        public enum SecurityProtocolType
        {
            //
            // Summary:
            //     Specifies the Secure Socket Layer (SSL) 3.0 security protocol.
            Ssl3 = 48,
            //
            // Summary:
            //     Specifies the Transport Layer Security (TLS) 1.0 security protocol.
            Tls = 192,
            //
            // Summary:
            //     Specifies the Transport Layer Security (TLS) 1.1 security protocol.
            Tls11 = 768,
            //
            // Summary:
            //     Specifies the Transport Layer Security (TLS) 1.2 security protocol.
            Tls12 = 3072
        }
        private readonly static string reservedCharacters = "!*'();:@&=+$,/?#[]";
    }

}
