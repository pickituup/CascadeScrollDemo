using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App2 {
    public class MyRelativeLayout : RelativeLayout {

        public static readonly BindableProperty CriticalViewProperty =
            BindableProperty.Create("CriticalView",
                typeof(Element),
                typeof(MyRelativeLayout),
                defaultValue: null);

        public bool IsPrepared {
            get; private set;
        }

        public double YCriticalPoint {
            get; private set;
        }

        public Element CriticalView {
            get => (Element)GetValue(CriticalViewProperty);
            set => SetValue(CriticalViewProperty, value);
        }

        public void Init() {
            if (!IsPrepared) {
                IsPrepared = true;

                if (CriticalView != null) {

                    VisualElement parentOfCriticalView = CriticalView.Parent as VisualElement;
                    double calculatedYCriticalPoint = ((VisualElement)CriticalView).Y;

                    try {
                        while (parentOfCriticalView != null && (parentOfCriticalView != this)) {
                            calculatedYCriticalPoint += parentOfCriticalView.Y;

                            parentOfCriticalView = (VisualElement)parentOfCriticalView.Parent;
                        }
                    }
                    catch (Exception) {
                        parentOfCriticalView = null;
                        calculatedYCriticalPoint = 0;
                    }

                    YCriticalPoint = calculatedYCriticalPoint;

                    this.TranslationY = YCriticalPoint;
                    RelativeLayout.SetYConstraint(Children[0], Constraint.RelativeToParent((p) => YCriticalPoint * (-1)));
                    RelativeLayout.SetHeightConstraint(Children[0], Constraint.RelativeToParent((p) => p.Height + YCriticalPoint));
                }
            }
        }
    }
}
