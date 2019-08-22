using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class CSharpCallLua_ByLuaTable : MonoBehaviour
{
	LuaEnv EnvLua;

	void Start()
	{
		EnvLua = new LuaEnv();
		EnvLua.DoString("require 'CCallLua'");

		//获取LuaTable
		LuaTable myTalbe = EnvLua.Global.Get<LuaTable>("ship");
		Debug.Log("name	" + myTalbe.Get<string>("name"));
		Debug.Log("level		" + myTalbe.Get<int>("level"));
		Debug.Log("canAttack	" + myTalbe.Get<bool>("canAttack"));

		//LuaTable中，LuaFunction 参数的输入
		LuaFunction myFunction = myTalbe.Get<LuaFunction>("Fire");
		myFunction.Call(myTalbe, 4399);

		//LuaTable中，LuaFunction 返回值的输出
		LuaFunction myFunction_LevelUp = myTalbe.Get<LuaFunction>("levelUp");
		object[] objArray = myFunction_LevelUp.Call();
		Debug.Log("升级后为：   " + objArray[0]);
	}

	private void OnDestroy()
	{
		EnvLua.Dispose();
	}
}
