using UnityEngine;
using System.Collections;
using Movement;
using InputInterface;
using Holoville.HOTween;
using Movement.Platformer;
using Sound;

public class Slash : IMovementModifier {

	private bool _shouldSlash = false;
	private bool _canSlash = true;
	private Vector2 _lastPosition;
	private Vector2 _direction;
	private GameObject _sprite;
	private SpriteRenderer _swordRenderer;
	private Bounce _bounce;

	public GameObject Sword;
	public GameObject Player;
	public Animator Animation;
	public DeviceButtonMapping Button;
	public float AngleToAdd = 50f;
	public float TweenTime = 0.02f;
	public float SlashPositionAdjust = 0.3f;
	public EaseType Ease;
	public int Damage; 

	public bool SlashyDebug;

	#region implemented abstract members of IMovementModifier

	public override void Do () {	
		Debug.DrawLine (Player.transform.position, Sword.transform.position);

		if (_shouldSlash && _canSlash || SlashyDebug) {
			_canSlash = false;

			var _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			var object_pos = transform.position;

			_mousePosition.x = _mousePosition.x - object_pos.x;
			_mousePosition.y = _mousePosition.y - object_pos.y;

			var pos = Sword.transform.position + Vector3.ClampMagnitude (_mousePosition, SlashPositionAdjust);

			SoundManager.Instance.Play (Sounds.Effects.SWIPE);

			var obj = (GameObject) GameObject.Instantiate (Resources.Load ("Slash"), pos, Sword.transform.rotation);		

			HOTween.To(Sword.transform, TweenTime, new TweenParms().Prop("localRotation", new Vector3(0,0, Sword.transform.localRotation.eulerAngles.z - AngleToAdd)).Ease(Ease));
			HOTween.To(Sword.transform, TweenTime, new TweenParms().Prop("localRotation", new Vector3(0,0, Sword.transform.localRotation.eulerAngles.z + AngleToAdd)).Delay(TweenTime).Ease(Ease));
			HOTween.To(Sword.transform, TweenTime, new TweenParms().Prop("localRotation", new Vector3(0,0, Sword.transform.localRotation.eulerAngles.z)).Delay(TweenTime * 2).Ease(Ease).OnComplete(CanSlash));

			_bounce.Direction = _mousePosition.normalized;
			_bounce.IsEnabled = true;

			_shouldSlash = false;
		}
	}

	public void CanSlash () {
		_canSlash = true;
	}

	public override void DoAfter ()
	{
		_direction = (Vector2) transform.position - _lastPosition;
		_lastPosition = transform.position;
			
		Animation.SetFloat ("XDirection", _direction.x);
		Animation.SetFloat ("YDirection", _direction.y);
		Animation.SetBool ("Run", Mathf.Abs(CharacterController.Velocity.x) > 0.1 || Mathf.Abs(CharacterController.Velocity.y) > 0.1);
	}

	#endregion

	void Start () {
		Animation = GetComponentInChildren<Animator> ();
		_sprite = transform.GetChild (0).gameObject;
		_swordRenderer = Sword.GetComponent<SpriteRenderer> ();
		_bounce = GetComponent<Bounce> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (_sprite.transform.localScale.x < 0) {
			_sprite.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		} else {
			_sprite.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}

		if (Sword.transform.position.y > transform.position.y) {
			_swordRenderer.sortingLayerID = 0;
		} else {
			_swordRenderer.sortingLayerID = 2;
		}

		_shouldSlash = InputController.WasPressed (Button);

	}

}
