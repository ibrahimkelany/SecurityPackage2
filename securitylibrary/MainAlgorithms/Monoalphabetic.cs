using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
	public class Monoalphabetic : ICryptographicTechnique<string, string>
	{
		public string Analyse(string plainText, string cipherText)
		{
            //throw new NotImplementedException();
            cipherText = cipherText.ToLower();
			plainText = plainText.ToLower();
			plainText = new string(plainText.ToCharArray().Distinct().ToArray());
			cipherText = new string(cipherText.ToCharArray().Distinct().ToArray());
			cipherText = cipherText.ToLower();
			plainText = plainText.ToLower();
			char[] unUsedAlpha = new char[26];
			char[] resultarray = Enumerable.Repeat('*', 26).ToArray();
			int unsedArrayC = 0;
			int A_ASSCI = 'a';
			int Z_ASSCI = 'z';
			for (int i = A_ASSCI; i <= Z_ASSCI; i++)
			{
				int plainindex = plainText.IndexOf((char)i);
				if (plainindex != -1)
					resultarray[i - A_ASSCI] = cipherText[plainindex];
			}
			for (int i = A_ASSCI; i < Z_ASSCI; i++)
			{
				int y = cipherText.IndexOf((char)i);
				if (y == -1)
				{
					unUsedAlpha[unsedArrayC] = (char)i;
					unsedArrayC++;
				}
			}
			int index1 = 0;
			for (int i = 0; i < 26; i++)
			{
				if (resultarray[i] == '*')
				{
					resultarray[i] = unUsedAlpha[index1];
					index1++;
				}
			}
			return string.Join("", resultarray);
		}

		public string Decrypt(string cipherText, string key)
		{
            //throw new NotImplementedException();
            string msg = "";
			int len = cipherText.Length;
			cipherText = cipherText.ToLower();
			for (int i = 0; i < len; i++)
			{
				for (int j = 0; j < key.Length; j++)
				{
					while (key[j] == cipherText[i])
					{
						int pos = j + 97;
						msg += (char)(pos);
						break;
					}

				}
			}
			return msg;
		}

		public string Encrypt(string plainText, string key)
		{
            //throw new NotImplementedException();
            string Enc = "";
			for (int i = 0; i < plainText.Length; i++)
			{
				char chr = plainText[i];
				chr = Char.ToLower(chr);
				if (chr >= 97 && chr <= 122)
				{
					int cur_pos = chr - 97;
					Enc += key[cur_pos];
				}
				else
				{
					Enc += plainText[i];
				}
			}
			return Enc;
		}

		/// <summary>
		/// Frequency Information:
		/// E   12.51%
		/// T	9.25
		/// A	8.04
		/// O	7.60
		/// I	7.26
		/// N	7.09
		/// S	6.54
		/// R	6.12
		/// H	5.49
		/// L	4.14
		/// D	3.99
		/// C	3.06
		/// U	2.71
		/// M	2.53
		/// F	2.30
		/// P	2.00
		/// G	1.96
		/// W	1.92
		/// Y	1.73
		/// B	1.54
		/// V	0.99
		/// K	0.67
		/// X	0.19
		/// J	0.16
		/// Q	0.11
		/// Z	0.09
		/// </summary>
		/// <param name="cipher"></param>
		/// <returns>Plain text</returns>
		public string AnalyseUsingCharFrequency(string cipher)
		{
            //throw new NotImplementedException();
            cipher = cipher.ToLower();
			List<HelperMONO> List = new List<HelperMONO>();

			int A_ASSCI = 'a';
			int Z_ASSCI = 'z';

			for (int i = A_ASSCI; i <= Z_ASSCI; i++)
			{
				var chara = (char)i;
				int counte = cipher.Count(q => q == chara);
				List.Add(new HelperMONO() { CharVal = chara, Count = counte });
			}

			string result = "";
			List = List.OrderByDescending(q => q.Count).ToList();
			char[] lett = "etaoinsrhldcumfpgwybvkxjqz".ToCharArray();
			for (int i = 0; i < cipher.Length; i++)
			{
				int index = List.FindIndex(q => q.CharVal == cipher[i]);
				result += lett[index];
			}
			return result;
		}
	}
}
