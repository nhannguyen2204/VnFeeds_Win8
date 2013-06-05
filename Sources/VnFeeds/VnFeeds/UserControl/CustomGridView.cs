﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VnFeeds.DataModel;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VnFeeds.UserControl
{
    class CustomGridView : GridView
    {
        private int rowVal;
        private int colVal;
        private Random _rand;
        private List<Size> _sequence;

        public CustomGridView()
        {
            _rand = new Random();
            _sequence = new List<Size> { 
                LayoutSizes.PrimaryItem, 
                LayoutSizes.OtherSmallItem
            };
        }

        protected override void PrepareContainerForItemOverride(Windows.UI.Xaml.DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            DataItem dataItem = item as DataItem;
            int index =-1;

            if (dataItem != null)
            {
                index = dataItem.Group.Items.IndexOf(dataItem);
            }

            if (index >=0 && index < _sequence.Count)
            {
                colVal = (int)_sequence[index].Width;
                rowVal = (int)_sequence[index].Height;
            }
            else
            {
                colVal = (int)LayoutSizes.OtherSmallItem.Width;
                rowVal = (int)LayoutSizes.OtherSmallItem.Height;
            }

            VariableSizedWrapGrid.SetRowSpan(element as UIElement, rowVal);
            VariableSizedWrapGrid.SetColumnSpan(element as UIElement, colVal);
        }
    }

    public static class LayoutSizes
    {
        public static Size PrimaryItem = new Size(4, 2);
        //public static Size SecondarySmallItem = new Size(2, 1);
        //public static Size SecondaryTallItem = new Size(1, 2);
        public static Size OtherSmallItem = new Size(2, 1);

    }
}
