using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage2 : ContentPage {

        PanGestureRecognizer _panGestureRecognizer = new PanGestureRecognizer();
        Resolver resolver = new Resolver();

        public MainPage2() {
            InitializeComponent();

            string[] items = new string[23];

            for (int i = 0; i < items.Length; i++) {
                items[i] = string.Format("{0} item", i);
            }

            _list_ListView.ItemsSource = items;

            MessagingCenter.Subscribe<Resolver, float>(this, "START_TRANSLATE", (sender, e) => {
                _rootLayout_RelativeLayout.TranslationY = e;

                _outbut_Label.Text = e.ToString();
            });


            MessagingCenter.Subscribe<object, bool>(this, "IS_LIST_PULL_TO_REFRESH", (sender, e) => {
                _list_ListView.IsPullToRefreshEnabled = e;
            });
        }
    }

    public class Resolver {



        public Resolver() {

            float _currentTranslation = 76;

            MessagingCenter.Subscribe<object, CooArg>(this, "TEST", (sender, e) => {
                _currentTranslation += e.FormsY;

                if (_currentTranslation > 76) {
                    _currentTranslation = 76;
                    MessagingCenter.Send<Resolver, float>(this, "START_TRANSLATE", _currentTranslation);

                    MessagingCenter.Send<object, bool>(this, "BLOCK_LIST_EVENTS_HANDLING", false);
                    MessagingCenter.Send<object, bool>(this, "IS_LIST_PULL_TO_REFRESH", true);
                }
                else if (_currentTranslation < 0) {
                    _currentTranslation = 0;
                    MessagingCenter.Send<Resolver, float>(this, "START_TRANSLATE", _currentTranslation);
                    MessagingCenter.Send<object, bool>(this, "KEEP_NATIVE_GESTURE_OFFSET_TRACKING", false);

                    MessagingCenter.Send<object, bool>(this, "BLOCK_LIST_EVENTS_HANDLING", false);
                }
                else {
                    MessagingCenter.Send<Resolver, float>(this, "START_TRANSLATE", _currentTranslation);
                    MessagingCenter.Send<object, bool>(this, "BLOCK_LIST_EVENTS_HANDLING", true);
                    MessagingCenter.Send<object, bool>(this, "IS_LIST_PULL_TO_REFRESH", false);
                }

            });

            MessagingCenter.Subscribe<object, object>(this, "HIDE_CLAMP", (sender, e) => {
                _currentTranslation = 0;
                MessagingCenter.Send<Resolver, float>(this, "START_TRANSLATE", _currentTranslation);
            });
        }
    }

    public class CooArg {
        public float FormsY {
            get; set;
        }

        public float RawY {
            get; set;
        }
    }
}