using System.Runtime.InteropServices;

namespace DinoJumper
{
    static class CursorPos
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point pt);
        internal static Point GetCursorPosition()
        {
            var p = new Point();
            GetCursorPos(ref p);
            return p;
        }
    }
}
