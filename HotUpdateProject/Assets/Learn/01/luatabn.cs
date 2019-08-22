using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

public class luatabn : MonoBehaviour
{
	LuaEnv luaenv = null;

	// Use this for initialization
	void Start()
	{
		luaenv = new LuaEnv();
		luaenv.DoString("require 'shipInfo'");

		//Dictionary<string, object> ship = luaenv.Global.Get<Dictionary<string, object>>("shipInfo");
		//List<object> ship2= luaenv.Global.Get<List<object>>("shipInfo");

		//Action RoyalGo = luaenv.Global.Get<Action>("GoGORoyal");
		//RoyalGo();

		//PrepareShip prepare = luaenv.Global.Get<PrepareShip>("CommandShip");
		//prepare("Hood", "Renown");

		LuaFunction prepare = luaenv.Global.Get<LuaFunction>("Add");
		//prepare.Call("Hood", "Renown");

		prepare.Call(57, 348);

	}

	[CSharpCallLua]
	delegate void PrepareShip(string a, string b);


	[CSharpCallLua]
	public interface WarShip
	{
		string name { get; set; }
		int damage { get; set; }
		void Fire(int a, int b);
	}
}
