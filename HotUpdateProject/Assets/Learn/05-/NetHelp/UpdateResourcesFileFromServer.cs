using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

//从服务器下载更新的资源文件（包括：校验码、Ab包、Lua、配置文件）

//1>		下载“校验文件”
//2>		根据“校验文件”对比，哪些是变化的，哪些是新加的，整理到集合中
//3>		根据整理出需要更新的文件，下载资源

public class UpdateResourcesFileFromServer : MonoBehaviour
{
	public bool CheckUpdate = true;
	//PC平台的资源（包含 AssetBundle）下载存放路径
	private string _DownloadPath = string.Empty;
	//HTTP		服务器地址
	private string _ServerURL = PathTools.ServerURL;

	private void Awake()
	{
		if (CheckUpdate)
		{
			CheckAndDownload(_ServerURL);
		}
		else
		{
			BroadcastMessage(ABDefine.ReceiveInfoStartRuning, SendMessageOptions.DontRequireReceiver);
		}
	}

	/// <summary>
	/// 检查更新
	/// </summary>
	/// <param name="url"> 服务器下载URL </param>
	/// <returns></returns>
	IEnumerator CheckAndDownload(string url)
	{
		//参数检查
		if (string.IsNullOrEmpty(url))
		{
			Debug.LogError("服务器路径错误");
			yield break;
		}

		//1>		下载“校验文件”
		string fileURL = _ServerURL + ABDefine.VeriyFilesName;
		UnityWebRequest WebRequest = UnityWebRequest.Get(fileURL);
		yield return WebRequest.SendWebRequest();

		if (WebRequest.error != null)
		{
			Debug.Log(WebRequest.error);
		}
		else
		{
			//创建存放目录
			if (!Directory.Exists(_DownloadPath))
			{
				Directory.CreateDirectory(_DownloadPath);
			}
			//写入存放目录
			File.WriteAllBytes(_DownloadPath + ABDefine.VeriyFilesName, WebRequest.downloadHandler.data);

			string strServerFileText = WebRequest.downloadHandler.text;
			string[] lines = strServerFileText.Split('\n');         //按行截取

			for (int i = 0; i < lines.Length; i++)
			{
				//检查是否出现空行
				if (string.IsNullOrEmpty(lines[i]))
				{
					continue;
				}
				string[] fileAndMd5 = lines[i].Split('|');      //按符号截取
				string serverFileName = fileAndMd5[0].Trim();       //服务器端的文件名（路径）
				string serverMD5 = fileAndMd5[1].Trim();        //服务器端的MD5码

				string localFile = _DownloadPath + "/" + serverFileName;        //得到本地的这个文件

				//2>		根据“校验文件”对比，哪些是变化的，哪些是新加的，整理到集合中

				if (!File.Exists(localFile))
				{
					//从服务器下载
					string dir = Path.GetDirectoryName(localFile);
					if (!string.IsNullOrEmpty(dir))
					{
						Directory.CreateDirectory(dir);
					}
					//todo 通过www下载并写入本地
				}
				else
				{
					//todo  进行MD5码比对
				}

				//3>		根据整理出需要更新的文件，下载资源
			}
		}


		yield return null;
	}
}


/*
--------------------------------------------------------------------------------
层级设计
UpdateControl （是否跳过更新？向下通知）
CSharpStart
LuaStart
*/