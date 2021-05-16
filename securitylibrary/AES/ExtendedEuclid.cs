using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    public class ExtendedEuclid 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="baseN"></param>
        /// <returns>Mul inverse, -1 if no inv</returns>
        public int GetMultiplicativeInverse(int number, int baseN)
        {
            //throw new NotImplementedException();
            int result = -1;
            if(number==baseN)
            {
                return -1;
            }

         
            int B = 0;
            double A1 = number;
            double C1 = baseN;
            while (true)
            {
              
                if(B>baseN)
                {
                    break;
                }
                double AA = A1 % C1;
                double BB = B % C1;
                double CC = AA * BB;
                double DD = CC % C1;
                if (DD==1)
                {
                    return B;
                }
                B++;

            }




            return -1;
        }
      
    }
}
