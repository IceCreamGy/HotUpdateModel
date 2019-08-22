using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnVector3 : MonoBehaviour
{
	public Transform TargetTrans;
	public float MoveSpeed;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, TargetTrans.position, MoveSpeed * Time.deltaTime);
	}
}
