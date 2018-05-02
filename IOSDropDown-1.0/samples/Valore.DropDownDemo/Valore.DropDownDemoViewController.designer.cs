// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace Valore.DropDownDemo
{
	[Register ("Valore_DropDownDemoViewController")]
	partial class Valore_DropDownDemoViewController
	{
		[Outlet]
		UIKit.UILabel lblSelection { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblSelection != null) {
				lblSelection.Dispose ();
				lblSelection = null;
			}
		}
	}
}
