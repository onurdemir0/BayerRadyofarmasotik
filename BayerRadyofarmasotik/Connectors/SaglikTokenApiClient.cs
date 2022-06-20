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
    public class SaglikTokenApiClient
    {
        private ILoggerService _logger;
        public SaglikTokenApiClient(ILoggerService logger)
        {
            _logger = logger;
        }

        public LoginResponse Login(UserForLogin user, out System.Net.HttpStatusCode status)
        {
            try
            {
                string tokenService = Resources.accessToken;
                var data = JsonConvert.SerializeObject(user);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(tokenService, data, out status);
                if (response != null)
                {
                    var configData = Operations.ReadFile("config.txt");
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response);
                    return loginResponse;
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
