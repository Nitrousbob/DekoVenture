namespace DekoVenture
{
    public enum ColorMode
    {
        FullColor,
        Monochrome
    }
    public static class Clr
    {
        //I wrote this class because I was tired of typeing Console.ForegroundColor = ConsoleColor etc
        //If I was all done or wanted to trim it down I could boil it down to the used colors
        //delete all the 0 reference colors and make colors for the rgb colors that are used more than
        //a couple times
        public static ColorMode CurrentMode
        {
            get
            {
                if (Game.CurrentGame != null) //I need to add something to the Game that tracs the color Mode
                {
                    return Game.CurrentGame.CurrentMode;
                }
                return ColorMode.FullColor;
            }
        }
        //Brown
        public static void Br() => Clr.Rgb(170, 143, 110);
        public static void Br(string message) => RgbWrite(170, 143, 110, message);
        public static void DBr() => Clr.Rgb(130, 103, 61);
        public static void DBr(string message) => RgbWrite(130, 103, 61, message);
        //Orange 
        public static void O() => Clr.Rgb(230, 136, 43);
        public static void O(string message) => RgbWrite(230, 136, 0, message);
        //Dark Orange
        public static void DO() => Clr.Rgb(191, 111, 30);
        public static void DO(string message) => RgbWrite(191, 111, 0, message);
        //Light Yellow
        public static void LY() => Clr.Rgb(255, 255, 16);
        public static void LY(string message) => RgbWrite(255, 255, 16, message);
        //Yellow
        public static void Y() => Clr.Rgb(222, 182, 0);
        public static void Y(string message) => RgbWrite(215, 200, 0, message);
        //Orange/DarkYellow
        public static void DY() => Clr.Rgb(176, 144, 0);
        public static void DY(string message) => RgbWrite(176, 144, 0, message);
        //White
        public static void W() => Clr.Rgb(255, 255, 255);
        public static void W(string message) => RgbWrite(255, 255, 255, message);
        //Red
        public static void R() => Clr.Rgb(255, 0, 0);
        public static void R(string message) => RgbWrite(255, 0, 0, message);
        //Dark Red
        public static void DR() => Clr.Rgb(150, 0, 0);
        public static void DR(string message) => RgbWrite(150, 0, 0, message);
        //Gray
        public static void Gr() => Clr.Rgb(150, 150, 150);
        public static void Gr(string message) => RgbWrite(150, 150, 150, message);
        //Dark Gray
        public static void DGr() => Clr.Rgb(100, 100, 100);
        public static void DGr(string message) => RgbWrite(100, 100, 100, message);
        //Light Green
        public static void LG() => Clr.Rgb(172, 255, 135);
        public static void LG(string message) => RgbWrite(172, 255, 135, message);
        //Green
        public static void G() => Clr.Rgb(131, 190, 105);
        public static void G(string message) => RgbWrite(142, 200, 115, message);
        //Dark Green
        public static void DG() => Clr.Rgb(91, 140, 70);
        public static void DG(string message) => RgbWrite(91, 140, 70, message);
        //Light Blue
        public static void LB() => Clr.Rgb(0, 200, 255);
        public static void LB(string message) => RgbWrite(0, 200, 255, message);
        //Blue
        public static void B() => Clr.Rgb(23, 105, 188);
        public static void B(string message) => RgbWrite(23, 105, 188, message);
        //Dark Blue
        public static void DB() => Clr.Rgb(48, 52, 255);
        public static void DB(string message) => RgbWrite(48, 52, 255, message);
        public static void LP() => Clr.Rgb(220, 50, 235);
        public static void LP(string message) => RgbWrite(220, 50, 235, message);
        public static void P() => Rgb(167, 27, 228);
        public static void P(string message) => RgbWrite(167, 27, 228, message);
        public static void DP() => Rgb(103, 17, 140);
        public static void DP(string message) => RgbWrite(103, 17, 140, message);

        public static void Pk() => Rgb(255, 118, 198);
        public static void Pk(string message) => RgbWrite(255, 118, 198, message);
        public static void DPk() => Rgb(194, 0, 113);
        public static void DPk(string message) => RgbWrite(194, 0, 113, message);
        public static void RgbWrite(int r, int g, int b, string message)
        {
            if (CurrentMode == ColorMode.Monochrome)
            {
                Console.Write(message);
                return;
            }
            Console.Write($"\x1b[38;2;{r};{g};{b}m{message}\x1b[0m");
        }
        public static void Rgb(int r, int g, int b)
        {
            if (CurrentMode == ColorMode.Monochrome)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write($"\x1b[38;2;{r};{g};{b}m");
            }
        }
        public static void Rgb(ColorMode mode, int r, int g, int b, string message)
        {
            if (mode == ColorMode.Monochrome)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write($"\x1b[38;2;{r};{g};{b}m{message}\x1b[0m");
            }
        }
        public static string ColorString(int r, int g, int b, string message)
        {
            if (CurrentMode == ColorMode.Monochrome)
            {
                return message;
            }
            return $"\x1b[38;2;{r};{g};{b}m{message}\x1b[0m";

        }
        //public static string ColorString(IColorable item, string message)
        //{
        //    if (CurrentMode == ColorMode.Monochrome)
        //    {
        //        return message;
        //    }
        //    // Use the color properties directly from the item
        //    return $"\x1b[38;2;{item.ColorR};{item.ColorG};{item.ColorB}m{message}\x1b[0m";
        //}
        //public static void ColorWrap(string? message, int? number = null, char? begin = null, char? end = null, Action? color = null) //i need to be able to omit one or the other
        //{
        //    if (begin.HasValue)
        //    {
        //        W($"{begin.Value}");
        //    }
        //    else
        //    {
        //        W("(");
        //    }

        //    if (!string.IsNullOrEmpty(message))
        //    {
        //        DGr();
        //        Console.Write(message);
        //    }

        //    if (number.HasValue)
        //    {
        //        if (color != null)
        //        {
        //            color(); // execute the specific color method passed in
        //        }
        //        else
        //        {
        //            DGr(); //default color of wrapped text/number
        //        }

        //        Console.Write(message);
        //    }

        //    if (end.HasValue)
        //    {
        //        W($"{end}");
        //    }
        //    else
        //    {
        //        W(")");
        //    }
        //}
        public static void ColorStatsList(int a, int s, int f, int i, int p, int k) //this was the DaggerHeart stats
        {
            DGr("["); O($"{a}"); DGr("]");
            DGr("["); O($"{s}"); DGr("]");
            DGr("["); O($"{f}"); DGr("]");
            DGr("["); O($"{i}"); DGr("]");
            DGr("["); O($"{p}"); DGr("]");
            DGr("["); O($"{k}"); DGr("]");
        }

        public static void ErrorMsg(string message)
        {
            //var oldColor = Console.ForegroundColor;
            R(message);
            //Console.ResetColor();;
        }

        public static void InvalidMenuItem(string message)
        {
            //var oldColor = Console.ForegroundColor;
            DGr(message);
            //Console.ResetColor();;
        }

        //public static void Heart(Action setColorAction) //this can call an action to be used
        //{
        //    //var oldColor = Console.ForegroundColor;
        //    setColorAction();  //run the method that was passed in
        //    R("♥ ");
        //    //Console.ResetColor();;
        //}

        public static void ColorMove(string message)
        {
            var old = Console.ForegroundColor;
            try
            {
                if (message.Contains("West"))
                {
                    DGr(); Console.Write("<<<");
                    int gAdjust = 225;
                    foreach (char c in message)
                    {
                        Clr.Rgb(0, gAdjust, 0);
                        Console.Write(c);
                        gAdjust -= 25;

                    }
                    Clr.DGr("<<<");
                }
                else if (message.Contains("East"))
                {
                    DGr(">>>");
                    int gAdjust = 125;
                    foreach (char c in message)
                    {
                        Clr.Rgb(0, gAdjust, 0);
                        Console.Write(c);
                        gAdjust += 25;
                    }
                    DGr(">>>");
                }
                else if (message.Contains("North") || message.Contains("South"))
                {
                    DGr();
                    if (message.Contains("North"))
                    {
                        Console.Write("///");
                    }
                    else
                    {
                        Console.Write(@"\\\");
                    }
                    int count = 1;
                    int gAdjust = 150;
                    foreach (char c in message)
                    {
                        if (count < 3)
                        {
                            Clr.Rgb(0, gAdjust, 0);
                            Console.Write(c);
                            gAdjust += 35;
                            count++;
                        }
                        else if (count == 3)
                        {
                            Clr.Rgb(0, gAdjust, 0);
                            Console.Write(c);
                            gAdjust -= 50; //adjust going out to next letter
                            count++;
                        }
                        else if (count > 3)
                        {
                            Clr.Rgb(0, gAdjust, 0);
                            Console.Write(c);
                            gAdjust -= 35;
                            count++;
                        }
                    }
                    if (message.Contains("North"))
                    {
                        DGr(@"\\\");
                    }
                    else
                    {
                        DGr("///");
                    }
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            finally
            {
                Console.ForegroundColor = old;
            }
        }

    }
}
