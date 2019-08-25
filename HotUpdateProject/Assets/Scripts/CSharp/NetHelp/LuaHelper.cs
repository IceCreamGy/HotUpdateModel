using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using XLua;

public class LuaHelper : MonoBehaviour
{
	private static LuaHelper _instance;
	public static LuaHelper Instance
	{
		get
		{
			if (Instance == null)
			{
				_instance = new LuaHelper();
			}
			return _instance;
		}
	}

	private LuaEnv _luaEnv = new LuaEnv();
	//缓存Lua文件与对应的信息
	private Dictionary<string, byte[]> _DicLuaFile = new Dictionary<string, byte[]>();
	private LuaHelper()
	{
		_luaEnv.AddLoader(customLoader);
	}

	private byte[] customLoader(ref string fileName)
	{
		string luaPath = PathTools.ABPath_Directory + PathTools.LuaPath_Publish;
		if (_DicLuaFile.ContainsKey(fileName))
		{
			return _DicLuaFile[fileName];
		}
		else
		{
			return ProcessDIR(new DirectoryInfo(luaPath), fileName);
		}
	}

	//根据Lua文件名，取得Lua内容信息
	private byte[] ProcessDIR(FileSystemInfo fileSystemInfo, string fileName)
	{
		DirectoryInfo dirInfo = fileSystemInfo as DirectoryInfo;
		FileSystemInfo[] files = dirInfo.GetFileSystemInfos();
		foreach (FileSystemInfo item in files)
		{
			FileInfo file = item as FileInfo;
			if (file == null)
			{
				//如果是文件夹，则继续递归
				ProcessDIR(item, fileName);
			}
			else
			{
				//如果是文件
				string fileNameNoExt = item.Name.Split('.')[0];
				if(item.Extension==".meta"|| fileNameNoExt!= fileName)
				{
					continue;
				}
				byte[] bytes = File.ReadAllBytes(item.FullName);
				//添加到缓存集合
				_DicLuaFile.Add(fileNameNoExt, bytes);

				return bytes;
			}
		}
		return null;
	}   //ProcessDIR_End

	//执行Lua代码
	public void DoString(byte[] chunk, string chunkName = "chunk", LuaTable env = null)
	{
		_luaEnv.DoString(chunk, chunkName, env);
	}
	//调用Lua方法
	public object[] CallLuaFunction(string luaScriptName, string luaMethodName, params object[] args)
	{
		LuaTable luaTable = _luaEnv.Global.Get<LuaTable>(luaScriptName);
		LuaFunction luaFun = luaTable.Get<LuaFunction>(luaMethodName);
		return luaFun.Call(args);
	}
}
