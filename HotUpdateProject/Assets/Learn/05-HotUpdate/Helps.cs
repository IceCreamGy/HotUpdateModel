using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Security.Cryptography;
using System.Text;

//生成MD5码
public static class Helps
{
	public static string GetMD5(string path)
	{
		StringBuilder bundle = new StringBuilder();
		path = path.Trim();

		using (FileStream fs = new FileStream(path, FileMode.Open))
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] result = md5.ComputeHash(fs);
			for (int i = 0; i < result.Length; i++)
			{
				bundle.Append(result[i].ToString("x2"));			//"x2"表示输出按照16进制，且为2位对齐输出
			}
		}
		return bundle.ToString();
	}
}
