using System.Runtime.InteropServices;

namespace DinoJumper
{
    internal static class Jumper
    {
        const ushort SPACE = 0x20;
        internal static readonly int sizeOfInput = Marshal.SizeOf<INPUT>();


        #region Sendinput
        [DllImport("user32.dll")]
        private static extern uint SendInput(uint numInputs, INPUT[] input, int cbSize);

        internal struct INPUT
        {
            public InputEventType type;
            public MKH_INPUTUNION u;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MKH_INPUTUNION       //este paso extra es obligatorio para C# (teclado) :/
        {
            [FieldOffset(0)]
            public MouseInput mInput;

            [FieldOffset(0)]
            public KeyboardInput kInput;

            [FieldOffset(0)]
            public HardwareInput hinput;
        }

        internal enum InputEventType : int
        {
            Mouse,     //0
            Keyboard,  //1
            Hardware   //2
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HardwareInput      //actualmente sin efecto activo en el proyecto, solo requerido por Sendinput
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }
        #endregion

        #region Mouse
        [StructLayout(LayoutKind.Sequential)]
        internal struct MouseInput
        {
            internal int dX;    // Movimiento horizontal del ratón en píxeles
            internal int dY;     // Movimiento vertical del ratón en píxeles
            internal uint mouseData;    // Información adicional sobre el evento (ej. rueda + ó -; xbutton 1 ó 2)
            internal MouseF dwFlags;  // ¡importante! Flags que indican qué tipo de evento (ej. presionar, soltar)
            internal uint time;             // Sendinput usa la hora del sistema por defecto (ej. mdown = 0 y mup = 100 ms despues) - ms
            internal IntPtr dwExtraInfo;    // Información adicional del evento (generalmente se deja como null)
        }

        [Flags]
        internal enum MouseF : uint
        {
            move = 0x0001,
            leftDown = 0x0002,
            leftUp = 0x0004,
            rightDown = 0x0008,
            rightUp = 0x0010,
            wheelRotation = 0x0800,
            hWheel = 0x1000,
            wheelDown = 0x0020,
            wheelUp = 0x0040,
            xDown = 0x0080,
            xUp = 0x0100,
            virtualDesk = 0x4000,
            absolute = 0x8000,
        }
        internal enum SideButton
        {
            atrás = 0x0001,
            adelante = 0x0002
        }
        #endregion

        #region Keyboard
        [StructLayout(LayoutKind.Sequential)]
        internal struct KeyboardInput
        {
            internal ushort wVk;      //a virtual-key code
            internal ushort wScan;    //A hardware scan code for the key.
            internal KbFlags dwFlags;    //Specifies various aspects of a keystroke.
            internal uint time;
            internal IntPtr dwExtraInfo;
        }

        internal enum KbFlags : uint
        {
            /// <summary>
            /// If specified, the system synthesizes a VK_PACKET keystroke.
            /// The wVk parameter must be zero. 
            /// This flag can only be combined with the KEYEVENTF_KEYUP flag.
            /// For more information, see the Remarks section. 
            ///KEF_EXTENDEDKEY = 0x0001,
            /// </summary>
            EXTENDEDKEY = 0x0001,

            /// <summary>
            /// If specified, the key is being released. If not specified, the key is being pressed. 
            ///KEF_KEYUP = 0x0002,
            /// </summary>
            KEYUP = 0x0002,

            /// <summary>
            /// If specified, wScan identifies the key and wVk is ignored. 
            /// KEF_SCANCODE = 0x0008,
            /// </summary>

            SCANCODE = 0x0008,

            /// <summary>
            /// If specified, the wScan scan code consists of a sequence of two bytes,
            /// where the first byte has a value of 0xE0. See Extended-Key Flag for more info. 
            /// KEF_UNICODE = 0x0004,
            /// </summary>

            UNICODE = 0x0004,

        }
        #endregion


        internal static INPUT[] TeclaEspacio = new INPUT[2]
        {
            new INPUT{
                type = InputEventType.Keyboard,
                u =  new MKH_INPUTUNION{
                    kInput =  new KeyboardInput{
                        wVk = SPACE,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            },
            new INPUT{
                type = InputEventType.Keyboard,
                u =  new MKH_INPUTUNION{
                    kInput =  new KeyboardInput{
                        wVk = SPACE,
                        dwFlags = KbFlags.KEYUP,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            }
        };

        //public static Action SendTeclaEspacio = () => ThreadPool.QueueUserWorkItem((_) => SendInput(2, TeclaEspacio, sizeOfInput));

        public static void SendTeclaEspacio()
        {
            SendInput(2, TeclaEspacio, sizeOfInput);
        }
    }
}
