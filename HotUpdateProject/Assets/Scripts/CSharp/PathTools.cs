using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathTools
{
	//AssetBundle路径
	public const string ABPath_Directory = "/AbRes";
	public const string ABPath_Windows64 = "/AbRes/StandaloneWindows64";
	public const string ABPath_iOS = "/AbRes/iOS";
	public const string ABPath_Android = "/AbRes/Android";

	//Lua文件的编辑区路径
	public const string LuaPath_Editor = "/Scripts/LuaScripts/src";
	//Lua文件的发布区路径
	public const string LuaPath_Publish = "/Lua";

	//服务器链接地址
	public const string ServerURL = "http://127.0.0.1:8080/UpdateAsset";
}
