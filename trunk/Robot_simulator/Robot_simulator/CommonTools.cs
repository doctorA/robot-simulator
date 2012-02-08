using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Robot_simulator
{
    /// <summary>
    /// Razred z pretvorbami Vector3d v Matrika4d in obratno
    /// </summary>
    public class CommonTools
    {
        /// <summary>
        /// Metoda pretvori stopinje v radiane
        /// </summary>
        /// <param name="angle">Kot</param>
        /// <returns>Kot v radianih</returns>
        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Metoda pretvori matriko4d v 3d vektor ( točko )
        /// </summary>
        /// <param name="H">vhodna matrika</param>
        /// <param name="result_loc">3d izhodna točka - lokacija</param>
        /// <param name="result_ori">3d izhodna točka - končna orientacija</param>
        /// <param name="previous_ori">3d izhodna točka - prejšnja orientacija</param>
        public static void MatrikavPRPYwithPrevious(Matrix4d H, Vector3d result_loc, Vector3d result_ori, Vector3d previous_ori)
        {
            //ce ne bo dobro probaj upostevat stare zunanje koordinate. winrobsim: datoteka MySys in metode MatrikavPRPY...

            double vStopinje = 180.0 / Math.PI;
            double vRadiane = Math.PI / 180.0;

            //result_loc.x = H.m03;
            //result_loc.y = H.m13;
            //result_loc.z = H.m23;

            result_loc.X = H.M14;
            result_loc.Y = H.M24;
            result_loc.Z = H.M34;

            double fi, theta, psi;
            fi = previous_ori.X * vRadiane;
            theta = previous_ori.Y * vRadiane;
            psi = previous_ori.Z * vRadiane;

            //if (Math.Abs(H.m20) < 0.9999)
            if (Math.Abs(H.M31) < 0.9999)
            {
                //result_ori.x = Math.atan2(H.m10, H.m00);
                //result_ori.y = Math.atan2(-H.m20, Math.sqrt(H.m21 * H.m21 + H.m22 * H.m22));
                //result_ori.z = Math.atan2(H.m21, H.m22);

                result_ori.X = Math.Atan2(H.M21, H.M11);
                result_ori.Y = Math.Atan2(-H.M31, Math.Sqrt(H.M32 * H.M32 + H.M33 * H.M33));
                result_ori.Z = Math.Atan2(H.M32, H.M33);
            }
            else
            {
                //if (H.m20 < 0)
                if (H.M31 < 0)
                {
                    result_ori.Y = Math.PI / 2.0;
                    result_ori.Z = psi;		//kako izbrat Psi?
                    //result_ori.x = Math.atan2(H.m12 - H.m01, H.m02 + H.m11) - result_ori.z;
                    result_ori.X = Math.Atan2(H.M23 - H.M12, H.M13 + H.M22) - result_ori.Z;
                }
                else
                {
                    result_ori.Y = -Math.PI / 2.0;
                    result_ori.Z = psi;		//kako izbrat Psi?
                    //result_ori.x = Math.atan2(-H.m12 - H.m01, -H.m02 + H.m11) - result_ori.z;
                    result_ori.X = Math.Atan2(H.M23 - H.M12, H.M13 + H.M22) - result_ori.Z;
                }
            }

            result_ori.X *= vStopinje;
            result_ori.Y *= vStopinje;
            result_ori.Z *= vStopinje;
        }

        /// <summary>
        /// Metoda iz lokacije in orientacije tvori homogeno matriko
        /// </summary>
        /// <param name="loc">3d vektor lokacije</param>
        /// <param name="ori">3d vektor orientacije</param>
        public static Matrix4d PRPYvMatriko(Vector3d loc, Vector3d ori)
        {
            Matrix4d result = new Matrix4d();

            //koti: F, T, P
            //double F = Math.toRadians(ori.x);
            //double T = Math.toRadians(ori.y);
            //double P = Math.toRadians(ori.z);

            double F = DegreeToRadian(ori.X);
            double T = DegreeToRadian(ori.Y);
            double P = DegreeToRadian(ori.Z);

            double SF = Math.Sin(F);
            double CF = Math.Cos(F);
            double ST = Math.Sin(T);
            double CT = Math.Cos(T);
            double SP = Math.Sin(P);
            double CP = Math.Cos(P);

            double eps = 0.00001;

            //result.m00 = CF * CT;
            //result.m01 = CF * ST * SP - SF * CP;
            //result.m02 = CF * ST * CP + SF * SP;
            //result.m03 = loc.x;
            //result.m10 = SF * CT;
            //result.m11 = SF * ST * SP + CF * CP; //prej bilo --> result.m11 = SF * ST * CP + CF * CP; Ne vem, zakaj???
            //result.m12 = SF * ST * CP - CF * SP;
            //result.m13 = loc.y;
            //result.m20 = -ST;
            //result.m21 = CT * SP;
            //result.m22 = CT * CP;
            //result.m23 = loc.z;
            //result.m30 = 0;
            //result.m31 = 0;
            //result.m32 = 0;
            //result.m33 = 1;

            result.M11 = CF * CT;
            result.M12 = CF * ST * SP - SF * CP;
            result.M13 = CF * ST * CP + SF * SP;
            result.M14 = loc.X;
            result.M21 = SF * CT;
            result.M22 = SF * ST * SP + CF * CP; //prej bilo --> result.m11 = SF * ST * CP + CF * CP; Ne vem, zakaj???
            result.M23 = SF * ST * CP - CF * SP;
            result.M24 = loc.Y;
            result.M31 = -ST;
            result.M32 = CT * SP;
            result.M33 = CT * CP;
            result.M34 = loc.Z;
            result.M41 = 0;
            result.M42 = 0;
            result.M43 = 0;
            result.M44 = 1;

            return result;
        }
    }
}
