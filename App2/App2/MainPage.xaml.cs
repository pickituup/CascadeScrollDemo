using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2 {
    public partial class MainPage : ContentPage {

        private double _scrollCriticalPoint;

        public MainPage() {
            InitializeComponent();
            Init();

            double d = AbsoluteLayout.AutoSize;

            AbsoluteLayout.SetLayoutFlags(_stickySecondBlock_Label, AbsoluteLayoutFlags.WidthProportional);
            AbsoluteLayout.SetLayoutBounds(_stickySecondBlock_Label, new Rectangle(0, 0, 1, AbsoluteLayout.AutoSize));
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == HeightProperty.PropertyName) {
                if (Height > 0) {
                    _scrollCriticalPoint = _firstBlock_Label.Height + _secondBlock_Label.Height;
                }
            }
        }

        private void Init() {
            string[] items = new string[23];

            for (int i = 0; i < items.Length; i++) {
                items[i] = string.Format("{0} item", i);
            }

            _list_ListView.ItemsSource = items;



            _rootScroll_ScrollView.Scrolled += _rootScroll_ScrollView_Scrolled;
        }

        private void _rootScroll_ScrollView_Scrolled(object sender, ScrolledEventArgs e) {
            _scrollCriticalPoint = _firstBlock_Label.Height;
            
            if (e.ScrollY >= _scrollCriticalPoint) {
                //
                // Stick
                //
                _stickySecondBlock_Label.IsVisible = true;

                _rootScroll_ScrollView.InputTransparent = true;
                _thirdGridRow_RowDefinition.Height = new GridLength(1, GridUnitType.Star);
            }
            else if (e.ScrollY < _scrollCriticalPoint) {
                //
                // Pull off
                //
                _stickySecondBlock_Label.IsVisible = false;


            }
        }
    }
}
