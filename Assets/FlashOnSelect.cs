using UnityEngine;
using System.Collections;

public class FlashOnSelect : MonoBehaviour
{
	public float flashOffset = 0.3f;
	public bool stopFlashing = false;
	float lastFlash = 0.0f;
	bool vis = true;

	void Update()
	{
		if (lastFlash + flashOffset <= Time.timeSinceLevelLoad)
		{
			lastFlash = Time.timeSinceLevelLoad;
			vis = !vis;
			if (vis || stopFlashing)
			{
				gameObject.GetComponent<SpriteRenderer>().color = Color.white;
			}
			else
			{
				gameObject.GetComponent<SpriteRenderer>().color = Color.black;
			}
		}
	}
}
