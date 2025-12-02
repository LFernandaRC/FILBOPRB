namespace EventosCorferias.Controls
{
    class CustomPickerDate : DatePicker
    {
        public static BindableProperty BorderThicknessProperty = 
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(CustomPicker), 0);

        public static BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(CustomPicker), Colors.Transparent);

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
    }
}
