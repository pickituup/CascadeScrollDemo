using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using App2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(App2.MyRelativeLayout), typeof(MyRelativeLayoutRenderer))]
namespace App2.Droid {
    public class MyRelativeLayoutRenderer : ViewRenderer {

        public MyRelativeLayoutRenderer(Context context) : base(context) {
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            
        }

        protected override void OnDraw(Canvas canvas) {
            base.OnDraw(canvas);

            if (Element != null) {
                ((MyRelativeLayout)Element).Init();
            }
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec) {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b) {
            base.OnLayout(changed, l, t, r, b);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e) {
            base.OnElementChanged(e);

            if (Control == null) {
                SetNativeControl(new Android.Views.View(Context));
            }

            Control?.SetOnTouchListener(MainActivity.MY_TOUCH_LISTENER);
            MainActivity.MY_TOUCH_LISTENER.FOR_PARENT = Control;
        }
    }

    public class MyTouchListener : Java.Lang.Object, IOnTouchListener {

        public Android.Views.View FOR_LIST;
        public Android.Views.View FOR_PARENT;

        float _lastRawY = float.MinValue;
        bool _keep_native_gesture_offset_tracking = true;
        bool _block_list_events_handling = false;

        public MyTouchListener() {
            MessagingCenter.Subscribe<object, bool>(this, "KEEP_NATIVE_GESTURE_OFFSET_TRACKING", (sender, e) => {
                _keep_native_gesture_offset_tracking = e;
            });

            MessagingCenter.Subscribe<object, bool>(this, "BLOCK_LIST_EVENTS_HANDLING", (sender, e) => {
                _block_list_events_handling = e;
            });
        }

        public bool OnTouch(Android.Views.View v, MotionEvent e) {

            switch (e.Action) {
                case MotionEventActions.Down:
                    _lastRawY = e.RawY;
                    break;
                case MotionEventActions.Move:
                    if (_keep_native_gesture_offset_tracking) {
                        if (_lastRawY != e.RawY) {

                            float offset = e.RawY - _lastRawY;

                            MessagingCenter.Send<object, CooArg>(this, "TEST", new CooArg() { RawY = offset, FormsY = offset / 3 });
                        }

                        _lastRawY = e.RawY;
                    }
                    else {
                        _lastRawY = e.RawY;

                        MessagingCenter.Send<object, object>(this, "HIDE_CLAMP", null);
                    }
                    break;
                case MotionEventActions.Up:
                    _lastRawY = float.MinValue;
                    break;
                default:
                    break;
            }

            if (v is Android.Widget.ListView) {
                //if (!_block_list_events_handling) {
                //    return true;
                //}
                //else {
                //    return false;
                //}

                //return _block_list_events_handling;
                return false;
            }
            else {
                return true;
            }
        }
    }
}