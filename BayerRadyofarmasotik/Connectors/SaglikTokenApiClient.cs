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
            var configData = Operations.ReadFile("config.txt");
            try
            {
                string baseAddress = Resources.testApiServiceEndPoint;
                string tokenService = Resources.accessToken;
                var data = JsonConvert.SerializeObject(user);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(baseAddress, tokenService, data, out status);
                if (response != null)
                {
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response);
                    return loginResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"HATA!! Login sırasında hata ile karşılaşıldı. Hata mesajı: {ex.Message}", configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                throw ex;
            }
            return default;
        }

        public LoginResponse LoginToProd(UserForLogin user, out System.Net.HttpStatusCode status)
        {
            var configData = Operations.ReadFile("config.txt");
            try
            {
                string baseAddress = Resources.prodApiServiceEndPoint;
                string tokenService = Resources.prodAcccessToken;
                var data = JsonConvert.SerializeObject(user);
                SaglikApiBaseClient saglikApiBaseClient = new SaglikApiBaseClient();
                var response = saglikApiBaseClient.PostData(baseAddress, tokenService, data, out status);
                if (response != null)
                {
                    _logger.WriteLog("Request: " + data + "\n" + "Response: " + response, configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response);
                    return loginResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLog($"HATA!! Login sırasında hata ile karşılaşıldı. Hata mesajı: {ex.Message}", configData["log_path"].Substring(1, configData["log_path"].Length - 2), configData["environment"].Substring(1, configData["environment"].Length - 2));
                throw ex;
            }
            return default;
        }
    }
}
