using System;
using System.Collections.Generic;
using System.Text;

namespace RestService.models
{
    public class Payment
    {
        public int PAYMENT_ID, ORDER_ID;
        public String PAYMENT_CHANNEL, PAYMENT_REF, PAYMENT_STATUS, PAYMENT_DATE, PAYMENT_NOTES, PAYMENT_NUMBER;
        public double PAYMENT_AMOUNT;
    }
}
