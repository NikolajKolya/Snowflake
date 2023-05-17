using Avalonia;
using Avalonia.FreeDesktop.DBusIme;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snowflake.Snowflake
{
    public class Snowflake
    {
        #region Constants

        private readonly Pen _snowFlakePen = new Pen(new SolidColorBrush(new Color(150, 255, 255, 255)), 5);

        private const int SnowflakeSize = 30;
        private const double ObliqueSnowflakeSize = 2 / 3.0 * SnowflakeSize;

        #endregion

        private int _x, _y;
        private int _width, _height;
        private readonly int _speed;

        private Random _randomGenerator = new Random();

        public Snowflake()
        {
            _x = 0;
            _y = 0;

            _width = 0;
            _height = 0;

            _speed = _randomGenerator.Next(1, 10);
        }

        public void OnResize(int width, int height)
        {
            _width = width;
            _height = height;

            _x = _randomGenerator.Next(0, width);
            _y = _randomGenerator.Next(0, height);
        }

        public void SnowflakeFallLoop()
        {
            while (true)
            {
                FallStep();
            }
        }

        public void Draw(DrawingContext context)
        {
            context.DrawLine(_snowFlakePen, new Point(_x - SnowflakeSize, _y), new Point(_x + SnowflakeSize, _y));
            context.DrawLine(_snowFlakePen, new Point(_x, _y - SnowflakeSize), new Point(_x, _y + SnowflakeSize));
            context.DrawLine(_snowFlakePen, new Point(_x - ObliqueSnowflakeSize, _y + ObliqueSnowflakeSize), new Point(_x + ObliqueSnowflakeSize, _y - ObliqueSnowflakeSize));
            context.DrawLine(_snowFlakePen, new Point(_x + ObliqueSnowflakeSize, _y + ObliqueSnowflakeSize), new Point(_x - ObliqueSnowflakeSize, _y - ObliqueSnowflakeSize));
        }

        private void FallStep()
        {
            //Thread.Sleep(_randomGenerator.Next(1, 100));
            Thread.Sleep(10);

            if (_width == 0 || _height == 0)
            {
                return;
            }

            _y += _speed;

            if (_y > _height + 20)
            {
                _y = -10;
                _x = _randomGenerator.Next(0, _width);
            }
        }
    }
}
