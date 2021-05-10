using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
          
         // throw new NotImplementedException();
            int p_len = plainText.Length;
            int c_len = cipherText.Length;
            plainText = plainText.ToLower();
            cipherText = cipherText.ToLower();
            List<int> indecies = new List<int>();
            List<int> key = new List<int>();
            int _colum = 1;
            for (int i = 0; i < p_len; i++)
            {
                for (int j = 0; j < c_len; j++)
                {
                    int p = plainText[j];
                    int c = cipherText[i];
                    if ( p == c)
                    {
                        for (int w = j + 1; w < c_len; w++)
                        {
                            int _p = plainText[w];
                            int new_i = i + 1;
                            if (  _p != cipherText[new_i])
                            {
                                _colum += 1;
                            }
                            else
                            {
                                break;
                            
                            }

                        }
                    }
                    if (_colum <= 1) continue;
                   else break;
                }
                if (_colum <= 1) continue;
                else break;
            }
            int rows = c_len / _colum;
            int diff = -1;
            if (c_len % _colum != 0)
            {
                int tmp = c_len;
                rows++;
                do
                {
                    if(tmp % _colum != 0)
                    {
                        diff++;
                        tmp++;
                    }


                } while (tmp % _colum != 0);
            }



            int idx = 1;
            for (int i = 0; i < c_len; i += rows)
            {
                bool flag = false;
                for (int j = 0; j < _colum; j++)
                {
                    int p = plainText[j];
                    int c = cipherText[i];
                    if (i < c_len - 1 && c == p)
                    {
                        if (plainText[j + _colum] == cipherText[i + 1] && !indecies.Contains(j))
                        {
                            indecies.Add(j);
                            idx++;
                            flag = true;
                            break;
                        }
                    }
                    else
                    {
                        if (plainText[j + _colum] == c && !indecies.Contains(j))
                        {
                            if (i > 0 && p == cipherText[i - 1])
                            {
                                flag = true;
                                indecies.Add(j);
                                idx++;
                                break;
                            }
                        }
                    }
                }
                if (!flag)
                {
                    for (int j = 0; j < _colum; j++)
                    {
                        int p = plainText[j];
                        if (cipherText[i - diff] == p && !indecies.Contains(j))
                        {
                            if (plainText[j + _colum] == cipherText[i - diff + 1])
                            {
                                indecies.Add(j);
                                idx++;
                                if (flag == true) break;
                                else continue;
                            }
                        }
                    }
                }
            }





            for (int i = 0; i < _colum; i++)
            {
                for (int j = 0; j < _colum; j++)
                {
                    if (indecies[j] != i)
                    {
                        
                        continue;
                    }
                    else
                    {
                        key.Add(j + 1);
                        break;
                    }

                }
            }
            return key;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        }

        public string Decrypt(string cipherText, List<int> key)
        {
           // throw new NotImplementedException();
             int numcol = key.Max();
            cipherText = cipherText.ToLower();
            int c_len = cipherText.Length;
            int numrow = c_len/ numcol;
            int c_chk = 0;
            int[,] arr = new int[numrow, numcol];
            int[,] sort = new int[numcol, numrow];
            string p_txt = "";
       
            for (int i = 0; i < numcol; i++)
            {
               
                for (int j = 0; j < numrow; j++)
                {
                    
                    arr[j, i] = cipherText[c_chk];
                    c_chk++;
                }
            }
                for (int i = 0; i < numcol; i++)
                {
                    for (int j = 0; j < numrow; j++)
                    {
                        sort[i, j] = arr[j, key[i] - 1];
                    }
                }
            for (int i = 0; i < numrow; i++)
            {
                for (int j = 0; j < numcol; j++)
                    p_txt = p_txt+ (char)sort[j, i];

            }
            return p_txt;
        
        
        }

        public string Encrypt(string plainText, List<int> key)
        {
         //   throw new NotImplementedException();
             string cipher = "";
            char addx = 'x';
            int chk = 0;
            int _coln = key.Count;
            int numrow = (plainText.Length / _coln);
            if (plainText.Length%_coln !=0)
                {
                numrow++;
                }
            char[,] _mat = new char[numrow, _coln];  
                for(int i=0;i<numrow;i++)
                {
                    for(int q=0;q<_coln;q++)
                     { 
                           if(chk==plainText.Length)
                            { _mat[i, q] = addx; }
                           else
                            {
                        _mat[i, q] = plainText[chk];
                        chk++;
                           }
                
                      }
           
                 }

           for(int i=1;i<=_coln;i++)
            {
                int _posindx = key.IndexOf(i);
                for(int k =0;k<numrow;k++)
                {
                    cipher += _mat[k, _posindx];
                }

            }




            return cipher;
        
    
        
        }
    }
}
