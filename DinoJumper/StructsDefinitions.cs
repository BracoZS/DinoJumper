namespace DinoJumper
{
    struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    struct Color
    {
        public byte r;
        public byte g;
        public byte b;

        public static bool operator ==(Color color1, Color color2)
        {
            return color1.r == color2.r && color1.g == color2.g && color1.b == color2.b;
        }

        // Sobrecargar el operador '!=' (desigualdad)
        public static bool operator !=(Color color1, Color color2)
        {
            return !(color1 == color2); // Llama al operador '==' para comparar
        }
    }
}
