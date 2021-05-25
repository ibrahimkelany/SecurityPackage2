using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.AES
{
    /// <summary>
    /// If the string starts with 0x.... then it'str Hexadecimal not string
    /// </summary>
    public class AES : CryptographicTechnique
    {

        string[,] box = new string[16, 16] {
    {"52", "09", "6a", "d5", "30", "36", "a5", "38", "bf", "40", "a3", "9e", "81", "f3", "d7", "fb" },
     {"7c", "e3", "39", "82", "9b", "2f", "ff", "87", "34", "8e", "43", "44", "c4", "de", "e9", "cb" },
      {"54", "7b", "94", "32", "a6", "c2", "23", "3d", "ee", "4c", "95", "0b", "42", "fa", "c3", "4e" },
     {"08", "2e", "a1", "66", "28", "d9", "24", "b2", "76", "5b", "a2", "49", "6d", "8b", "d1", "25" },
      {"72", "f8", "f6", "64", "86", "68", "98", "16", "d4", "a4", "5c", "cc", "5d", "65", "b6", "92" },
     {"6c", "70", "48", "50", "fd", "ed", "b9", "da", "5e", "15", "46", "57", "a7", "8d", "9d", "84" },
      {"90", "d8", "ab", "00", "8c", "bc", "d3", "0a", "f7", "e4", "58", "05", "b8", "b3", "45", "06" },
     {"d0", "2c", "1e", "8f", "ca", "3f", "0f", "02", "c1", "af", "bd", "03", "01", "13", "8a", "6b" },
      {"3a", "91", "11", "41", "4f", "67", "dc", "ea", "97", "f2", "cf", "ce", "f0", "b4", "e6", "73" },
      {"96", "ac", "74", "22", "e7", "ad", "35", "85", "e2", "f9", "37", "e8", "1c", "75", "df", "6e" },
      {"47", "f1", "1a", "71", "1d", "29", "c5", "89", "6f", "b7", "62", "0e", "aa", "18", "be", "1b" },
      {"fc", "56", "3e", "4b", "c6", "d2", "79", "20", "9a", "db", "c0", "fe", "78", "cd", "5a", "f4" },
      {"1f", "dd", "a8", "33", "88", "07", "c7", "31", "b1", "12", "10", "59", "27", "80", "ec", "5f" },
      {"60", "51", "7f", "a9", "19", "b5", "4a", "0d", "2d", "e5", "7a", "9f", "93", "c9", "9c", "ef" },
      {"a0", "e0", "3b", "4d", "ae", "2a", "f5", "b0", "c8", "eb", "bb", "3c", "83", "53", "99", "61" },
      {"17", "2b", "04", "7e", "ba", "77", "d6", "26", "e1", "69", "14", "63", "55", "21", "0c", "7d" } };

        int[,] mainm = new int[4, 4]{

                {14,11,13,9 },
                {9,14,11,13 },
                {13,9,14,11 },
                {11,13,9,14 }
            };



       
        

        public long sfv(long value) //change
        {
            value <<= 1;
            if ((value & 256) == 0)
            {
                return value;

            }
            else
            {
                value -= 256;
                value ^= 27;
            }
            return value;

        }
    
       
      

        public List<string> ListOfD(string x)//change
        {
            List<string> u = new List<string>();
            x = x.Remove(0, 2);
            List<string> myX = new List<string>();
            for (int i = 0; i < x.Length; i += 2)
            {
                myX.Add(x[i].ToString() + x[i + 1].ToString());
            }
            int z = 0;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    u.Add(myX[z]);
                    z++;
                }

            return u;
        }

        public string[,] coinv(string x) //change
        {
           
            List<string> u2 = ListOfD(x);
            string[,] U = new string[4, 4];
            for (int i = 0; i < 4; i++)
            {
                U[i, 0] = u2[i];
            }
            for (int i = 0; i < 4; i++)
            {
                U[i, 1] = u2[i+4];
            }
            for (int i = 0; i < 4; i++)
            {
                U[i, 2] = u2[i+8];
            }
            for (int i = 0; i < 4; i++)
            {
                U[i, 3] = u2[i+12];
            }

            string[,] res = new string[4, 4];
            long value;

            long tmpres = 0;
            for (int i = 0; i < 4; i++)//rowtext
            {
                for (int k = 0; k < 4; k++)//rowkey
                {
                    for (int j = 0; j < 4; j++)//coltext-colkey
                    {
                        value = Convert.ToInt64(U[j, k], 16);

                        if (mainm[i, j] == 13)
                        {

                            value = sfv(value);
                            value ^= Convert.ToInt64(U[j, k], 16);

                            for (int mm = 0; mm < 2; mm++)
                            {
                                value = sfv(value);
                            }

                          
                            value ^= Convert.ToInt64(U[j, k], 16);
                        }
                        else if (mainm[i, j] == 11)
                        {
                            for (int mm = 0; mm < 2; mm++)
                            {
                                value = sfv(value);
                            }

                            value ^= Convert.ToInt64(U[j, k], 16);
                            value = sfv(value);
                            value ^= Convert.ToInt64(U[j, k], 16);
                        }
                        else if (mainm[i, j] == 9)
                        {
                            for (int mm = 0; mm < 3; mm++)
                            {
                                value = sfv(value);
                            }
                            value ^= Convert.ToInt64(U[j, k], 16);
                        }


                        else
                        {

                            for (int mm = 0; mm < 2; mm++)
                            {
                                value = sfv(value);
                                value ^= Convert.ToInt64(U[j, k], 16);

                            }
                           
                            value = sfv(value);
                        }

                        tmpres ^= value;
                    }
                    res[i, k] = tmpres.ToString("X");
                    if(res[i,k].Length==1)
                    {
                        res[i, k] = "0" + res[i, k];
                    }
                    
                    tmpres = 0;
                }

            }
            return res;
        }

     
        public string ShiftRowsInvers(string inp,bool chk)
        {
           
            List<string> MyInp = new List<string>();
            for (int i = 2; i < inp.Length; i+=2)
            {
                MyInp.Add(inp[i].ToString() + inp[i + 1].ToString());
            }
            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append("0x");
            if (chk)
            {
                stringBuilder.Append(MyInp[0]);
                stringBuilder.Append(MyInp[7]);
                stringBuilder.Append(MyInp[10]);
                stringBuilder.Append(MyInp[13]);
                stringBuilder.Append(MyInp[1]);
                stringBuilder.Append(MyInp[4]);
                stringBuilder.Append(MyInp[11]);
                stringBuilder.Append(MyInp[14]);
                stringBuilder.Append(MyInp[2]);
                stringBuilder.Append(MyInp[5]);
                stringBuilder.Append(MyInp[8]);
                stringBuilder.Append(MyInp[15]);
                stringBuilder.Append(MyInp[3]);
                stringBuilder.Append(MyInp[6]);
                stringBuilder.Append(MyInp[9]);
                stringBuilder.Append(MyInp[12]);
            }
            else
            {
                stringBuilder.Append(MyInp[0]);
                stringBuilder.Append(MyInp[13]);
                stringBuilder.Append(MyInp[10]);
                stringBuilder.Append(MyInp[7]);
                stringBuilder.Append(MyInp[4]);
                stringBuilder.Append(MyInp[1]);
                stringBuilder.Append(MyInp[14]);
                stringBuilder.Append(MyInp[11]);
                stringBuilder.Append(MyInp[8]);
                stringBuilder.Append(MyInp[5]);
                stringBuilder.Append(MyInp[2]);
                stringBuilder.Append(MyInp[15]);
                stringBuilder.Append(MyInp[12]);
                stringBuilder.Append(MyInp[9]);
                stringBuilder.Append(MyInp[6]);
                stringBuilder.Append(MyInp[3]);
            }


            return stringBuilder.ToString();
        }

        public override string Decrypt(string cipherText, string key)
        {
            //    throw new NotImplementedException();
            //cipherText = "0x29C3505F571420F6402299B31A02D73A";
           // key = "Thats my Kung Fu";
            string tempKey = key;
            string tempcipherText = cipherText;


            if (!cipherText.StartsWith("0x"))
            {

                cipherText = "0x" + String.Concat(cipherText.Select(x => ((int)x).ToString("x")));


            }
            if (!key.StartsWith("0x"))
            {

                key = "0x" + String.Concat(key.Select(x => ((int)x).ToString("x")));


            }




      
            string[,] MixcolumnsState = new string[4, 4];
            string[] Keys = new string[11];
            Keys[0] = key;

            for (int i = 0; i < 10; i++)
                Keys[i + 1] = generatekey(Keys[i], i, 9);
            key = Keys[10];
            cipherText = AddRoundKey(cipherText, key);
            cipherText = "0x" + cipherText;
            //cipherText = sr(cipherText);
            cipherText = ShiftRowsInvers(cipherText,false);
         //   cipherText = isp(cipherText);


            StringBuilder sb = new StringBuilder();
            sb.Append("0x");
            for (int i = 2; i < 34; i += 2)
            {
                sb.Append(box[long.Parse(cipherText[i].ToString(), System.Globalization.NumberStyles.HexNumber), long.Parse(cipherText[i + 1].ToString(), System.Globalization.NumberStyles.HexNumber)]);

            }
            cipherText = sb.ToString();
           

           


            for (int i = 9; i > 0; i--)

            {
                sb = new StringBuilder();
                sb.Append("0x");
                cipherText = AddRoundKey(cipherText, Keys[i]);
                cipherText = "0x" + cipherText;
                MixcolumnsState = coinv(cipherText);
               
                cipherText = ShiftRowsInvers("0x" + string.Join("", MixcolumnsState.Cast<string>()),true);
                
                for (int k = 2; k < 34; k += 2)
                {
                    sb.Append(box[long.Parse(cipherText[k].ToString(), System.Globalization.NumberStyles.HexNumber), long.Parse(cipherText[k + 1].ToString(), System.Globalization.NumberStyles.HexNumber)]);

                }
                cipherText = sb.ToString();
            }

            cipherText = AddRoundKey(cipherText, Keys[0]);
            if (tempcipherText.StartsWith("0x") || tempKey.StartsWith("0x"))
            {
                cipherText = "0x" + cipherText;
                return cipherText;
            }
            
           

            return cipherText;
        }
        public static string DecToHex(long x)
        {
            string result = "";
            List<string> Hexx = new List<string>();
            for (int i = 0; i < 16; i++)
            {
                Hexx.Add(i.ToString("X"));
            }
            while (x != 0)
            {
                int mod = (int.Parse(x.ToString()) % 16);
               result = Hexx[mod] + result;
               

                x /= 16;
            }
            if (result.Length == 1)
            {
                return "0" + result;
            }
            return result;
        }
        public override string Encrypt(string plainText, string key)
        {
            // throw new NotImplementedException();
            //plainText = "Two One Nine Two";
            //key = "Thats my Kung Fu";
            string tempPlain = plainText;
            string tempKey = key;
            string Enc = "";

           
            if (!plainText.StartsWith("0x"))
            {

                 plainText="0x"+String.Concat(plainText.Select(x => ((int)x).ToString("x")));


            }
            if (!key.StartsWith("0x"))
            {

                key = "0x"+String.Concat(key.Select(x => ((int)x).ToString("x")));


            }




            //1st Add round key
            Enc = AddRoundKey(plainText, key);
            Enc = "0x" + Enc;
            int rounds = 9;
            int i = 0;
            while (i <= rounds)
            {
                Enc = SubBytes(Enc);
                //ShifRows


                Enc = ShiftRows(Enc); // need to be cracked

                //Mix columns
                if (i != rounds)
                {
                    Enc = MixColumns(Enc, 4);
                }
                //key = generatekey("0x5468617473206D79204B756E67204675", i, rounds);
                key = generatekey(key, i, rounds);
                Enc = "0x" + AddRoundKey(Enc, key);
                i++;
            }
            if(tempPlain.StartsWith("0x")||tempKey.StartsWith("0x"))
                {

                return Enc;
                }
           /* else
            {
                Enc = Enc.Remove(0,2);

               // Enc = Enc.Remove(0);
            }*/
            return Enc;
        }
        public static string Reverse(string str)
        {
            char[] charArray = str.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public string generatekey(string key, int r, int rounds)
        {

            List<string> RC = new List<string>();
            RC.Add("01");
            RC.Add("02");
            RC.Add("04");
            RC.Add("08");
            RC.Add("10");
            RC.Add("20");
            RC.Add("40");
            RC.Add("80");
            RC.Add("1b");
            RC.Add("36");


            string input2;
            string res = "";

            int i;
            StringBuilder strbuild = new StringBuilder(9);
            for (i = key.Length - 1; i > 25; i--)
            {
                strbuild.Append(key[i]);

            }

            input2 = Reverse(strbuild.ToString());
            string str;
            List<string> temp = new List<string>();
            temp.Add(input2[2].ToString() + input2[3].ToString());
            temp.Add(input2[4].ToString() + input2[5].ToString());
            temp.Add(input2[6].ToString() + input2[7].ToString());
            temp.Add(input2[0].ToString() + input2[1].ToString());


            StringBuilder strBuilder = new StringBuilder(256);
            i = 0;
            long a, aa;
            bool ch = false;
            List<string> myRES = new List<string>(256);
            List<string> myKey = new List<string>(256);
            string[,] Box1 = new string[,]
          {
                 {"63","7c","77","7b","f2","6b","6f","c5","30","01","67","2b","fe","d7","ab","76"},
                 {"ca","82","c9","7d","fa","59","47","f0","ad","d4","a2","af","9c","a4","72","c0"},
                 {"b7","fd","93","26","36","3f","f7","cc","34","a5","e5","f1","71","d8","31","15"},
                 {"04","c7","23","c3","18","96","05","9a","07","12","80","e2","eb","27","b2","75"},
                 {"09","83","2c","1a","1b","6e","5a","a0","52","3b","d6","b3","29","e3","2f","84"},
                 {"53","d1","00","ed","20","fc","b1","5b","6a","cb","be","39","4a","4c","58","cf"},
                 {"d0","ef","aa","fb","43","4d","33","85","45","f9","02","7f","50","3c","9f","a8"},
                 {"51","a3","40","8f","92","9d","38","f5","bc","b6","da","21","10","ff","f3","d2"},
                 {"cd","0c","13","ec","5f","97","44","17","c4","a7","7e","3d","64","5d","19","73"},
                 {"60","81","4f","dc","22","2a","90","88","46","ee","b8","14","de","5e","0b","db"},
                 {"e0","32","3a","0a","49","06","24","5c","c2","d3","ac","62","91","95","e4","79"},
                 {"e7","c8","37","6d","8d","d5","4e","a9","6c","56","f4","ea","65","7a","ae","08"},
                 {"ba","78","25","2e","1c","a6","b4","c6","e8","dd","74","1f","4b","bd","8b","8a"},
                 {"70","3e","b5","66","48","03","f6","0e","61","35","57","b9","86","c1","1d","9e"},
                 {"e1","f8","98","11","69","d9","8e","94","9b","1e","87","e9","ce","55","28","df"},
                 {"8c","a1","89","0d","bf","e6","42","68","41","99","2d","0f","b0","54","bb","16"}
          };
            while (i < (RC.Count - 2) * 2)
            {
                if (i < RC.Count - 2 && ch == false)
                {
                    str = Box1[Convert.ToInt64(temp[i / 2].ToString()[0].ToString(), 16), Convert.ToInt64(temp[i / 2].ToString()[1].ToString(), 16)];
                    res += str;
                    i++;
                    i++;
                    continue;
                }

                else
                {
                    long xoKEy;
                    if (ch == false)
                    {
                        ch = true;
                        for (i = 0; i < res.Length; i += 2)
                        {
                            myRES.Add(res[i].ToString() + (res[i + 1].ToString()));

                        }
                        i = 0;
                        for (i = 0; i < key.Length; i += 2)
                        {
                            myKey.Add(key[i].ToString() + (key[i + 1].ToString()));

                        }
                        i = 0;
                        a = Convert.ToInt64(myRES[0], 16);
                        xoKEy = Convert.ToInt64(myKey[1], 16);
                        a = a ^ xoKEy;
                        a ^= Convert.ToInt64(RC[r], 16);
                        strBuilder.Append(DecToHex(a));
                        i++;
                        continue;
                    }




                    a = Convert.ToInt64(myRES[i], 16);
                    xoKEy = Convert.ToInt64(myKey[i + 1], 16);
                    a = a ^ xoKEy;
                    strBuilder.Append(DecToHex(a));
                    i++;

                    if (i >= (RC.Count - 2) / 2)
                    {
                        break;
                    }
                }
            }




            int l = 0;
            i = 0;
            string RETT = strBuilder.ToString();

            List<string> myRet = new List<string>(256);


            for (i = 0; i < RETT.Length / 2; i++)
            {

                myRet.Add(RETT[i].ToString() + (RETT[i + 1].ToString()));

            }

            RETT = strBuilder.ToString();


            rounds++;
            int round = rounds;
            i = 0;
            while (true)
            {
                str = (Convert.ToInt64(key[round].ToString() + key[round + 1].ToString(), 16) ^ Convert.ToInt64(RETT[l].ToString() + RETT[l + 1].ToString(), 16)).ToString("X");
                if (str.Length == 1)
                {
                    str = "0" + str;
                }
                RETT = RETT + str;
                round++;
                round++;
                i++;
                l = l + 2;
                if (i == 12)
                {
                    break;
                }

            }
            return "0x" + RETT;
        }
        public string MixColumns(string inp, int mine)
        {

            StringBuilder Mixed = new StringBuilder(256);
            Mixed.Append("0x");

            List<string> Look = new List<string>();
            Look.Add("02");
            Look.Add("03");
            for (int k = 0; k < 3; k++)
            {
                Look.Add("01");

            }

            Look.Add("02");
            Look.Add("03");
            for (int k = 0; k < 3; k++)
            {
                Look.Add("01");

            }
            Look.Add("02");
            Look.Add("03");
            Look.Add("03");
            for (int k = 0; k < 2; k++)
            {
                Look.Add("01");

            }

            Look.Add("02");


            List<string> myInp = new List<string>();
            for (int i = 0; i < inp.Length; i += 2)
            {
                myInp.Add(inp[i].ToString() + inp[i + 1].ToString());
            }



            int index = 2;


            int bigLoopIndex = 0;
            long ress = 0;
            while (bigLoopIndex < mine)
            {
                string str = "";
                long a, b, c;

                int co = 0;
                ress = 0;
                int j = 0;
                while (j < mine)
                {
                    ress = 0;
                    index = 2 + 8 * bigLoopIndex;
                    for (int i = 0; i < mine; i++)
                    {

                        str = myInp[index / 2];


                        a = Convert.ToInt64(str, 16);
                        b = Convert.ToInt64(Look[co], 16);
                        long b1 = b;
                        if (b == 2 || b == 3)
                        {
                            b = 2;
                            c = a * b;
                            if (c > 255)
                            {
                                c &= 255;
                                c ^= 27;

                            }
                            if (b1 == 3)
                            {

                                c ^= a;
                            }
                            ress ^= c;
                        }

                        else
                        {
                            ress = ress ^ a;
                        }

                        index += 2;
                        co += 1;
                    }

                    str = ress.ToString("X");
                    if (str.Length == 1)
                        str = "0" + str;

                    Mixed.Append(str);
                    j++;
                }
                bigLoopIndex++;
            }

            return Mixed.ToString();

        }





        public String AddRoundKey(string plain, string key)
        {

            //to perform add round key they have four steps
            //1st SubBytes

            /*
             
            Simple substitution on each byte of state independently
            Use an S-box of 16x16 bytes containing a permutation of all 256 8-bit values
            Each byte of state is replaced by a new byte indexed by row (left 4-bits) & column (right 4-bits)
            eg. byte {95} is replaced by {2A} in row 9 column 5
            S-box constructed using defined transformation of values in GF(28)
            Designed to be resistant to all known attacks

             */

            string p = "";
            string k = "";
            //plain 0x....
            int i = 2;
            StringBuilder stringBuilder = new StringBuilder(256);

            while (i < 34)
            {
                p += plain[i].ToString();
                k += key[i].ToString();
                if ((i + 1) % 2 != 0)
                {

                    i++;
                    continue;
                }


                long aa = Convert.ToInt64(p, 16);
                long bb = Convert.ToInt64(k, 16);
                aa = aa ^ bb;
                string chk = aa.ToString("X");
                if (chk.Length == 1)
                {
                    chk = "0" + chk;
                }
                stringBuilder.Append(chk);
                p = "";
                k = "";
                i++;
            }





            return stringBuilder.ToString();
        }
        public string SubBytes(string plain)
        {
            string[,] Box2 = new string[,]
           {
                 {"63","7c","77","7b","f2","6b","6f","c5","30","01","67","2b","fe","d7","ab","76"},
                 {"ca","82","c9","7d","fa","59","47","f0","ad","d4","a2","af","9c","a4","72","c0"},
                 {"b7","fd","93","26","36","3f","f7","cc","34","a5","e5","f1","71","d8","31","15"},
                 {"04","c7","23","c3","18","96","05","9a","07","12","80","e2","eb","27","b2","75"},
                 {"09","83","2c","1a","1b","6e","5a","a0","52","3b","d6","b3","29","e3","2f","84"},
                 {"53","d1","00","ed","20","fc","b1","5b","6a","cb","be","39","4a","4c","58","cf"},
                 {"d0","ef","aa","fb","43","4d","33","85","45","f9","02","7f","50","3c","9f","a8"},
                 {"51","a3","40","8f","92","9d","38","f5","bc","b6","da","21","10","ff","f3","d2"},
                 {"cd","0c","13","ec","5f","97","44","17","c4","a7","7e","3d","64","5d","19","73"},
                 {"60","81","4f","dc","22","2a","90","88","46","ee","b8","14","de","5e","0b","db"},
                 {"e0","32","3a","0a","49","06","24","5c","c2","d3","ac","62","91","95","e4","79"},
                 {"e7","c8","37","6d","8d","d5","4e","a9","6c","56","f4","ea","65","7a","ae","08"},
                 {"ba","78","25","2e","1c","a6","b4","c6","e8","dd","74","1f","4b","bd","8b","8a"},
                 {"70","3e","b5","66","48","03","f6","0e","61","35","57","b9","86","c1","1d","9e"},
                 {"e1","f8","98","11","69","d9","8e","94","9b","1e","87","e9","ce","55","28","df"},
                 {"8c","a1","89","0d","bf","e6","42","68","41","99","2d","0f","b0","54","bb","16"}
           };
            StringBuilder stringBuilder = new StringBuilder(256);
            List<string> MyPlain = new List<string>();

            for (int i = 2; i < plain.Length; i += 2)
            {
                MyPlain.Add(plain[i].ToString() + plain[i + 1].ToString());
            }
            for (int i = 0; i < MyPlain.Count; i++)
            {
                stringBuilder.Append(Box2[Convert.ToInt64(MyPlain[i][0].ToString(), 16), Convert.ToInt64(MyPlain[i][1].ToString(), 16)]);
            }


            return "0x" + stringBuilder.ToString();
        }
        public string ShiftRows(string plain)
        {
            /*
             1st row is unchanged
             2nd row does 1 byte circular shift to left
             3rd row does 2 byte circular shift to left
             4th row does 3 byte circular shift to left
            */


            List<string> myCont = new List<string>();
            for (int i = 2; i < plain.Length; i += 2)
            {
                myCont.Add(plain[i].ToString() + plain[i + 1].ToString());
            }
            StringBuilder con = new StringBuilder(256);
            for (int i = 0; i < 20; i += 5)
            {
                con.Append(myCont[i]);
            }
            for (int i = 4; i < 15; i += 5)
            {
                con.Append(myCont[i]);
            }

            for (int i = 3; i < 15; i += 5)
            {
                con.Append(myCont[i]);
            }

            for (int i = 2; i < 15; i += 5)
            {
                con.Append(myCont[i]);
            }

            for (int i = 1; i < 15; i += 5)
            {
                con.Append(myCont[i]);
            }


            return "0x" + con.ToString();

        }
    }
}
