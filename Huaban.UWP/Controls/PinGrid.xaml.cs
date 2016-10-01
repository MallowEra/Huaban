﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace Huaban.UWP.Controls
{
	using ViewModels;
	public sealed partial class PinGrid : UserControl
	{
		public PinGrid()
		{
			this.InitializeComponent();
		}
		#region Header
		public UIElement Header
		{
			get { return (UIElement)GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof(UIElement), typeof(PinGrid), new PropertyMetadata(null, OnHeaderChanged));

		private static void OnHeaderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = (PinGrid)sender;
			control.HeaderContent.Content = e.NewValue;
		}
		#endregion

		#region Footer
		public UIElement Footer
		{
			get { return (UIElement)GetValue(FooterProperty); }
			set { SetValue(FooterProperty, value); }
		}

		public static readonly DependencyProperty FooterProperty =
			DependencyProperty.Register("Footer", typeof(UIElement), typeof(PinGrid), new PropertyMetadata(null, OnFooterChanged));

		private static void OnFooterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = (PinGrid)sender;
			control.FooterContent.Content = e.NewValue;
		}
		#endregion

		#region ListModel
		public PinListViewModel ListModel
		{
			get { return (PinListViewModel)GetValue(ListModelProperty); }
			set { SetValue(ListModelProperty, value); }
		}

		public static readonly DependencyProperty ListModelProperty =
			DependencyProperty.Register("ListModel", typeof(PinListViewModel), typeof(PinGrid), new PropertyMetadata(null, OnListModelChanged));

		private static void OnListModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var control = (PinGrid)sender;
			control.pinGrid.DataContext = e.NewValue;
		}

		#endregion
	}
}
