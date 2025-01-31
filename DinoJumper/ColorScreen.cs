using System.Runtime.InteropServices;

namespace DinoJumper
{
    static class ColorScreen
    {
        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        static extern int ReleaseDC(IntPtr window, IntPtr dc);

        internal static Color GetColorRGB(Point p)      //tupla
        {        
            IntPtr deviceContext = GetDC(IntPtr.Zero);      //all screen, none window
            var pixel = GetPixel(deviceContext, p.X, p.Y);
            ReleaseDC(IntPtr.Zero, deviceContext);

            Color color = new Color
            {
                r = (byte)((pixel >> 0) & 0xff),
                g = (byte)((pixel >> 8) & 0xff),
                b = (byte)((pixel >> 16) & 0xff)
            };

            return color;
        }
    }
}
