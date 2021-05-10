using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {
        public string Encrypt(string plainText, int key)
        {
            
            string Encrypted = "";
            for(int i=0; i<plainText.Length;i++)
            {
                char letter = plainText[i];
                int asc = letter + key;
                if(asc>122)
                {
                    asc -= 26;
                }
                else if(asc>90 &&asc<97)
                {
                    asc -= 26;

                }

                letter = (char)asc;



                Encrypted += letter.ToString();

            }
            return Encrypted;


        //    throw new NotImplementedException();
            
        }

        public string Decrypt(string cipherText, int key)
        {
            //throw new NotImplementedException();
            string Decrypted = "";
            for (int i = 0; i < cipherText.Length; i++)
            {
                char letter = cipherText[i];
                int asc = letter - key;
                if (asc < 97&&asc>=90)
                {
                    asc += 26;
                }
                else if (asc < 65 )
                {
                    asc += 26;

                }

                letter = (char)asc;



                Decrypted += letter.ToString();

            }
            return Decrypted;
        }

        public int Analyse(string plainText, string cipherText)
        {
            // throw new NotImplementedException();
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            if (plainText.Equals(cipherText))
            {
                return 0;
            }
            int key = 0;

            for (int k = 0; k < 26; k++)
            {
                for (int i = 0; i < plainText.Length; i++)
                {
                    char a = (char)(plainText[i]+k);
                    char b = (char)((cipherText[i] ));
                    if(a>'z')
                    {
                        int asc = a;
                        asc -= 26;
                        a = (char)asc;
                    }
                    if (a == b)
                    {
                        key = k;
                        break;
                        // return k;

                    }
                   
                    break;


                }
            }
            return key;
        }
    }
}
