using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class NPC : MonoBehaviour, ITrigger {

	private GameObject _dialogIcon;

	public ColliderCheck ColliderCheck;
	public float FadeSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		_dialogIcon = transform.FindChild ("SpaceButton").gameObject;
	}

	#region ITrigger implementation

	public void OnTriggerEnter2D (Collider2D other)
	{
		if (ColliderCheck.Check (other)) {
			HOTween.To(_dialogIcon.GetComponent<SpriteRenderer> (), FadeSpeed, new TweenParms().Prop("color", Color.white));
		}
	}

	public void OnTriggerExit2D (Collider2D other)
	{
		if (ColliderCheck.Check (other)) {
			HOTween.To(_dialogIcon.GetComponent<SpriteRenderer> (), FadeSpeed, new TweenParms().Prop("color", Color.clear));
		}
	}

	public void OnTriggerStay2D (Collider2D other)
	{

	}

	#endregion
}
