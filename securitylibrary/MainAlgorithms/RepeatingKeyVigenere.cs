using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string key = "";
            string keyStream = "";
            cipherText= cipherText.ToLower();
            plainText= plainText.ToLower();

            for (int i = 0; i < plainText.Length; i++)
            {
                int E = cipherText[i];
                int P = plainText[i];
                int T = 0;
                if (P > E)
                {
                    T = P - E;
                    char K = (char)(((26-T) % 26) + 97);
                    keyStream += K;
                }
                if (E > P)
                {
                    T = E - P;
                    char K = (char)(((T + 26) % 26) + 97);
                    keyStream += K;
                }
                
               


                }
            char first = keyStream[0];
            for (int i = 1; i < keyStream.Length; i++)
            {
                if(first==(char)keyStream[i])
                {
                    key += keyStream[i - 1];
                    break;
                }
                key += keyStream[i-1];
            }

            if(Encrypt(plainText,key).Equals(cipherText))
            {
                return key;
            }
            else 
            {
                for (int i = key.Length; i < keyStream.Length; i++)
                {
                    key += keyStream[i];
                    if (Encrypt(plainText, key).Equals(cipherText))
                    {
                        return key;
                    }
                }
            
            }

            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string keyStream="";
            string Dec = "";
            cipherText = cipherText.ToLower();
            key = key.ToLower();
          
            while (true)
            {
                if (keyStream.Length == cipherText.Length)
                {
                    break;
                }
                for (int i = 0; i < key.Length; i++)
                {
                    if (keyStream.Length == cipherText.Length)
                    {
                        break;
                    }
                    keyStream += key[i];
                }
                if (keyStream.Length == cipherText.Length)
                {
                    break;
                }
            }
            for (int i = 0; i < cipherText.Length; i++)
            {
                int D = (cipherText[i] - keyStream[i] + 26) % 26;
                D += 97;
                Dec += (char)D;
            }
            return Dec;
        }
        public char[,] VigTable()
        {
            char[,] arr = new char[26, 26];
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    int k = ((j + i) % 26)+97; //'97' -> 'a'
                    arr[i, j] = (char)k;
                }
            }



            return arr;
        }
        public string Encrypt(string plainText, string key)
        {
            // throw new NotImplementedException();
            string Enc = "";
            string keyStream = "";
            int i = 0;
            int k = 0;
            plainText = plainText.ToLower();
            key = key.ToLower();
            char[,] arr = VigTable();
            while (true)
             {
                 if (keyStream.Length == plainText.Length)
                 {
                     break;
                 }
                 for (i = 0; i < key.Length; i++)
                 {
                     if (keyStream.Length == plainText.Length)
                     {
                         break;
                     }
                    keyStream += key[i];
                 }
                 if(keyStream.Length==plainText.Length)
                 {
                     break;
                 }    
             }

            for (i = 0; i < keyStream.Length; i++)
            {
                int indx1 = keyStream[i]-97;
                int indx2 = plainText[i]-97;
                char En=arr[indx1,indx2];
                Enc += En;


            }





            return Enc;
        }
    }
}