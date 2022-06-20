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
    public class SaglikBildiriApiClient
    {
        private ILoggerService _logger;
        public SaglikBildiriApiClient(ILoggerService logger)
        {
            _logger = logger;
        }

        public ProductsResponse Bildiri(Products products, string accessToken)
        {
            try
            {
                string bildiriService = Resources.bildirimService;
                var data = JsonConvert.SerializeObject(products);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(bildiriService, data, accessToken);
                if (response != null)
                {
                    var configData = Operations.ReadFile("config.txt");
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var bildiriResponse = JsonConvert.DeserializeObject<ProductsResponse>(response);
                    return bildiriResponse;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return default;
        }
    }
}
