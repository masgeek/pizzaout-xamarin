using System;
using UIKit;
using Valore.IOSDropDown;
using System.Collections.Generic;

namespace Valore.DropDownDemo
{
	public partial class Valore_DropDownDemoViewController : UIViewController
	{
		private DropDownList ddl;

		public Valore_DropDownDemoViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			ddl = GetDDL1 ();
			ddl.DropDownTopInset = 60;
			ddl.DropDownListChanged += (e, a) => {
				var index = e; // e is the index selected
				var strValue = a.DisplayText; //a is the dropdown list item object
				var id = a.Id;
				lblSelection.Text = string.Format ("Id: {0} => Text: {1}", id, strValue);
				ddl.Toggle ();
			};

			NavigationItem.SetRightBarButtonItem (
				new UIBarButtonItem (UIImage.FromFile ("top_charts.png"), UIBarButtonItemStyle.Plain,
					(sender, e) => {
						ddl.Toggle ();
					}), false);
		}

		private DropDownList GetDDL1 ()
		{
			return new DropDownList (this.View, GetList1 ().ToArray ()) {
				BackgroundColor = UIColor.FromRGB (220, 220, 220),
				TextColor = UIColor.Blue,
				Opacity = 1f,
				TintColor = UIColor.Blue,
				ImageColor = UIColor.Blue

			};
		}

		/// <summary>
		/// Gets the list1.
		/// </summary>
		/// <returns>The list1.</returns>
		private List<DropDownListItem> GetList1 ()
		{
			var list = new List<DropDownListItem> ();
			list.Add (new DropDownListItem () {
				Id = "1",
				DisplayText = "View Animal Selections",
				Image = UIImage.FromBundle ("footprint.png")
			});
			list.Add (new DropDownListItem () {
				Id = "2",
				DisplayText = "Bugs Are The Bomb",
				Image = UIImage.FromBundle ("bug.png"),
				IsSelected = true
			});
			list.Add (new DropDownListItem () {
				Id = "3",
				DisplayText = "Connect With Friends",
				Image = UIImage.FromBundle ("facebook.png")
			});
			list.Add (new DropDownListItem () {
				Id = "4",
				DisplayText = "What Can Hurt You",
				Image = UIImage.FromBundle ("toxic.png")
			});
			list.Add (new DropDownListItem () {
				Id = "5",
				DisplayText = "Your Connections",
				Image = UIImage.FromBundle ("arrow.png")
			});
			list.Add (new DropDownListItem () {
				Id = "6",
				DisplayText = "Danger Will Robinson",
				Image = UIImage.FromBundle ("fire.png")
			});
			return list;
		}

	}
}

