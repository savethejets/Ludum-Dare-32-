using UnityEngine;
using System.Collections;
using Events;
using Holoville.HOTween;

public class ReloadWhenDead : MonoBehaviour {

	public Alien[] AliensToReload;
	public Transform RespawnPoint;
	public string EventToFireWhenAllAreDead;
	public ReloadWhenDead ReloadToEnable;

	// Use this for initialization
	void Start () {
		
	}

	public void ResetPlayer() {
		var player = GameObject.FindGameObjectWithTag ("Player");

		player.transform.position = RespawnPoint.transform.position;
		player.GetComponentInChildren<Health> ().Heal ();

		var camera = GameObject.FindGameObjectWithTag ("MainCamera");
		camera.transform.position = RespawnPoint.transform.position;

		var red = GameObject.Find ("Red").GetComponent<SpriteRenderer>();

		HOTween.To (red, 1f, new TweenParms ().Prop ("color", Color.white));
		HOTween.To (red, 1f, new TweenParms ().Prop ("color", Color.clear).Delay(1f));


	}

	public void ReloadAliens() {

		foreach (var alien in AliensToReload) {
			alien.GetComponentInChildren<Health> ().Heal();
			alien.GetComponentInChildren<Animator> ().Play("Idle");
			alien.GetComponent<BoxCollider2D> ().enabled = true;
		}

	}
	
	// Update is called once per frame
	void Update () {
		bool allDead = true;
		foreach (var aliens in AliensToReload) {
			if (! aliens.GetComponentInChildren<Health> ().IsDead ()) {
				allDead = false;
			}
		}

		if (allDead) {
			EventManager.Instance.PostNotification (EventToFireWhenAllAreDead, new Notification ());
			ReloadToEnable.enabled = true;
			this.enabled = false;
		}

	}
}
