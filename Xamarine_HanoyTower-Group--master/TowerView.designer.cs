// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace XamarinAnimationIOS_HanoyTower_
{
    [Register ("TowerView")]
    partial class TowerView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView _viewForTowers { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton PauseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StartButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton StopButton { get; set; }

        [Action ("PauseButton_Tapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void PauseButton_Tapped (UIKit.UIButton sender);

        [Action ("ReverseButton_Tapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ReverseButton_Tapped (UIKit.UIButton sender);

        [Action ("StartButton_Tapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StartButton_Tapped (UIKit.UIButton sender);

        [Action ("StopButton_Tapped:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void StopButton_Tapped (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (_viewForTowers != null) {
                _viewForTowers.Dispose ();
                _viewForTowers = null;
            }

            if (PauseButton != null) {
                PauseButton.Dispose ();
                PauseButton = null;
            }

            if (StartButton != null) {
                StartButton.Dispose ();
                StartButton = null;
            }

            if (StopButton != null) {
                StopButton.Dispose ();
                StopButton = null;
            }
        }
    }
}