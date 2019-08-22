using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreatVerifyFile : MonoBehaviour
{
	[MenuItem("AssetBundleTool/CreatVerifyFile")]
	public static void CreatVerifyFileMethod()
	{
		Debug.Log("生成校验文件");

		//定义局部变量
		string abOutPath = string.Empty;                            //AssetBundle的输出路径 
		string VeriyFilesOutPath = string.Empty;                //校验文件的路径
		List<string> fileList = new List<string>();             //储存所有合法文件的路径信息

		//定义校验文件的输出路径
		string streamingPath = Application.streamingAssetsPath;
		abOutPath = streamingPath + "/" + PathTools.LuaPath_Publish;
		Debug.Log("abOutPath " + abOutPath);
		VeriyFilesOutPath = abOutPath + ABDefine.VeriyFilesName;

		//如果项目已有校验文件，则进行覆盖
		if (File.Exists(VeriyFilesOutPath))
		{
			File.Delete(VeriyFilesOutPath);
		}

		//遍历当前文件夹（校验文件的输出路径）的所有文件，生成MD5编码
		ListFile(new DirectoryInfo(abOutPath), ref fileList);

		//把 "文件路径" 与 "对应的MD5码" ，写入校验文件
		WriteVerifyFile(VeriyFilesOutPath, fileList);
	}

	/// <summary>
	/// 遍历校验文件的输出路径，得到所有合法文件
	/// </summary>
	/// <param name="fileSystemInfo">文件（夹）路径信息</param>
	/// <param name="fileList">把所有合法文件的相对路径写入集合</param>
	private static void ListFile(FileSystemInfo fileSystemInfo, ref List<string> fileList)
	{
		//文件系统转为目录系统
		DirectoryInfo dirInfo = fileSystemInfo as DirectoryInfo;
		//获取文件夹下的所有文件信息（文件系统包括：文件、文件夹）
		FileSystemInfo[] fileSystemInfos = dirInfo.GetFileSystemInfos();        //这里我们把文件与目录，都看成文件系统信息

		foreach (FileSystemInfo item in fileSystemInfos)
		{
			//假定先是一个文件类型
			FileInfo fileInfo = item as FileInfo;

			if (fileInfo != null)
			{
				//如果是文件，就尝试写入集合

				//win路径分隔符，与unity路径分隔符不一样，需要转换
				string strFileFullName = fileInfo.FullName.Replace("\\", "/");
				//文件后缀
				string fileExt = Path.GetExtension(strFileFullName);
				if (fileExt.EndsWith(".meta"))
				{
					continue;
				}

				fileList.Add(strFileFullName);
			}
			else
			{
				//如果是文件夹，就递归调用下一层文件夹
				ListFile(item, ref fileList);
			}
		}
	}

	/// <summary>
	/// 把文件路径和MD5码 写入txt文件
	/// </summary>
	/// <param name="verifyFileOutPath">写入校验文件的路径</param>
	/// <param name="fileLists">储存所有合法文件的相对路径集合</param>
	private static void WriteVerifyFile(string verifyFileOutPath, List<string> fileLists)
	{
		using (FileStream fs = new FileStream(verifyFileOutPath, FileMode.CreateNew))
		{
			using (StreamWriter sw = new StreamWriter(fs))
			{
				for (int i = 0; i < fileLists.Count; i++)
				{
					string path = fileLists[i];
					string md5 = Helps.GetMD5(path);    //todo
					path = path.Replace(Application.streamingAssetsPath + "/", "");

					//做参数检查，写入文件前

					//写入文件
					sw.WriteLine(path + "|" + md5);

				}
			}
		}

		Debug.Log("CreatVerifyFile/WriteVerifyFile/ 用户操作成功");

		AssetDatabase.Refresh();
	}
}
