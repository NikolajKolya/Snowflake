using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Threading;
using Avalonia.Threading;

namespace Snowflake.Controls
{
    public partial class MainControl : UserControl
    {

        private double _scaling;

        #region constants

        private const int fps = 10;

        #endregion

        private int _width;
        private int _height;

        private List<Snowflake.Snowflake> _snowflakes = new List<Snowflake.Snowflake>();

        public MainControl()
        {
            InitializeComponent();

            PropertyChanged += OnPropertyChangedListener;

            for(int i = 0; i < 100; i++)
            {
                var snowflake = new Snowflake.Snowflake();

                _snowflakes.Add(snowflake);

                var snowflakeThread = new Thread(snowflake.SnowflakeFallLoop);
                snowflakeThread.Start();
            }

            var threadFPS = new Thread(OnNextFrame);
            threadFPS.Start();
        }

        private void OnNextFrame()
        {
            while (true)
            {
                InvalidateVisual();
                Thread.Sleep(fps);
            }
        }

        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds"))
            {
                OnResize((Rect)e.NewValue);
            }
        }

        private void OnResize(Rect bounds)
        {
            _scaling = VisualRoot.RenderScaling;

            _width = (int)(bounds.Width * _scaling);
            _height = (int)(bounds.Height * _scaling);

            foreach (var snowflake in _snowflakes)
            {
                snowflake.OnResize(_width, _height);
            }
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            foreach (var snowflake in _snowflakes)
            {
                snowflake.Draw(context);
            }
        }
    }
}


