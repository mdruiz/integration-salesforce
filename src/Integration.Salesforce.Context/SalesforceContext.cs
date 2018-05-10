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

            // Group the salesforce request key-value pairs into dictionary
            SalesforceConfig.Add("grant_type", "password");
            SalesforceConfig.Add("client_id", settings.Value.ClientId);
            SalesforceConfig.Add("client_secret", settings.Value.ClientSecret);
            SalesforceConfig.Add("username", settings.Value.Username);
            SalesforceConfig.Add("password", settings.Value.Password);

            // Group the URLs together
            SalesforceUrls.Add("login", settings.Value.LoginUrl);
            SalesforceUrls.Add("resource_base", settings.Value.ResourceUrlExtension);

            // Retrieve authorization token from salesforce
            Task.Run(() => GetAuthToken()).Wait();
        }

        /// <summary>
        /// login to salesforce and retrieves authorization token
        /// </summary>
        private async Task GetAuthToken()
        {
            // Client used for login
            HttpClient authClient = new HttpClient();

            // Content contains all the key-value pairs needed in the request body
            HttpContent content = new FormUrlEncodedContent(SalesforceConfig);

            // Build the URL for login by concatenating the base URL for Salesforce and additional url for login purposes
            string loginURL = SalesforceUrls["login"];

            // POST to the login URL using the Content as the request body
            HttpResponseMessage message = await authClient.PostAsync(loginURL, content);

            string responseString = await message.Content.ReadAsStringAsync();

            // Request body for login will contain the access_token
            JObject obj = JObject.Parse(responseString);
            SalesforceConfig.Add("access_token", obj["access_token"].ToString() );
            SalesforceUrls.Add("instance_url", obj["instance_url"].ToString() );
        }

         /// <summary>
        /// Get all info for object of type AModel from salesforce.
        /// </summary>
        /// <param name="str"> specify the data to retrieve from Salesforce  </param>
        /// <returns> returns IEnumerable of AModels </returns>
        internal async Task<IEnumerable<TModel>> RetrieveFromSalesforce(string str)
        {
            List<TModel> modelList = new List<TModel>();

            // Client used to GET the data from Salesforce
            HttpClient queryClient = new HttpClient();

            string query = "SELECT Name FROM " + str;

            // Build the url using the URLs and URL extensions from appsettings.json
            string restQuery = SalesforceUrls["instance_url"] + SalesforceUrls["resource_base"] + query;

            // Define headers for the GET request
            // Authorization header and Accept json header
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, restQuery);
            req.Headers.Add("Authorization", "Bearer " + SalesforceConfig["access_token"]);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await queryClient.SendAsync(req);

            // GET request returns a list of models from Salesforce
            // Parse the response into a JSON object.
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(jsonResponse);

            // Take records from json response
            JArray jsonResponseArray = JArray.Parse(jsonObject["records"].ToString());

            // Returns an IEnum of all requested info as JObjects
            IEnumerable<JObject> allModels = await GetAll(jsonResponseArray);

            //TODO: map JObjects to actual object models
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

            // Iterate through each contact in the JSON array
            // Each contact contains a URL pointing to its resource
            // The URL can be accessed through the url property of the attributes property within the contact
            foreach (var contact in contactList)
            {
                // Setup the base request for GET Http Request for each Contact
                // All requests need the Authorization header and all accepts json
                HttpRequestMessage contactRequest = new HttpRequestMessage();
                contactRequest.Method = HttpMethod.Get;
                contactRequest.Headers.Add("Authorization", "Bearer " + SalesforceConfig["access_token"]);
                contactRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build each contact url using the url found in the attributes property
                string contactURL = SalesforceUrls["instance_url"] + contact["attributes"]["url"];

                contactRequest.RequestUri = new Uri(contactURL);

                // Send the request to the specific contact
                HttpResponseMessage response = await queryClient.SendAsync(contactRequest);
                string jsonContactAsString = await response.Content.ReadAsStringAsync();

                // Receive the contact as a JSON object and map it to object model
                // Add the mapped object to the list of mapped models
                JObject jsonContact = JObject.Parse(jsonContactAsString);
                modelList.Add(jsonContact);
            }

            return modelList;
        }

    }
}