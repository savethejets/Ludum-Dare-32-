using UnityEngine;
using System.Collections;
using Movement.Platformer;
using Movement;

public class Hittable : MonoBehaviour, ICollision {

	private Animator _anim;
	private bool _killNext = false;

	public bool KillOnNextCollision;

	// Use this for initialization
	void Start () {
		_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ICollision implementation

	public void OnCollisionEnter2D (Collision2D other)
	{

		if (!_killNext) {

			_killNext = true;

			var direction = Camera.main.ScreenToWorldPoint (Input.mousePosition);

			var object_pos = GameObject.FindGameObjectWithTag ("Player").transform.position;

			direction.x = direction.x - object_pos.x;
			direction.y = direction.y - object_pos.y;

			var bounce = gameObject.GetComponent<Bounce> ();

			bounce.Direction = Vector3.ClampMagnitude (direction, 1f);
			bounce.IsEnabled = true;

			GetComponent<ICharacterController> ().enabled = true;

			GameObject.Instantiate (Resources.Load ("Hit"), other.contacts[0].point, Quaternion.identity);
		} else {

			if (KillOnNextCollision) {

				if (_anim != null) {
					_anim.Play ("Kill");
				}
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

	public void Destroy() {
		Destroy (this.gameObject);
	}
}
