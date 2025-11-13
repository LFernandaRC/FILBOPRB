namespace EventosCorferias.Controls
{
    public class CheckBoxEx : GraphicsView
    {
        public CheckBoxEx()
        {
            Drawable = new CheckBoxDrawable(this);
            WidthRequest = 12;  // 🔹 Ajustamos el tamaño exacto
            HeightRequest = 12;
            GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => IsChecked = !IsChecked) });
        }

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBoxEx), false, BindingMode.TwoWay, propertyChanged: (bindable, _, __) =>
            {
                ((CheckBoxEx)bindable).Invalidate(); // 🔹 Redibuja cuando cambia
            });

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        private class CheckBoxDrawable : IDrawable
        {
            private readonly CheckBoxEx _parent;

            public CheckBoxDrawable(CheckBoxEx parent) => _parent = parent;

            public void Draw(ICanvas canvas, RectF dirtyRect)
            {
                canvas.FillColor = Colors.White;
                canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

                canvas.StrokeSize = 2;
                canvas.StrokeColor = Color.FromArgb("#58585e"); // 🔹 Borde azul
                canvas.DrawRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);

                if (_parent.IsChecked)
                {
                    canvas.FillColor = Color.FromArgb("#58585e");
                    canvas.FillRectangle(2, 2, dirtyRect.Width - 4, dirtyRect.Height - 4);
                }
            }
        }
    }
}
