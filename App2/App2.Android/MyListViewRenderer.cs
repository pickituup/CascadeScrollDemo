using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;
using static Android.Widget.AbsListView;

[assembly: ExportRenderer(typeof(App2.MyListView), typeof(MyListViewRenderer))]
namespace App2.Droid {
    public class MyListViewRenderer : ListViewRenderer , IOnScrollListener {

        public MyListViewRenderer(Context context) : base(context) {

        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount) {
            if (firstVisibleItem == 0) {
                Console.WriteLine("QQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
                MessagingCenter.Send<object, bool>(this, "KEEP_NATIVE_GESTURE_OFFSET_TRACKING", true);
            }
            else {

            }
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState) {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e) {
            base.OnElementChanged(e);


            Control?.SetOnScrollListener(this);
            Control?.SetOnTouchListener(MainActivity.MY_TOUCH_LISTENER);
            MainActivity.MY_TOUCH_LISTENER.FOR_LIST = Control;
        }

    }
}