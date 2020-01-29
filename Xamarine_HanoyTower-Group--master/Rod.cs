using System;
using System.Collections.Generic;
using UIKit;

namespace XamarinAnimationIOS_HanoyTower_
{
    public class Rod
    {
        public nfloat X { get; }
        public nfloat Y { get; set; }

        public Stack<UIView> Disks { get; set; }

        public Rod(nfloat x, nfloat y)
        {
            X = x;
            Y = y;

            Disks = new Stack<UIView>();
        }
    }
}