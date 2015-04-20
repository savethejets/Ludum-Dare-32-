using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using Movement.Platformer;
using Events;
using Sound;


public class Alien : MonoBehaviour, ICollision {

	private Health _health;
	private Animator _anim;
	private Transform _player;
	private Transform _gun;
	private SpriteRenderer _gunSprite;

	private bool _isAttacking = false;

	public GameObject BulletToLoad;
	public ColliderCheck HurtColliderCheck;
	public float SightDistance;
	public float TimeToWaitMinBeforeAttacking;
	public float TimeToWaitMaxBeforeAttacking;
	public string EventOnKill;

	void Start () {
		_health = GetComponentInChildren<Health> ();
		_anim = GetComponentInChildren<Animator> ();
		_health.DoOnKill = DoOnDeath;
		_player = GameObject.FindGameObjectWithTag ("Player").transform;
		_gun = transform.FindChild ("Sprite").FindChild("Gun").transform;
		_gunSprite = _gun.GetComponent<SpriteRenderer> ();
	}		

	IEnumerator WaitThenAttack(float randomTime) {
		yield return new WaitForSeconds (randomTime);
		Attack ();
	}

	void Attack () {
		if (!_health.IsDead ()) {
			_gun.GetComponent<AlienGun>().Shoot ();
		}
		_isAttacking = false;
	}

	void Update () {
		if (!_health.IsDead() && Vector2.Distance (_player.position, transform.position) < SightDistance) {

			var direction = _player.position - transform.position;

			_anim.SetFloat ("XDirection", direction.x);
			_anim.SetFloat ("YDirection", direction.y);

			float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;

			_gun.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));

			if (direction.y < 0) {
				_gunSprite.sortingOrder = 3;
			} else {
				_gunSprite.sortingOrder = 0;
			}

			if (direction.x > 0) {
				_gunSprite.transform.localScale = new Vector3(_gunSprite.transform.localScale.x, 1, _gunSprite.transform.localScale.z) ;
			} else {
				_gunSprite.transform.localScale = new Vector3(_gunSprite.transform.localScale.x, -1, _gunSprite.transform.localScale.z) ;
			}

			if (!_isAttacking) {
				_isAttacking = true;
				StartCoroutine (WaitThenAttack (Random.Range(TimeToWaitMinBeforeAttacking, TimeToWaitMaxBeforeAttacking)));
			}
		}
	}

	public void DoOnDeath () {
		_anim.Play ("Death");

		if (EventOnKill != null) {
			EventManager.Instance.PostNotification (EventOnKill, new Notification());
		}

		SoundManager.Instance.Play (Sounds.Effects.KILL);

		GetComponent<BoxCollider2D> ().enabled = false;
	}


	#region ICollision implementation

	public void OnCollisionEnter2D (Collision2D other)
	{
		if (HurtColliderCheck.Check (other.collider) && !_health.IsDead() && other.gameObject.GetComponent<IMyDamage>() != null) {

			_health.Hurt (other.gameObject.GetComponent<IMyDamage>().Damage);

			var spriteRenderer = GetComponentInChildren<SpriteRenderer> ();

			GameObject.Instantiate (Resources.Load ("Hit"), other.contacts[0].point, Quaternion.identity);

			SoundManager.Instance.Play (Sounds.Effects.HURT);

			spriteRenderer.color = Color.white;
			HOTween.To (spriteRenderer, 0.1f, new TweenParms().Prop("color", Color.red).Loops(2, LoopType.Yoyo));

			for (int i = 1; i <= 20; i++) {
				GameObject obj = (GameObject)Instantiate (Resources.Load ("AlienBlood"), other.contacts[0].point, Quaternion.identity);
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
