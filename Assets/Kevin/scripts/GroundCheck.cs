using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	[SerializeField]
	GameObject dustCloud;

	[SerializeField] private LayerMask groundLayer;

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.gameObject.tag.Equals ("Ground"))
			Instantiate (dustCloud, transform.position, dustCloud.transform.rotation);
	}
}
