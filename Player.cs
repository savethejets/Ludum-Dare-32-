using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Sound;

public class Player : MonoBehaviour, ICollision {

	private Health _health;

	public ColliderCheck BulletColliderCheck;

	void Start () 
	{
		_health = GetComponentInChildren<Health> ();
	}

	void Update () 
	{
	
	}

	#region ICollision implementation

	public void OnCollisionEnter2D (Collision2D other)
	{
		if(BulletColliderCheck.Check(other.collider)) 
		{
			_health.Hurt (other.gameObject.GetComponent<IMyDamage> ().Damage);

			Destroy(other.gameObject, 0.5f);

			var spriteRenderer = transform.FindChild("Sprite").GetComponent<SpriteRenderer> ();

			spriteRenderer.color = Color.white;

			SoundManager.Instance.Play (Sounds.Effects.HURT);

			HOTween.To (spriteRenderer, 0.1f, new TweenParms().Prop("color", Color.red).Loops(2, LoopType.Yoyo));

			for (int i = 1; i <= 20; i++) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("HumanBlood"), other.contacts[0].point, Quaternion.identity);
				obj.transform.GetComponent<Rigidbody2D> ().AddForce ((other.contacts[0].normal.normalized * 0.01f) + new Vector2 (Random.Range (-0.008f, 0.008f), Random.Range (0, 0.006f)), ForceMode2D.Impulse);

			}
		}
	}

	public void OnCollisionExit2D (Collision2D other)
	{

	}

	public void OnCollisionStay2D (Collision2D other)
	{

	}

	#endregion
}
