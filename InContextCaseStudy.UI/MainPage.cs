using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InContextCaseStudy.Logic;
using InContextCaseStudy.Logic.Contracts;
using Xamarin.Forms;

namespace InContextCaseStudy.UI
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            _refreshButton = new Button
            {
                Text = "Refresh",
            };
            _refreshButton.Clicked += Refresh;

            _grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) },
                }
            };

            _layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    _refreshButton,
                    _grid,
                }
            };

            _scrollView = new ScrollView
            {
                Content = _layout,
            };

            _activityIndicator = new ActivityIndicator
            {
                IsRunning = true,
            };
        }

        private readonly Button _refreshButton;
        private readonly Grid _grid;
        private readonly Layout _layout;
        private readonly ScrollView _scrollView;
        private readonly ActivityIndicator _activityIndicator;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh(this, new EventArgs());
        }

        private bool IsWorking
        {
            set
            {
                if (value)
                    Content = _activityIndicator;
                else
                    Content = _scrollView;
            }
        }

        private async void Refresh(object sender, EventArgs ea)
        {
            try
            {
                IsWorking = true;

                var serviceClient = new ServiceClient();
                var data = await serviceClient.GetProductImages(12);
                BindData(data);
            }
            finally
            {
                IsWorking = false;
            }
        }

        private void BindData(IEnumerable<ProductImage> data)
        {
            _grid.Children.Clear();
            _grid.RowDefinitions.Clear();
            _grid.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});

            int column = 0;
            int row = 0;
            foreach (var item in data)
            {
                if (column >= 2)
                {
                    _grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    column = 0;
                    row++;
                }

                var view = GetImageView(item);
                _grid.Children.Add(view, column, row);
                column++;
            }
        }

        private View GetImageView(ProductImage productImage)
        {
            var stream = new MemoryStream(productImage.Bytes);
            var view = new Image
            {
                Source = ImageSource.FromStream(() => stream)
            };

            return view;
        }
    }
}
