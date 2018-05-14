using Foundation;
using Plugin.Messaging;
using UIKit;

namespace PizzaOut.IOS.DataManager
{
    public class MessagingActions
    {
        public bool MakePhoneCall(string numberToDial)
        {

            // Make Phone Call
            var url = new NSUrl("tel:"+numberToDial);
            return UIApplication.SharedApplication.OpenUrl(url);
  
        }

        public bool SendEmail(string recipient,string from,string subject,string body)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
               // emailMessenger.SendEmail("to.plugins@xamarin.com", "Xamarin Messaging Plugin", "Well hello there from Xam.Messaging.Plugin");

                // Alternatively use EmailBuilder fluent interface to construct more complex e-mail with multiple recipients, bcc, attachments etc.
                var email = new EmailMessageBuilder()
                    .To(recipient)
                    //.Cc("cc.plugins@xamarin.com")
                    //.Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
                    .Subject(subject)
                    .Body(body)
                    .Build();

                emailMessenger.SendEmail(email);

                return true;
            }

            return false;
        }

        public static void ShowAlert(string title, string message)
        {
#pragma warning disable 618
            new UIAlertView(title, message, null, "OK", null).Show();
#pragma warning restore 618
        }
    }
}