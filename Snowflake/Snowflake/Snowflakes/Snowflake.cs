using Avalonia;
using Avalonia.FreeDesktop.DBusIme;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Snowflake.Snowflake
{
    public class Snowflake
    {
        public int X, Y, Speed;
        Random RandomGenerator = new Random();
        public Snowflake() {
            X = RandomGenerator.Next(0, 800);
            Y = -10;
            Speed = RandomGenerator.Next(10, 100);
        }

        public void Fall(int width, int height)
        {
            Y += Speed;
            if (Y > height + 20) {
                Y = -10;
                X = RandomGenerator.Next(0, width);
                Speed = RandomGenerator.Next(5, 60);
            }
        }
    }
}
