using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckWhileRunning : MonoBehaviour
{
	[SerializeField]
	GameObject dustCloud;
	private Rigidbody2D rb;
	bool coroutineAllowed, grounded;

    void Start()
    {
		rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Ground"))
		{
			grounded = true;
			coroutineAllowed = true;
			//Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Ground"))
		{
			grounded = false;
			coroutineAllowed = false;
		}
	}

	void Update()
	{
		if (grounded && Input.GetAxisRaw("Horizontal") != 0 && coroutineAllowed)
		{
			StartCoroutine("SpawnCloud");
			coroutineAllowed = false;
		}

		if(Input.GetAxisRaw("Horizontal") == 0f || !grounded)
		{
			StopCoroutine("SpawnCloud");
			coroutineAllowed = true;
		}
	}

	IEnumerator SpawnCloud()
	{
		while (grounded)
		{
			Instantiate(dustCloud, transform.position, dustCloud.transform.rotation);
			yield return new WaitForSeconds(0.1f);
		}
	}
}
