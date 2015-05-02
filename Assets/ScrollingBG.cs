using UnityEngine;
using System.Collections;

public class ScrollingBG : MonoBehaviour
{
	public Vector2 speed = new Vector2(1.0f, 1.0f);
	Vector2 offset       = Vector2.zero;
	Material mat         = null;

	void Start()
	{
		mat = gameObject.GetComponent<MeshRenderer>().materials[0];
	}
	
	void Update()
	{
		offset += speed * Time.deltaTime;
		mat.SetTextureOffset("_MainTex", offset);
	}
}
