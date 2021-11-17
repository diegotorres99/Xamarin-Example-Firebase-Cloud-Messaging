using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFCMDemo
{
    public partial class MainPage : ContentPage
    {
        // More info: https://firebase.google.com/docs/cloud-messaging

        //Past your API KEY from device and ServerKey from FCM console
        private string Emulador = "dFXQOpwQTmikFO-BVWScH8:APA91bF2KChkYFwY2pCr13WjpS0CvhYDl2_XjUfxXVBoPFa2sDHOTHsg451HVxwXfBapMqdergKC6KJY31vJGGvvMI-s0rvcy-tOHnyVU1CawpaDaKleu0zw86sOoIaE93iLsRpn0d8N";
        private string SERVERKEY = "AAAA3GsubPo:APA91bHHF6CLqsN1r8MG1JXHVpj5FTxg4HV8jYg7mAPvSRzi4zXfqQePC8cBVX4HEAmorru2hRIM6hMiP7oq_g_leLoTXHe2_qThVgHGkCmjv6wAqhKTwPKsKwfnIFM_M7wk2aNSh_5z";
        private string MyPhoneA10 ="d-YTGQNEQ2a1LeSNEVkTG7:APA91bFfjyA3pIW1iEd9_DuQX_D-0lx4iAq7vCGuUsnkzZg3U_7QR_r_HWHPfc2k9T0l1KG9XizJDkKeJrB09BOdVaKbHyXed5c9a9hsAZqF8WPaeE-CkVF4MP8yMFvTVahqYoDhl7wv";
        public MainPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Button to call send Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await NotifyAsync(MyPhoneA10, "Example Push Notifications", "This is Push Notifications from FCM");
        }

        /// <summary>
        /// Method for send Push Notifications
        /// </summary>
        /// <param name="to">Token device to</param>
        /// <param name="title">Title for notification show</param>
        /// <param name="body">Info for show in notification</param>
        /// <returns></returns>
        public async Task<bool> NotifyAsync(string to, string title, string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", "AAAA3GsubPo:APA91bHHF6CLqsN1r8MG1JXHVpj5FTxg4HV8jYg7mAPvSRzi4zXfqQePC8cBVX4HEAmorru2hRIM6hMiP7oq_g_leLoTXHe2_qThVgHGkCmjv6wAqhKTwPKsKwfnIFM_M7wk2aNSh_5z");

                // Get the sender id from FCM console A10=d-YTGQNEQ2a1LeSNEVkTG7:APA91bFfjyA3pIW1iEd9_DuQX_D-0lx4iAq7vCGuUsnkzZg3U_7QR_r_HWHPfc2k9T0l1KG9XizJDkKeJrB09BOdVaKbHyXed5c9a9hsAZqF8WPaeE-CkVF4MP8yMFvTVahqYoDhl7wv
                //EMULADOR dFXQOpwQTmikFO-BVWScH8:APA91bF2KChkYFwY2pCr13WjpS0CvhYDl2_XjUfxXVBoPFa2sDHOTHsg451HVxwXfBapMqdergKC6KJY31vJGGvvMI-s0rvcy-tOHnyVU1CawpaDaKleu0zw86sOoIaE93iLsRpn0d8N
                var senderId = string.Format("id={0}", "dFXQOpwQTmikFO-BVWScH8:APA91bF2KChkYFwY2pCr13WjpS0CvhYDl2_XjUfxXVBoPFa2sDHOTHsg451HVxwXfBapMqdergKC6KJY31vJGGvvMI-s0rvcy-tOHnyVU1CawpaDaKleu0zw86sOoIaE93iLsRpn0d8N");

                var data = new
                {
                    to, // Recipient device token
                    notification = new { title, body }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            // Use result.StatusCode to handle failure
                            // Your custom error handler here
                           // _logger.LogError($"Error sending notification. Status Code: {result.StatusCode}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Exception thrown in Notify Service: {ex}");
            }

            return false;
        }

    }
}
