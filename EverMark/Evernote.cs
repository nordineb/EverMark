using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace EverMark
{
    public static class Evernote
    {
        private static string _noteStoreUrl;

        public static UserStore.Client GetUserStore()
        {
            var userStoreUrl = new Uri(Configuration.EvernoteUrl + "/edam/user");
            var userStoreTransport = new THttpClient(userStoreUrl);
            var userStoreProtocol = new TBinaryProtocol(userStoreTransport);
            return new UserStore.Client(userStoreProtocol);
        }

        public static NoteStore.Client GetNoteStore(string token)
        {
            if (string.IsNullOrEmpty(_noteStoreUrl))
            {
                var userStore = GetUserStore();
                _noteStoreUrl = userStore.getNoteStoreUrl(token);
            }

            var noteStoreTransport = new THttpClient(new Uri(_noteStoreUrl));
            var noteStoreProtocol = new TBinaryProtocol(noteStoreTransport);
            return new NoteStore.Client(noteStoreProtocol);
        }

    }
}