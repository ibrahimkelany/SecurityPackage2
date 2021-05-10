using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int x;
            for (x = 1; x < plainText.Length; x++)  
                
                {
  
                if (cipherText.ToLower() == Encrypt(plainText, x).ToLower())
                {
                    break;
                }
                }
            return x;


           // throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, int key)
        {
            int mre = 0;
         
            int c = 0;
            int r = 0;
            StringBuilder result = new StringBuilder();


            if (cipherText.Length % key != 0)
            {
                mre = 1;
            }
            char[,] text = new char[cipherText.Length, cipherText.Length / key + mre];

            int sizofcol = cipherText.Length / key + mre;
            for (int i = 0; i < cipherText.Length; i++)

                for (int j = 0; j < sizofcol; j++)
                {
                    text[i, j] = '$';
                }
         
            for (int i = 0; i < cipherText.Length; i++)
            {
                text[r, c] = cipherText[i];
                if (c == sizofcol - 1)
                {
                    c = 0;
                    r++;
                }
                else c++;
            }
            
            for (int i = 0; i < sizofcol; i++)

                for (int j = 0; j < cipherText.Length; j++)
                {
                    if (text[j, i] != '$')
                        result.Append(text[j, i]);
                }

            string res = result.ToString();


            return res; 
            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, int key)
        {
            StringBuilder result = new StringBuilder();
            int r = 0;
            int c = 0;
            char[,] ctext = new char[key, plainText.Length];

            for (int i = 0; i < key; i++)

                for (int j = 0; j < plainText.Length; j++)
                {
                    ctext[i, j] = '$';
                }
          
            for (int i = 0; i < plainText.Length; i++)
            {
                ctext[r, c] = plainText[i];
                if (r == key - 1)
                {
                    r = 0;
                    c++;
                }
                else
                {
                    r++;
                }
            }
            for (int i = 0; i < key; i++)

                for (int j = 0; j < plainText.Length; j++)
                {
                    if (ctext[i, j] != '$')
                    {
                        result.Append(ctext[i, j]);
                    }
                }
            string res =result.ToString();


            return res;
            //throw new NotImplementedException();
        }
    }
}
