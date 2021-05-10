using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            string Dec = "";
            key = key.ToLower();
            string myKey = key;
            string inp = "";
            HashSet<string> HS = new HashSet<string>();
            for (int i = 0; i < myKey.Length; i++)
            {
                if (myKey[i].Equals("i") || myKey[i].Equals("j"))
                {
                    HS.Add("i");
                    continue;
                }
                HS.Add(myKey[i].ToString());


            }
            string[,] arr = new string[5, 5];
            //inp = HS.ToString();
            inp = string.Join("", HS.ToArray()); // .NET 4.0
                                                 //Second fill 5x5 array


            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (count < inp.Length)
                    {
                        arr[i, k] = inp[count].ToString();
                    }
                    count++;





                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (string.IsNullOrEmpty(arr[i, k]))
                    {

                        for (char d = 'z'; d >= 'a'; d--)
                        {
                            if (checkLetterinArr(d, arr) || d == 'j')
                            {
                                continue;
                            }
                            else
                            {
                                arr[i, k] = d.ToString();
                            }
                        }

                    }





                }
            }
            //Third Decrypt


            cipherText = cipherText.ToLower();
            string[] plain = new string[cipherText.Length / 2];
            count = 0;
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (count == cipherText.Length)
                {
                    break;
                }
                plain[i] = cipherText[count].ToString() + cipherText[count + 1].ToString();
                count++;
                count++;
            }
            //If both the letters are in the same column


            for (int k = 0; k < cipherText.Length / 2; k++)
            {
                string tmp = plain[k];
                string let1 = tmp[0].ToString();
                string let2 = tmp[1].ToString();


                for (int i = 0; i < 5; i++)
                {
                    for (int z = 0; z < 5; z++)
                    {
                        if (arr[i, z].Equals(let1))
                        {
                            bool f1 = false;
                            bool f2 = false;
                            int rect = -1;
                            for (int m = 0; m < 5; m++)
                            {



                                //same column
                                if (arr[m, z].Equals(let2))
                                {
                                    if (i == 0 && m != 0)
                                    {
                                        Dec += arr[4, z] + arr[m - 1, z];
                                        f1 = true;
                                        rect = 1;
                                    }
                                    else if (i != 0 && m != 0)
                                    {
                                        Dec += arr[i - 1, z] + arr[m - 1, z];
                                        f1 = true;
                                        rect = 1;
                                    }
                                    else if (i != 0 && m == 0)
                                    {
                                        Dec += arr[i - 1, z] + arr[4, z];
                                        f1 = true;
                                        rect = 1;
                                    }

                                    //Enc += arr[i , z+1] + arr[i, m+1];
                                }
                                //same row
                                if (arr[i, m].Equals(let2)) //m!=
                                {
                                    //z+1 m+1
                                    if (z != 0 && m != 0)
                                    {
                                        Dec += arr[i, z - 1] + arr[i, m - 1];
                                        f2 = true;
                                        rect = 2;
                                    }
                                    else if (z != 0 && m == 0)
                                    {
                                        Dec += arr[i, z - 1] + arr[i, 4];
                                        f2 = true;
                                        rect = 2;

                                    }
                                    else if (z == 0 && m != 0)
                                    {
                                        Dec += arr[i, 4] + arr[i, m - 1];
                                        f2 = true;
                                        rect = 2;


                                    }


                                }







                            }
                            if (rect == -1)//Rect
                            {



                                int x = -1, y = -1;

                                for (int i2 = 0; i2 < 5; i2++)
                                {
                                    for (int z2 = 0; z2 < 5; z2++)
                                    {
                                        if (arr[i2, z2].Equals(let2))
                                        {
                                            x = i2;
                                            y = z2;
                                            Dec += arr[i, y] + arr[x, z];

                                        }


                                    }


                                }
                            }
                        }






                    }
                }




            }

            string DEC = "";
            for (int i = 0; i < Dec.Length - 2; i += 2)
            {
                DEC += Dec[i];
                if (Dec[i] == Dec[i + 2] && Dec[i + 1] == 'x') //lw x between 2duplicateLetters remove x
                {
                    continue;
                }
                else 
                 {
                    DEC += Dec[i + 1];
               
                 }
              
            }
            DEC += Dec[Dec.Length - 2];
            if (Dec[Dec.Length - 1] != 'x')
             {
               
                DEC += Dec[Dec.Length - 1];
             }
           
            Dec= DEC;
            return Dec;
        }
        
    
        public string Encrypt(string plainText, string key)
        {

            String Enc = "";

            //First remove Duplicated letters
            key = key.ToLower();
            string myKey = key;
            string inp = "";
            HashSet<string> HS = new HashSet<string>();
            for (int i = 0; i < myKey.Length; i++)
            {
                if (myKey[i].Equals("i") || myKey[i].Equals("j"))
                {
                    HS.Add("i");
                    continue;
                }
                HS.Add(myKey[i].ToString());


            }
            string[,] arr = new string[5, 5];
            //inp = HS.ToString();
            inp = string.Join("", HS.ToArray()); // .NET 4.0
            //Second fill 5x5 array
            

            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (count < inp.Length)
                    {
                        arr[i, k] = inp[count].ToString();
                    }
                    count++;





                }
            }

            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {
                    if (string.IsNullOrEmpty(arr[i, k]))
                    {

                        for (char d = 'z'; d >= 'a'; d--)
                        {
                            if (checkLetterinArr(d,arr) || d == 'j')
                            {
                                continue;
                            }
                            else
                            {
                                arr[i, k] = d.ToString();
                            }
                        }

                    }





                }
            }
            //Third Encrypt
            plainText = plainText.ToLower();
            /* if(plainText.Length%2!=0)
             {
                 plainText = plainText + "z";
             }*/
            StringBuilder sb = new StringBuilder(plainText);

            for (int i = 0; i < sb.Length; i += 2)
            {

                if (i == sb.Length - 1&& sb.Length % 2 == 1)
                {

                    sb.Append("x");

                }

                else if (sb[i] == sb[i + 1])
                    sb.Insert(i + 1, 'x');
            }
            plainText = sb.ToString();
            string[] plain = new string[plainText.Length / 2];
            count = 0;
            for (int i = 0; i < plainText.Length; i++)
            {
                if(count ==plainText.Length)
                {
                    break;
                }
                plain[i] = plainText[count].ToString()+plainText[count + 1].ToString();
                count++;
                count++;
            }
            //If both the letters are in the same column
            string str1;
            string str2;

            for (int k = 0; k < plainText.Length/2; k++)
            {
                string tmp = plain[k];
                string let1 = tmp[0].ToString();
                string let2 = tmp[1].ToString();
                if(let1.Equals("e")&&let2.Equals("g"))
                {
                    int eee;
                    eee = 2;
                }    

                for (int i = 0; i < 5; i++)
                {
                    for (int z = 0; z < 5; z++)
                    { 
                        if(arr[i,z].Equals(let1))
                        {
                            bool f1 = false;
                            bool f2 = false;
                            int rect = -1;
                            for (int m = 0; m < 5; m++)
                            {
                               
                                str2 = Enc;
                                
                                //same column
                                if (arr[m,z].Equals(let2))
                                {
                                    if(i==4&&m!=4)
                                    {
                                    Enc += arr[0 , z] + arr[ m+1, z];
                                    f1 = true;
                                        rect = 1;
                                    }
                                    else if(i!=4&&m!=4)
                                    {
                                    Enc += arr[i+1 , z] + arr[ m+1, z];
                                    f1 = true;
                                        rect = 1;
                                    }
                                    else if(i!=4&&m==4)
                                    {
                                    Enc += arr[i+1 , z] + arr[ 0, z];
                                    f1 = true;
                                        rect = 1;
                                    }

                                    //Enc += arr[i , z+1] + arr[i, m+1];
                                }
                                //same row
                                if (arr[i,m].Equals(let2)) //m!=
                                {
                                    //z+1 m+1
                                    if (z != 4&&m!=4)
                                    {
                                        Enc += arr[i, z + 1] + arr[i, m + 1];
                                    f2 = true;
                                        rect = 2;
                                    }
                                    else if (z != 4&&m==4)
                                    {
                                        Enc += arr[i, z + 1] + arr[i, 0];
                                    f2 = true;
                                        rect = 2;

                                    }
                                    else if (z == 4&&m!=4)
                                    {
                                        Enc += arr[i,0] + arr[i, m+1];
                                        f2 = true;
                                        rect = 2;


                                    }


                                }
                                 






                            }
                            if (rect==-1)//Rect
                            {


                                
                                int x = -1, y = -1;

                                for (int i2 = 0; i2 < 5; i2++)
                                {
                                    for (int z2 = 0; z2 < 5; z2++)
                                    {
                                        if (arr[i2, z2].Equals(let2))
                                        {
                                            x = i2;
                                            y = z2;
                                            Enc += arr[i, y] + arr[x, z];
                                            
                                        }


                                    }
                                   

                                }
                            }
                        }






                    }
                }




                    }

            return Enc;

            //throw new NotImplementedException();
        }
        public bool checkLetterinArr(char letter, string[,] arr)
        {
           


            for (int i = 0; i < 5; i++)
            {
                for (int k = 0; k < 5; k++)
                {

                    if(arr[i,k]==letter.ToString())
                    {
                        return true;
                    }

                }
            }
            return false;
        }
    }
}
