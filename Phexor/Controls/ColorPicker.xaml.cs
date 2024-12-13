using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Phexor.Controls;

public partial class ColorPicker : UserControl
{
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ColorPicker),
            new PropertyMetadata("Title")
        );

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
   
    
    public static readonly DependencyProperty ColorProperty =
        DependencyProperty.Register(
            nameof(SelectedColor),
            typeof(string),
            typeof(ColorPicker),
            new PropertyMetadata("SelectedColor")
        );

    public string SelectedColor
    {
        get => (string)GetValue(ColorProperty);
        set => SetValue(ColorProperty, value);
    }
    public ColorPicker()
    {
        InitializeComponent();

        RedSlider.ValueChanged += OnColorChanged;
        GreenSlider.ValueChanged += OnColorChanged;
        BlueSlider.ValueChanged += OnColorChanged;
    }

    private void OnColorChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        // Update the ColorDisplay rectangle based on the sliders' values
        var color = Color.FromRgb((byte)RedSlider.Value, (byte)GreenSlider.Value, (byte)BlueSlider.Value);
        ColorDisplay.Fill = new SolidColorBrush(color);
    }
    
}