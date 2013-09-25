using System;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using System.Web.Mvc;
using ApplicationException = Elmah.ApplicationException;

namespace EverMark.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool loadNotes = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    var noteStore = Evernote.GetNoteStore(User.Identity.Name);
                    noteStore.listNotebooks(User.Identity.Name);

                    return RedirectToAction("Index", "Notes");
                }
                catch
                {
                    return RedirectToAction("Logoff");
                }
            }

            return View();
        }


        public ActionResult Error()
        {
            throw new ApplicationException("Test exception on " + DateTime.Now + ".");
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            var oauthConsumer = new WebConsumer(Configuration.GetOauthConfiguration(), new JsonTokenManager(Request));
            var oauthResponse = oauthConsumer.ProcessUserAuthorization();

            if (oauthResponse != null)
            {
                FormsAuthentication.SetAuthCookie(oauthResponse.AccessToken, true);
                return RedirectToAction("Index");
            }

            var oauthRequest = oauthConsumer.PrepareRequestUserAuthorization(Request.Url, null, null);
            return oauthConsumer.Channel.PrepareResponse(oauthRequest).AsActionResult();
        }
    }
}
