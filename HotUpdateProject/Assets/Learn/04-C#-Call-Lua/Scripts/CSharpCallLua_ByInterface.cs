using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class CSharpCallLua_ByInterface : MonoBehaviour
{
	LuaEnv EnvLua;

	void Start()
	{
		EnvLua = new LuaEnv();
		EnvLua.DoString("require 'CCallLua'");

		//获取table测试-By Interface
		IShip myShip = EnvLua.Global.Get<IShip>("ship");
		Debug.Log("By Interface");
		Debug.Log("name		" + myShip.name);
		Debug.Log("year		" + myShip.year);
		Debug.Log("damage		" + myShip.damage);
		myShip.Fire(666);
	}

	private void OnDestroy()
	{
		EnvLua.Dispose();
	}

	[CSharpCallLua]
	interface IShip
	{
		string name { get; set; }
		int year { get; set; }
		float damage { get; set; }
		void Fire(float value);
	}
}
