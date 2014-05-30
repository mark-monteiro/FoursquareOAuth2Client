using DotNetOpenAuth.Messaging;
using FoursquareOAuth2Client.ApiClasses;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace FoursquareOAuth2Client
{
    public class FSqApiInterface
    {
        private FoursqaureApiConfig _config;
        private IAccessTokenManager _tokenManager;

        #region Properties
        public string ClientId
        {
            get { return _config.ClientId; }
        }

        public string ClientSecret
        {
            get { return _config.ClientSecret; }
        }

        public string PushSecret
        {
            get { return _config.PushSecret; }
        }

        public string ApiEndpoint
        {
            get { return _config.ApiEndpoint; }
        }

        public string ApiVersion
        {
            get { return _config.ApiVersion; }
        }

        public string ProfileUrlPrefix
        {
            get { return _config.ProfileUrlPrefix; }
        }
        #endregion

        /// <summary>
        /// Constructor. Uses default configuration section name of "foursqareApi"
        /// </summary>
        public FSqApiInterface(IAccessTokenManager TokenManager)
            : this(TokenManager, "foursqareApi")
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public FSqApiInterface(IAccessTokenManager TokenManager, string configSectionName)
        {
            _tokenManager = TokenManager;
            _config = (FoursqaureApiConfig)System.Configuration.ConfigurationManager.GetSection("foursqareApi");
        }

        /// <summary>
        /// Query the database to get user information
        /// </summary>
        public FSqUser GetUserData(string accessToken)
        {
            JObject response = queryApi(accessToken, "/users/self");
            return response == null ? null : response["user"].ToObject<FSqUser>();
        }

        /// <summary>
        /// Returns the url to the public profile of a Foursquare user
        /// </summary>
        public Uri GetProfileUrl(string profileId)
        {
            return new Uri(ProfileUrlPrefix + profileId);
        }

        /// <summary>
        /// Query the database at the specified endpoint, with the provided access token
        /// </summary>
        public JObject queryApi(string accessToken, string apiPath)
        {
            UriBuilder uriBuilder = getUriBuilder(apiPath, accessToken);
            return sendRequest(uriBuilder.Uri);
        }

        #region Helpers

        /// <summary>
        /// Gets a uri builder with the specified Foursquare API path and access token
        /// Automatically appends the version parameter
        /// </summary>
        private UriBuilder getUriBuilder(string path, string accessToken)
        {
            UriBuilder uriBuilder = new UriBuilder(ApiEndpoint + path);
            uriBuilder.AppendQueryArgument("oauth_token", accessToken);
            uriBuilder.AppendQueryArgument("v", ApiVersion);
            return uriBuilder;
        }

        /// <summary>
        /// Send a request to the forusquare api using the provided uri
        /// </summary>
        /// <returns>The JSON object returned by the api</returns>
        private JObject sendRequest(Uri uri)
        {
            using (WebClient wc = new WebClient())
            {
                string textResponse = wc.DownloadString(uri);
                return DeserializeResponse(textResponse);
            }
        }

        /// <summary>
        /// Takes the string response returned by the Foursquare API and converts it to
        /// a JObject after checking that the request succeeded
        /// </summary>
        private JObject DeserializeResponse(string textResponse)
        {
            FSqApiResponse response = JsonConvert.DeserializeObject<FSqApiResponse>(textResponse);

            if (response.Meta.code == 401 && response.Meta.errorType == "invalid_auth")
            {
                //TODO: handle this
            }

            if (response.Meta.code != 200)
            {
                return null;
            }
            else
            {
                return response.Response;
            }
        }

        #endregion
    }
}