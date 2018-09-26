using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.D.A_Host
{
    class LoadingBar
    {
        private int Max;
        private int CurrentVal;
        private bool Up;
        public LoadingBar(int MaxVal, int StartVal)
        {
            this.Max = MaxVal;
            this.CurrentVal = StartVal;
            this.Up = true;
        }
        public LoadingBar(int MaxVal, int StartVal, bool CountUp)
        {
            this.Max = MaxVal;
            this.CurrentVal = StartVal;
            this.Up = CountUp;
        }
        public void Draw(int Current)
        {
            this.CurrentVal = Current;
            if (Up)
            {
                drawTextProgressBar(CurrentVal, Max);
            }
            else
            {
                drawTextProgressBar(Max - CurrentVal, Max);
            }

        }
        public void DrawIncrement(int IncrementVal)
        {
            this.CurrentVal += IncrementVal;
            if (Up)
            {
                drawTextProgressBar(CurrentVal, Max);
            }
            else
            {
                drawTextProgressBar(Max - CurrentVal, Max);
            }

        }
        public bool Finished()
        {
            if(Up)
            {
                if(CurrentVal == Max)
                {
                    return true;
                }
            }
            else
            {
                if(CurrentVal == 0)
                {
                    return true;
                }
            }
            return false;
        }
        private static void drawTextProgressBar(int progress, int total)
        {
            Console.CursorLeft = 0;
            Console.Write("[");
            Console.CursorLeft = 32;
            Console.Write("]");
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    ");
        }
    }
}
