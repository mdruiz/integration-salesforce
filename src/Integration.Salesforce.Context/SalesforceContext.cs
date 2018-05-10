using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Integration.Salesforce.Context
{
    public class SalesforceContext <TModel> where TModel : AModel, new()
    {
        // SalesForceConfig contains all necessary information to connect to Salesforce API and retrieve data.
        protected readonly Dictionary<string, string> SalesforceConfig;

        // SalesForceUrls contains all the URLs required for all HTTP requests
        protected readonly Dictionary<string, string> SalesforceUrls;
        
        public SalesforceContext(IOptions<Settings> settings)
        {
            SalesforceConfig = new Dictionary<string, string>();
            SalesforceUrls = new Dictionary<string, string>();

            SalesforceConfig.Add("grant_type", "password");
            SalesforceConfig.Add("client_id", settings.Value.ClientId);
            SalesforceConfig.Add("client_secret", settings.Value.ClientSecret);
            SalesforceConfig.Add("username", settings.Value.Username);
            SalesforceConfig.Add("password", settings.Value.Password);

            SalesforceUrls.Add("login", settings.Value.LoginUrl);
            SalesforceUrls.Add("resource_base", settings.Value.ResourceUrlExtension);

            Task.Run(() => GetAuthToken()).Wait();
        }

        /// <summary>
        /// login to salesforce and retrieves authorization token
        /// </summary>
        private async Task GetAuthToken()
        {
            HttpClient authClient = new HttpClient();

            HttpContent content = new FormUrlEncodedContent(SalesforceConfig);
            string loginURL = SalesforceUrls["login"];

            HttpResponseMessage message = await authClient.PostAsync(loginURL, content);
            string responseString = await message.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(responseString);
            SalesforceConfig.Add("access_token", obj["access_token"].ToString() );
            SalesforceUrls.Add("instance_url", obj["instance_url"].ToString() );
        }

         /// <summary>
        /// Get all info for object of type AModel from salesforce.
        /// </summary>
        /// <param name="str"> specify the data to retrieve from Salesforce  </param>
        /// <returns> returns IEnumerable of AModels </returns>
        internal async Task<IEnumerable<TModel>> RetrieveFromSalesforce(string query)
        {
            List<TModel> modelList = new List<TModel>();
            HttpClient queryClient = new HttpClient();

            string restQuery = SalesforceUrls["instance_url"] + SalesforceUrls["resource_base"] + query;
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, restQuery);
            req.Headers.Add("Authorization", "Bearer " + SalesforceConfig["access_token"]);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await queryClient.SendAsync(req);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonResponse);

            JArray jsonResponseArray = JArray.Parse(jsonObject["records"].ToString());
            IEnumerable<JObject> allModels = await GetAll(jsonResponseArray);

            foreach(var item in allModels)
            {
                var model = new TModel();
                model.MapJsonToModel(item);
                modelList.Add(model);
            }
            return modelList;
        }
        
        private async Task<IEnumerable<JObject>> GetAll(JArray contactList)
        {
            HttpClient queryClient = new HttpClient();
            List<JObject> modelList = new List<JObject>();

            foreach (var contact in contactList)
            {
                HttpRequestMessage contactRequest = new HttpRequestMessage();
                contactRequest.Method = HttpMethod.Get;
                contactRequest.Headers.Add("Authorization", "Bearer " + SalesforceConfig["access_token"]);
                contactRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string contactURL = SalesforceUrls["instance_url"] + contact["attributes"]["url"];
                contactRequest.RequestUri = new Uri(contactURL);

                HttpResponseMessage response = await queryClient.SendAsync(contactRequest);
                string jsonContactAsString = await response.Content.ReadAsStringAsync();

                JObject jsonContact = JObject.Parse(jsonContactAsString);
                modelList.Add(jsonContact);
            }
            return modelList;
        }

    }
}