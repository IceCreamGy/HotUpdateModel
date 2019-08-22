using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle
{
	[MenuItem("AssetBundleTool/BuildAllAssetBundles")]
	public static void BuildAllAB()
	{
		string strABOutPathDir = string.Empty;
		switch (EditorUserBuildSettings.activeBuildTarget)
		{
			case BuildTarget.Android:
				strABOutPathDir = Application.streamingAssetsPath + PathTools.ABPath_Android;
				break;
			case BuildTarget.iOS:
				strABOutPathDir = Application.streamingAssetsPath + PathTools.ABPath_iOS;
				break;
			case BuildTarget.StandaloneWindows64:
				strABOutPathDir = Application.streamingAssetsPath + PathTools.ABPath_Windows64;
				break;
			default:
				break;
		}


		if (!Directory.Exists(Application.streamingAssetsPath))
		{
			Directory.CreateDirectory(Application.streamingAssetsPath);
		}
		if (!Directory.Exists(Application.streamingAssetsPath + PathTools.ABPath_Directory))
		{
			Directory.CreateDirectory(Application.streamingAssetsPath + PathTools.ABPath_Directory);
		}

		if (!Directory.Exists(strABOutPathDir))
		{
			Directory.CreateDirectory(strABOutPathDir);
		}
		Debug.Log("AB 包的生成路径 " + strABOutPathDir);

		//1>		生成AB包
		BuildPipeline.BuildAssetBundles(strABOutPathDir, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
		//2>		拷贝Lua文件
		CopyLuaFileToSA.CopyLuaFile();
		//3>		生成校验文件
		CreatVerifyFile.CreatVerifyFileMethod();
	}
}
