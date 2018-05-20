using System;
using System.Globalization;
using Microsoft.AppCenter.Analytics;
using PizzaData.models;
using PizzaOut.IOS.DataManager;
using PizzaOut.IOS.UIHelpers;
using UIKit;

namespace PizzaOut.IOS
{
    public partial class PaymentConfirmationViewController : UIViewController
    {
        private Order _order;
        private MessagingActions _messagingActions;
        private string _paymentUssd;
        private LoadingOverlay _loadingOverlay;
        private double _totalAmount;
        public PaymentConfirmationViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetOrderItems(Order order, double orderTotal)
        {
            _order = order;
            _messagingActions = new MessagingActions();
            _totalAmount = orderTotal;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (_order != null)
            {
                if (_totalAmount <= 0)
                {
                    _totalAmount = _order.ComputeOrderTotal();
                }

                _paymentUssd = $"{_order.USSD_NUMBER}{_totalAmount}#";

                Title = "Order Payment";


                TxtOrderNumber.Text = _order.ORDER_ID.ToString();
                TxtOrderTotal.Text = _totalAmount.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));

                LblOrderSummary.Text = $"Pay for order number :{_order.ORDER_ID}";
                //lblHelpLine.Text = $"Call us on {UserSession.HelpLine()}";
                LblUssdNumber.Text = $"Payment Number is :{_paymentUssd}";

                BtnPayOrder.SetTitle($"Tap to pay for order number :{_order.ORDER_ID}", UIControlState.Normal);

                TxtOrderTotal.Enabled = false;
                TxtOrderNumber.Enabled = false;
                BtnPayOrder.TouchUpInside += (e, s) =>
                {
                    //let us launch the dialler
                    var launched = _messagingActions.MakePhoneCall(_paymentUssd);
                    
                    if (!launched)
                    {
                        MessagingActions.ShowAlert("Dialler Not opened","Unable to open dialling keypad on this device");
                        Analytics.TrackEvent($"Unable to launch dialler for user id {UserSession.GetUserId()}");
                    }
                };

                BtnClose.TouchUpInside += (e, s) =>
                {
                    //dismiss the view controller
                    DismissModalViewController(true);
                };

            }
        }

        
    }
}