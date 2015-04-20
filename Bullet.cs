using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Movement;
using Movement.Platformer;
using Sound;

public class Bullet : MonoBehaviour, ICollision, ITrigger {

	public float ScaleTime;
	public Vector3 ScaleSize;

	public ColliderCheck SlashCollider;

	#region ITrigger implementation

	public void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.name);
		if (SlashCollider.Check (other)) {
			var bounce = GetComponent<Bounce> ();
			gameObject.layer = LayerMask.NameToLayer ("Player");

			var direction = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			var object_pos = GameObject.FindGameObjectWithTag("Player").transform.position;

			direction.x = direction.x - object_pos.x;
			direction.y = direction.y - object_pos.y;

			bounce.CharacterController.Velocity = new Vector2 ();
			bounce.Direction = Vector3.ClampMagnitude (direction, 1f);

			GetComponent<IMyDamage> ().Bounce ();

			SoundManager.Instance.Play (Sounds.Effects.REBOUND);

			CameraShake.Instance ().Shake (0.8f, 0.04f);

			HOTween.To (transform, 0.05f, new TweenParms ().Prop ("localScale", new Vector3(0.3f,0.3f,1)).Loops(2,LoopType.Yoyo));

			bounce.JumpVelocity = 30;
			bounce.IsEnabled = true;
		}
	}

	public void OnTriggerExit2D (Collider2D other)
	{

	}

	public void OnTriggerStay2D (Collider2D other)
	{

	}

	#endregion

	#region ICollision implementation

	public void OnCollisionEnter2D (Collision2D other)
	{

	}

	public void OnCollisionExit2D (Collision2D other)
	{

	}

	public void OnCollisionStay2D (Collision2D other)
	{

	}

	#endregion

	void Shoot () {
		var playerPos = GameObject.FindGameObjectWithTag ("Player").transform.position;

		var direction = playerPos - transform.position;

		var bounce = GetComponent<Bounce> ();

		bounce.Direction = direction.normalized;
		bounce.IsEnabled = true;

		SoundManager.Instance.Play (Sounds.Effects.SHOOT2);
	}
		
	void Start () {
		HOTween.To (transform, ScaleTime, new TweenParms().Prop("localScale", ScaleSize).OnComplete(Shoot));
	}

}
