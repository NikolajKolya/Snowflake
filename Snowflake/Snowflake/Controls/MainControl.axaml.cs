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

        private bool isProgramStartedForTheFirstTime = true;

        #region constants
        private const int snowflakeSize = 30;
        private const double obliqueSnowflakeSize = 2 / 3.0 * snowflakeSize;

        private const int fps = 33;

        private readonly Pen snowFlakeThickness = new Pen(new SolidColorBrush(new Color(150, 255, 255, 255)), 5);
        #endregion

        private int _width;
        private int _height;

        private List<Snowflake.Snowflake> Snowflake = new List<Snowflake.Snowflake>();

        public MainControl()
        {
            InitializeComponent();

            PropertyChanged += OnPropertyChangedListener;

            for(int i = 0; i < 100; i++)
            {
                Snowflake.Add(new Snowflake.Snowflake());
            }
            var threadFPS = new Thread(OnTimerTick);
            threadFPS.Start();
        }

        private void OnTimerTick()
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
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);


            foreach (var item in Snowflake)
            {
                if (isProgramStartedForTheFirstTime)
                {
                    Thread thread = new Thread(SnowflakeFall);
                    thread.Start();
                }
                context.DrawLine(snowFlakeThickness, new Point(item.X - snowflakeSize, item.Y), new Point(item.X + snowflakeSize, item.Y));
                context.DrawLine(snowFlakeThickness, new Point(item.X, item.Y - snowflakeSize), new Point(item.X, item.Y + snowflakeSize));
                context.DrawLine(snowFlakeThickness, new Point(item.X - obliqueSnowflakeSize, item.Y + obliqueSnowflakeSize), new Point(item.X + obliqueSnowflakeSize, item.Y - obliqueSnowflakeSize));
                context.DrawLine(snowFlakeThickness, new Point(item.X + obliqueSnowflakeSize, item.Y + obliqueSnowflakeSize), new Point(item.X - obliqueSnowflakeSize, item.Y - obliqueSnowflakeSize));
                void SnowflakeFall()
                {
                    while (true)
                    {
                        item.Fall(_width, _height);
                        Thread.Sleep(fps);
                    }
                }
            }
            isProgramStartedForTheFirstTime = false;
        }
    }
}


