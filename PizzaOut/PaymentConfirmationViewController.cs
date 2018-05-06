﻿using Foundation;
using System;
using System.Globalization;
using PizzaData.models;
using UIKit;

namespace PizzaOut
{
    public partial class PaymentConfirmationViewController : UIViewController
    {
        private Order _order;

        public PaymentConfirmationViewController (IntPtr handle) : base (handle)
        {
        }

        public void SetOrderItems(Order order)
        {
            _order = order;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (_order != null)
            {
                Title = "Order Payment";

                lblOrderHeader.Text = "Pay for order number :" + _order.ORDER_ID;
                txtOrderNumber.Text = _order.ORDER_ID.ToString();
                txtOrderTotal.Text = _order.ORDER_TOTAL.ToString("C", CultureInfo.CreateSpecificCulture("en-US"));
            }
        }
    }
}