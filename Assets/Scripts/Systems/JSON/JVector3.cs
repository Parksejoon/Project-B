using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JVector3
{
	[JsonProperty("x")]
	public float x;
	[JsonProperty("y")]
	public float y;
	[JsonProperty("z")]
	public float z;


	public JVector3()
	{
		x = y = z = 0f;
	}

	public JVector3(Vector3 v)
	{
		x = v.x;
		y = v.y;
		z = v.z;
	}

	public JVector3(float f)
	{
		x = y = z = f;
	}
}
