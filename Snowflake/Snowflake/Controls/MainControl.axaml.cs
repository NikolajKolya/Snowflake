using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Drawing.Text;
using System.Collections.Generic;

namespace Snowflake.Controls
{
    public partial class MainControl : UserControl
    {
        Timer Timer;

        private int _minSide;
        private int _maxSide;

        private double _scaling;

        private int _width = 800;
        private int _height;
        private List<Snowflake.Snowflake> Snowflake = new List<Snowflake.Snowflake>();
        private SolidColorBrush SnowflakeColor = new SolidColorBrush(Colors.SkyBlue);

        public MainControl()
        {
            InitializeComponent();

            PropertyChanged += OnPropertyChangedListener;

            for(int i = 0; i < 10; i++)
            {
                Snowflake.Add(new Snowflake.Snowflake());
            }

            Timer = new Timer(20);
            Timer.AutoReset = true;
            Timer.Enabled = true;
            Timer.Elapsed += OnTimerTick;
    }

        private void OnTimerTick(object? sender, ElapsedEventArgs e)
        {
            InvalidateVisual();
        }

        private void OnPropertyChangedListener(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals("Bounds")) // Если меняется размер окна
            {
                // Обработать изменение размера
                OnResize((Rect)e.NewValue);
            }
        }

        private void OnResize(Rect bounds)
        {
            _scaling = VisualRoot.RenderScaling;

            _width = (int)(bounds.Width * _scaling);
            _height = (int)(bounds.Height * _scaling);

            _minSide = Math.Min(_width, _height);
            _maxSide = Math.Max(_width, _height);
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);


            foreach (var item in Snowflake)
            {
                context.DrawLine(new Pen(new SolidColorBrush(new Color(100, 255, 255, 255)), 5), new Point(item.X - 30, item.Y), new Point(item.X + 30, item.Y));
                context.DrawLine(new Pen(new SolidColorBrush(new Color(100, 255, 255, 255)), 5), new Point(item.X, item.Y - 30), new Point(item.X, item.Y + 30));
                context.DrawLine(new Pen(new SolidColorBrush(new Color(100, 255, 255, 255)), 5), new Point(item.X - 20, item.Y + 20), new Point(item.X + 20, item.Y - 20));
                context.DrawLine(new Pen(new SolidColorBrush(new Color(100, 255, 255, 255)), 5), new Point(item.X + 20, item.Y + 20), new Point(item.X - 20, item.Y - 20));
                item.Fall();
            }
        }
    }
}
