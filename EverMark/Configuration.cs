using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using ePunkt.Utilities;

namespace EvernoteMvcExample
{
    public static class Configuration
    {
        public static string EvernoteUrl = Settings.Get("EvernoteUrl", "");
        public static string EvernoteConsumerKey = Settings.Get("EvernoteConsumerKey", "");
        public static string EvernoteConsumerSecret = Settings.Get("EvernoteConsumerSecret", "");

        public static string ElmahLogId = Settings.Get("ElmahLogId", "");

        public static ServiceProviderDescription GetOauthConfiguration()
        {
            return new ServiceProviderDescription
            {
                RequestTokenEndpoint = new MessageReceivingEndpoint(EvernoteUrl + "/oauth", HttpDeliveryMethods.PostRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint(EvernoteUrl + "/OAuth.action", HttpDeliveryMethods.PostRequest),
                AccessTokenEndpoint = new MessageReceivingEndpoint(EvernoteUrl + "/oauth", HttpDeliveryMethods.PostRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
                ProtocolVersion = ProtocolVersion.V10a
            };
        }
    }
}