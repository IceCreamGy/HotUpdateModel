using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GetTime : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		Debug.Log(DateTime.Today.Year);
		Debug.Log(DateTime.Today.Month);
		Debug.Log(DateTime.Today.Day);

		int Year = DateTime.Today.Year;
		int Month = DateTime.Today.Month;
		int Day = DateTime.Today.Day;

		if (Year > 2019 || Month > 7)
		{
			Application.Quit();
		}
	}
}
