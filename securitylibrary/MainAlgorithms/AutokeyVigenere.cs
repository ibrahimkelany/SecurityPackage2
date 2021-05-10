using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            int siz = 0;

            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();

            StringBuilder k = new StringBuilder();

            int L = cipherText.Length;
            for (int i = 0; i < L; i++)
            {
                int first = cipherText[i] - 'a';
                int secand = plainText[i] - 'a';

                char cc = (char)((first - secand + 26) % 26 + 'a');

                k.Append(cc);
            }


            int l2 = plainText.Length;

            for (int l = 1; l < l2; l++)
            {
                StringBuilder tmp = new StringBuilder
                    (k.ToString().Substring(plainText.Length - l, l));

                StringBuilder bgPlain = new StringBuilder
                    (plainText.ToString().Substring(0, l));


                if (tmp.ToString() == bgPlain.ToString())
                {
                    siz = l;

                    break;
                }
            }
            string res = k.ToString().Substring(0, plainText.Length - siz);

            return res;
            //throw new NotImplementedException();

        }


        public string Decrypt(string cipherText, string key)
        {
            cipherText = cipherText.ToLower();
            StringBuilder dec = new StringBuilder();

            StringBuilder k = new StringBuilder(key);



            int L = cipherText.Length;
            for (int i = 0; i < L; i++)
            {
                int first = cipherText[i] - 'a';
                int secand = k[i] - 'a';


                char cc = (char)((first - secand + 26) % 26 + 'a');


                dec.Append(cc);
                k.Append(cc);
            }

            string res = dec.ToString();


            return res;
            //throw new NotImplementedException();

        }

        public string Encrypt(string plainText, string key)
        {
            StringBuilder k = new StringBuilder(key);

            StringBuilder enc = new StringBuilder();



            int idx = 0;

            while (k.Length < plainText.Length)
            {
                k.Append(plainText[idx]);
                idx++;
            }


            for (int i = 0; i < plainText.Length; i++)
            {
                int first = plainText[i] - 'a';
                int sec = k[i] - 'a';


                char cc = (char)((first + sec) % 26 + 'a');

                enc.Append(cc);
            }
            string res = enc.ToString();

            return res;
            //throw new NotImplementedException();

        }
    }
}