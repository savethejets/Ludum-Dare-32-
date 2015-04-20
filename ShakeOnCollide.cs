using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Sound;

public class ShakeOnCollide : MonoBehaviour, ICollision {

	public ColliderCheck Collider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ICollision implementation

	public void OnCollisionEnter2D (Collision2D other)
	{
		if (Collider.Check (other.collider)) {
		
			GameObject.Instantiate (Resources.Load ("Hit"), other.contacts[0].point, Quaternion.identity);
			CameraShake.Instance ().Shake (0.9f, 0.04f);

			SoundManager.Instance.Play (Sounds.Effects.HURT);
//			GlobalEffectsController.Instance.Sleep (0.05f);
		}
	}

	public void OnCollisionExit2D (Collision2D other)
	{

	}

	public void OnCollisionStay2D (Collision2D other)
	{

	}

	#endregion

	#region ITrigger implementation

	public void OnTriggerEnter2D (Collider2D other)
	{

	}

	public void OnTriggerExit2D (Collider2D other)
	{

	}

	public void OnTriggerStay2D (Collider2D other)
	{

	}

	#endregion
}
