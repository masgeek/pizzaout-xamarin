using Foundation;
using System;
using System.Globalization;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using PizzaData.models;
using PizzaOut.DataManager;
using UIKit;

namespace PizzaOut
{
    public partial class PaymentConfirmationViewController : UIViewController
    {
        private Order _order;
        private MessagingActions _messagingActions;
        private string _paymentUssd;
        public PaymentConfirmationViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetOrderItems(Order order)
        {
            _order = order;
            _messagingActions = new MessagingActions();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (_order != null)
            {
                double totalAmount = _order.ComputeOrderTotal();
                _paymentUssd = $"{_order.USSD_NUMBER}{totalAmount}#";

                Title = "Order Payment";


                txtOrderNumber.Text = _order.ORDER_ID.ToString();
                txtOrderTotal.Text = totalAmount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

                lblOrderHeader.Text = "Pay for order number :" + _order.ORDER_ID;
                lblHelpLine.Text = $"Call us on {UserSession.HelpLine()}";
                lblPaymentNumber.Text = "Payment Number is :" + _paymentUssd;

                btnPayOrder.SetTitle($"Tap to pay for order number :{_order.ORDER_ID}", UIControlState.Normal);

                txtOrderTotal.Enabled = false;
                txtOrderNumber.Enabled = false;
                btnPayOrder.TouchUpInside += (e, s) =>
                {
                    //let us launch the dialler
                    var launched = _messagingActions.MakePhoneCall(_paymentUssd);
                    if (!launched)
                    {
                        MessagingActions.ShowAlert("Dialler Not opened","Unable to open dialling keypad on this device");
                        Analytics.TrackEvent("Unable to launch dialler for user id "+UserSession.GetUserId());
                    }
                };

               
            }
        }
    }
}