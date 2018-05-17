using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace PizzaOut.IOS.UIHelpers
{
    public class CatchEnterDelegate : UITextFieldDelegate
    {
        public override bool ShouldReturn(UITextField textField)
        {
            textField.ResignFirstResponder();
            return true;
        }
    }
}