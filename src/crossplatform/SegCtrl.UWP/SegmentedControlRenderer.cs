﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Plugin.SegmentedControl.Netstandard.Control;
using Plugin.SegmentedControl.UWP;
using Plugin.SegmentedControl.UWP.Control;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.Xaml;
using Grid = Windows.UI.Xaml.Controls.Grid;

[assembly: ExportRenderer(typeof(SegmentedControl), typeof(SegmentedControlRenderer))]
namespace Plugin.SegmentedControl.UWP
{
    public class SegmentedControlRenderer : ViewRenderer<Netstandard.Control.SegmentedControl, Control.SegmentedUserControl>
    {
        //private readonly IList<SegmentedControlOption> _segmentList;
        //private readonly ObservableCollection<SegmentedControlOption> _segmentCollection;
        private SegmentedUserControl _segmentedUserControl;

        private readonly ColorConverter _converter = new ColorConverter();

        public SegmentedControlRenderer()
        {
            //_segmentCollection = new ObservableCollection<SegmentedControlOption>();
            //_segmentCollection.CollectionChanged += OnSegmentCollectionChanged;
            //_segmentList = _segmentCollection;
        }

        //private void OnSegmentCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
        //{
        //    RebuildButtons();
        //}

        protected override void OnElementChanged(ElementChangedEventArgs<Netstandard.Control.SegmentedControl> e)
        {
            base.OnElementChanged(e);

            if (_segmentedUserControl == null)
            {
                CreateSegmentedRadioButtonControl();
                
            }

            if (e.NewElement != null)
            {
                
            }

            if (e.OldElement != null)
            {
                
            }
            
            //_segmentList.Add(Element.Children[0]);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName != "IsEnabled")
            {
                var t = e.PropertyName;
            }
            

            //RebuildButtons();

            //switch (e.PropertyName)
            //{
                    
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (_segmentedUserControl != null)
            {
                //_segmentCollection.CollectionChanged -= OnSegmentCollectionChanged;
            }

            base.Dispose(disposing);
        }

        private void CreateSegmentedRadioButtonControl()
        {
            _segmentedUserControl = new SegmentedUserControl();

            var grid = _segmentedUserControl.Body;

            grid.ColumnDefinitions.Clear();
            grid.Children.Clear();

            foreach (var child in Element.Children.Select((value, i) => new {i, value}))
            {
                var segmentButton = new SegmentRadioButton()
                {
                    Style = (Style)_segmentedUserControl.Resources["SegmentedRadioButtonStyle"],
                    Content = child.value.Text,
                    Tag = child.value.Text,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    IsChecked = child.value.IsEnabled,
                    BorderBrush = (SolidColorBrush)_converter.Convert(Element.TintColor, null, null, ""),
                    SelectedTextColor = (SolidColorBrush)_converter.Convert(Element.SelectedTextColor, null, null, ""),
                };

                if (child.value.IsEnabled)
                {
                    segmentButton.Background = (SolidColorBrush)_converter.Convert(Element.TintColor, null, null, "");
                }
                else
                {
                    segmentButton.Background = new SolidColorBrush(Colors.Transparent);
                }

                segmentButton.Checked += SegmentRadioButtonOnChecked;
                
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star),
                });

                segmentButton.SetValue(Grid.ColumnProperty, child.i);

                grid.Children.Add(segmentButton);
            }

            

            SetNativeControl(_segmentedUserControl);
        }

        private void SegmentRadioButtonOnChecked(object sender, RoutedEventArgs e)
        {
            var button = (SegmentRadioButton) sender;

            if (button != null)
            {
                Debug.WriteLine($"Segment pressed: {button.Tag}");
            }
        }

        private void RebuildButtons()
        {
            ////this.ColumnDefinitions.Clear();
            //this.Children.Clear();

            //_segmentedControl.Children.Clear();
            //_segmentedControl.ColumnDefinitions.Clear();

            //var label = new TextBlock
            //{
            //    //Text = _segmentList[0].Text,
            //};

            //_segmentedControl.Children.Add(label);

            //SetNativeControl(_segmentedControl);

            //for (var i = 0; i < _segmentList.Count; i++)
            //{
            //    var buttonSeg = _segmentList[i];

            //    var label = new Label
            //    {
            //        Text = buttonSeg.Text,
            //        HorizontalTextAlignment = TextAlignment.Center,
            //        VerticalTextAlignment = TextAlignment.Center
            //    };

            //    _grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            //    var frame = new AdvancedFrame();

            //    if (i == 0)
            //        frame.Corners = RoundedCorners.left;
            //    else if (i + 1 == SegmentedButtons.Count)
            //        frame.Corners = RoundedCorners.right;
            //    else
            //        frame.Corners = RoundedCorners.none;

            //    frame.CornerRadius = CornerRadius;

            //    frame.OutlineColor = OnColor;
            //    frame.Content = label;
            //    frame.HorizontalOptions = LayoutOptions.FillAndExpand;
            //    frame.VerticalOptions = LayoutOptions.FillAndExpand;

            //    DrawBoxes(i, frame, label);

            //    var tapGesture = new TapGestureRecognizer
            //    {
            //        Command = ItemTapped,
            //        CommandParameter = i
            //    };

            //    frame.GestureRecognizers.Add(tapGesture);

            //    this.Children.Add(frame, i, 0);
            //}
        }

        //public Command ItemTapped
        //{
        //    get
        //    {
        //        return new Command((obj) =>
        //        {

        //            var index = (int)obj;

        //            SelectedIndex = index;

        //            Command?.Execute(this.SegmentedButtons[index].Title);
        //        });
        //    }
        //}

        //private void SetSelectedIndex()
        //{
        //    for (var i = 0; i < Children.Count; i++)
        //    {
        //        var frame = Children[i] as AdvancedFrame;
        //        var label = frame.Content as Label;

        //        DrawBoxes(i, frame, label);
        //    }
        //}

        //private void DrawBoxes(int i, AdvancedFrame frame, Label label)
        //{

        //    if (i == SelectedIndex)
        //    {

        //        frame.InnerBackground = OnBackgroundColor;
        //        label.TextColor = OffColor;
        //    }
        //    else
        //    {

        //        frame.InnerBackground = OffBackgroundColor;
        //        label.TextColor = OnColor;
        //    }
        //}
        public static void Initialize()
        {
            //var resDir = Application.Current.Resources.MergedDictionaries.FirstOrDefault();
            //var mergedDir = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source.AbsoluteUri == "ms-ressouce:///Files/Style/SegmentedRadioButtonStyle.xaml");
            

            //var ctrl = new Control.SegmentedControl();

            //var radioButton = new RadioButton
            //{
            //    Style = (Style)ctrl.Resources["SegmentedRadioButton"],

            //};

            //var t = "";
        }

    }
}
