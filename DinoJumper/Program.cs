using System.Diagnostics;
using static DinoJumper.ColorScreen;

namespace DinoJumper
{
    internal class Program
    {
        static bool isRunning = true;

        static void Main(string[] args)
        {
            // tiempo inicial de espera (3 segs)
            Thread.Sleep(3000);

            Console.WriteLine("Ejecutando Dino Jumper... (presione cualquier tecla para salir)");

            // Inicio del Jumper
            ThreadPool.QueueUserWorkItem((_)=> IniciarDinoJumper());

            if(Console.ReadKey(true) != null)
            {
                DetenerDinoJumper();
                Console.WriteLine("Se detuvo Dino Jumper");
            }
        }

        static void IniciarDinoJumper()
        {
            while(isRunning)          
            {
                Thread.Sleep(50);
                CheckearObstaculo();
            }
        }
        static void DetenerDinoJumper() => isRunning = true;    

        private static void CheckearObstaculo()
        {
            var cursorPos = CursorPos.GetCursorPosition();
            var controlPoint = GetColorRGB(new Point(cursorPos.X - 250 , cursorPos.Y));     // area de control

            for(int i = 0; i < 25; i++)     //busca en una linea horizontal de 25px desde el cursor
            {
                if(controlPoint != GetColorRGB(cursorPos))
                {
                    Jumper.SendTeclaEspacio();
                    break;
                }
                cursorPos.X +=1;
            }
        }
    }
}
