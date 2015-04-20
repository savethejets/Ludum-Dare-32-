using UnityEngine;
using System.Collections;
using Events;
using Sound;

public class Teleport : MonoBehaviour {

	public GameObject AlienToCreate;

	public bool DestroyAfterCreate;
	public string EventToTeleportOn;
	public Repeatable Repeatable;

	// Use this for initialization
	void Start () {
		EventManager.Instance.AddObserver (EventToTeleportOn, this, "RunTeleportOnEvent", Repeatable);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RunTeleportOnEvent() {
		GetComponent<Animator> ().Play ("TeleportIn");
	}

	public void TeleportIn() {
		GameObject.Instantiate (AlienToCreate, transform.position, Quaternion.identity);	

		SoundManager.Instance.Play (Sounds.Effects.TELEPORT);

		if (DestroyAfterCreate) {
			Destroy (this.gameObject);
		}
	}
}
