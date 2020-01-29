using System;
using UIKit;

namespace XamarinAnimationIOS_HanoyTower_
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle) { }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }

        partial void StartButton_Tapped(UIButton sender)
        {
            var towerViewViewController = Storyboard.InstantiateViewController("_towerView") as TowerView;
            towerViewViewController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            towerViewViewController.DisksAmount = int.Parse(_txtFildAmountTower.Text);
            PresentViewController(towerViewViewController, true, null);
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
        }
    }
}