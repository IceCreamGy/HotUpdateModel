using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System.IO;

//拷贝Lua文件到 StreamingAsset 目录
public class CopyLuaFileToSA
{
	//编辑区
	static string sourcePath = Application.dataPath + PathTools.LuaPath_Editor;
	//发布区
	static string targetPath = Application.streamingAssetsPath + "/LUA";

	[MenuItem("AssetBundleTool/CopyLuaToSA")]
	public static void CopyLuaFile()
	{
		//参数检查
		Debug.Log("Lua sourcePath " + sourcePath);
		Debug.Log("Lua targetPath " + targetPath);

		//定义目录与文件结构
		DirectoryInfo dirInfo = new DirectoryInfo(sourcePath);
		FileInfo[] files = dirInfo.GetFiles();
		FileSystemInfo[] fileSystems = dirInfo.GetFileSystemInfos();
		Debug.Log("fileSystems  " + fileSystems.Length);

		//检查目标路径，不存在 则创建
		if (!Directory.Exists(targetPath))
		{
			Directory.CreateDirectory(targetPath);
		}

		CopyMethod(new DirectoryInfo(sourcePath));

		//Unity资源管理器刷新
		AssetDatabase.Refresh();


		Debug.Log("CopyLuaFileToSA/CopyLuaFile/		Lua文件拷贝完成");
	}

	private static void CopyMethod(FileSystemInfo fileSystemInfo)
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
				//若为文件，则拷贝

				//win路径分隔符，与unity路径分隔符不一样，需要转换
				string strFileFullName = fileInfo.FullName.Replace("\\", "/");
				//文件后缀
				string fileExt = Path.GetExtension(strFileFullName);
				if (fileExt.EndsWith(".meta"))
				{
					continue;
				}

				fileInfo.CopyTo(targetPath + "/" + item.Name, true);
			}
			else
			{
				//如果是文件夹，就递归调用下一层文件夹
				CopyMethod(item);
			}
		}
	}
}


//--说明：
// 为什么，不直接放在StreamingAsset目录下呢？因为后期删除所有AB包的时候，会把“StreamingAsset目录”清空