using BayerRadyofarmasotik.FileHelper;
using BayerRadyofarmasotik.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Connectors
{
    public class SaglikApiBaseClient
    {
        //private static string baseAddress = Resources.testApiServiceEndPoint;
        Dictionary<string, string> configData = Operations.ReadFile("config.txt");

        public string PostData(string baseAddress, string endPoint, string data, out System.Net.HttpStatusCode status)
        {
            var response = "";
            //var data = JsonConvert.SerializeObject(user);

            try
            {
                string addr = baseAddress + endPoint;
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(addr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                if (Convert.ToBoolean(configData["enabled"].Substring(0, configData["enabled"].Length).ToLower()) == true)
                {
                    WebProxy webProxy = new WebProxy(configData["host"].Substring(1, configData["host"].Length - 2), Convert.ToInt32(configData["port"].Substring(1, configData["port"].Length - 2)));
                    webProxy.Credentials = new NetworkCredential(configData["proxyUsername"].Substring(1, configData["proxyUsername"].Length - 2), configData["proxyPassword"].Substring(1, configData["proxyPassword"].Length - 2));
                    httpWebRequest.Proxy = webProxy;
                }

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                status = httpResponse.StatusCode;

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public string PostData(string baseAddress, string endPoint, string data, string accessToken)
        {
            var response = "";

            try
            {
                string addr = baseAddress + endPoint;
                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(addr);

                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Headers.Add("Authorization", "Bearer " + accessToken);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                if (Convert.ToBoolean(configData["enabled"].Substring(0, configData["enabled"].Length).ToLower()) == true)
                {
                    WebProxy webProxy = new WebProxy(configData["host"].Substring(1, configData["host"].Length - 2), Convert.ToInt32(configData["port"].Substring(1, configData["port"].Length - 2)));
                    webProxy.Credentials = new NetworkCredential(configData["proxyUsername"].Substring(1, configData["proxyUsername"].Length - 2), configData["proxyPassword"].Substring(1, configData["proxyPassword"].Length - 2));
                    httpWebRequest.Proxy = webProxy;
                }

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
