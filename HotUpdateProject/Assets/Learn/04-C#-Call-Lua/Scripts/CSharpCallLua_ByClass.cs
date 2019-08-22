using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class CSharpCallLua_ByClass : MonoBehaviour
{
	LuaEnv EnvLua;

	void Start()
	{
		EnvLua = new LuaEnv();
		EnvLua.DoString("require 'CCallLua'");

		ShipClass myShip = EnvLua.Global.Get<ShipClass>("ship");
		Debug.Log("by class");
		Debug.Log("name		" + myShip.name);
		Debug.Log("year		" + myShip.year);
		Debug.Log("damage		" + myShip.damage);
		myShip.Fire(666);
	}

	private void OnDestroy()
	{
		EnvLua.Dispose();
	}

	class ShipClass
	{
		public string name;
		public int year;
		public float damage;
		public void Fire(float value) { }
	}
}
