using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterDeDessin
{
    internal static class Helpers
    {

            public static Rectangle _2R(this RectangleF rF)
            {
                Rectangle r = new Rectangle((int)rF.Left, (int)rF.Top, (int)rF.Width, (int)rF.Height);
                return r;
            }
            public static T Next<T>(this T src) where T : struct
            {
                if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

                T[] Arr = (T[])Enum.GetValues(src.GetType());
                int j = Array.IndexOf<T>(Arr, src) + 1;
                return (Arr.Length == j) ? Arr[0] : Arr[j];
            }

        /// <summary>
        /// Suite de jalons temporels
        /// </summary>
        public static TimeRecording TimeRecord;
            /// <summary>
            /// permet de poser des jalons temporels
            /// et de mesurer le temps pour chaque étape d'un processus
            /// </summary>
            public class TimeRecording : List<TimeRecording.timedata>
            {
                public System.Diagnostics.Stopwatch sw;//

                public TimeRecording()
                {
                    sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                }

                /// <summary>
                ///  données unitaires à sauvgarder sous forme de STRUCT
                /// </summary>
                public struct timedata
                {
                    public string stepname;
                    public long elapsinceBeginning;
                    public long stepduration;
                    public override string ToString()
                    {
                        return string.Format("{0:00000}ms Duration: {1:0000} {2}", this.elapsinceBeginning, this.stepduration, stepname);
                    }
                }

                private static long lasttimestamp;

                public void checkin(string step)
                {
                    timedata td = new timedata();
                    td.stepname = step;
                    td.elapsinceBeginning = sw.ElapsedMilliseconds;
                    td.stepduration = td.elapsinceBeginning - lasttimestamp;
                    lasttimestamp = td.elapsinceBeginning;
                    base.Add(td);
                    //base.Add(td);
                }

            }

            public static Point getRealCoordOr0(PictureBox pb, int mouseX,int mouseY)
            {
                if (pb.Image == null) return default;
                Int32 realW = pb.Image.Width;
                Int32 realH = pb.Image.Height;
                
                Int32 currentW = pb.ClientRectangle.Width; //Obtient le rectangle qui représente la zone cliente du contrôle.
                Int32 currentH = pb.ClientRectangle.Height;
                Double zoomW = (currentW / (Double)realW);
                Double zoomH = (currentH / (Double)realH);
                Double zoomActual = Math.Min(zoomW, zoomH);
                Double padX = zoomActual == zoomW ? 0 : (currentW - (zoomActual * realW)) / 2;
                Double padY = zoomActual == zoomH ? 0 : (currentH - (zoomActual * realH)) / 2;

                Int32 realX = (Int32)((mouseX - padX) / zoomActual);
                Int32 realY = (Int32)((mouseY - padY) / zoomActual);
                //lblPosXval.Text = realX < 0 || realX > realW ? "-" : realX.ToString();
                //lblPosYVal.Text = realY < 0 || realY > realH ? "-" : realY.ToString();
                if (realX < 0) realX = 0;
                if (realX >= realW) realX = realW - 1;
                if (realY < 0) realY = 0;
                if (realY >= realH) realY = realH - 1;
                return new Point(realX, realY);
            }


      
    }

}
