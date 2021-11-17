using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Firebase.Messaging;
using Xamarin.Essentials;

namespace XamarinFCMDemo.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        AndroidNotificationManager androidNotification = new AndroidNotificationManager();
        public override void OnMessageReceived(RemoteMessage message)
        {
            string title = message.GetNotification().Title;
            string body = message.GetNotification().Body;
            //  IDictionary<string, string> MensajeData = message.Data;

            string Titulo = title;
            string SubTitulo = body;

            androidNotification.CrearNotificacionLocal(Titulo, SubTitulo);
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            Preferences.Set("TokenFirebase", token);
            sendRegisterToken(token);
        }
        public void sendRegisterToken(string token)
        {
            //Tu código para registrar el token a tu servidor y base de datos
        }
    }
}