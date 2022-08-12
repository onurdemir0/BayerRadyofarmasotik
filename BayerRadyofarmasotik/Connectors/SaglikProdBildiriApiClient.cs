using BayerRadyofarmasotik.Entities;
using BayerRadyofarmasotik.FileHelper;
using BayerRadyofarmasotik.Logger;
using BayerRadyofarmasotik.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BayerRadyofarmasotik.Connectors
{
    public class SaglikProdBildiriApiClient
    {
        private ILoggerService _logger;
        Dictionary<string, string> configData = Operations.ReadFile("config.txt");
        public SaglikProdBildiriApiClient(ILoggerService logger)
        {
            _logger = logger;
        }

        public ProductsResponse Bildiri(Products products, string accessToken)
        {
            try
            {
                string baseAddress = Resources.prodApiServiceEndPoint;
                string bildiriService = Resources.prodBildirimService;
                var data = JsonConvert.SerializeObject(products);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(baseAddress, bildiriService, data, accessToken);
                if (response != null)
                {
                    //var configData = Operations.ReadFile("config.txt");
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var bildiriResponse = JsonConvert.DeserializeObject<ProductsResponse>(response);
                    return bildiriResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("HATA!! " + ex.Message, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                throw ex;
            }
            return default;
        }

        public ProductsResponse IptalBildiri(ProductsIptal products, string accessToken)
        {
            try
            {
                string baseAddress = Resources.prodApiServiceEndPoint;
                string bildiriService = Resources.prodIptalService;
                var data = JsonConvert.SerializeObject(products);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(baseAddress, bildiriService, data, accessToken);
                if (response != null)
                {
                    //var configData = Operations.ReadFile("config.txt");
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var bildiriIptalResponse = JsonConvert.DeserializeObject<ProductsResponse>(response);
                    return bildiriIptalResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog("HATA!! " + ex.Message, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                throw ex;
            }
            return default;
        }
    }
}
