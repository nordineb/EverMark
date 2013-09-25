using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace EverMark
{
    public class JsonTokenManager : IConsumerTokenManager
    {
        private readonly HttpRequestBase _request;

        public JsonTokenManager(HttpRequestBase request)
        {
            _request = request;
            ConsumerKey = Configuration.EvernoteConsumerKey;
            ConsumerSecret = Configuration.EvernoteConsumerSecret;
        }

        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }

        private string Path
        {
            get
            {
                return _request.MapPath("~/App_Data/Tokens.json");
            }
        }

        private IDictionary<string, string> LoadJson()
        {
            var dictionary = File.Exists(Path) ? JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Path)) : new Dictionary<string, string>();
            return dictionary;
        }

        private void SaveJson(IDictionary<string, string> dictionary)
        {
            var directory = System.IO.Path.GetDirectoryName(Path) ?? "";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(Path, JsonConvert.SerializeObject(dictionary));
        }

        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            var dictionary = LoadJson();
            dictionary[accessToken] = accessTokenSecret;
            SaveJson(dictionary);
        }

        public string GetTokenSecret(string token)
        {
            var dictionary = LoadJson();
            if (dictionary.ContainsKey(token))
                return dictionary[token];
            return null;
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            var dictionary = LoadJson();
            dictionary[response.Token] = response.TokenSecret;
            SaveJson(dictionary);
        }
    }
}