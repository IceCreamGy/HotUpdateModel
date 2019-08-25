using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开始游戏的主逻辑
public class StartGame : MonoBehaviour,IStartGame
{
	public void ReceiveInfoStartRuning()
	{
		Debug.Log("启动游戏");
		LuaHelper.Instance.DoString("require 'LauchABFW'");
		LuaHelper.Instance.CallLuaFunction("LauchABFW", "StartABFW");
	}
}
