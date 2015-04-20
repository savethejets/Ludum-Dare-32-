using UnityEngine;
using System.Collections;

public interface AlienGun {
	void Shoot();
}

public class AlienPistol : MonoBehaviour, AlienGun {

	public GameObject BulletToFire;

	// Use this for initialization
	void Start () {
	
	}

	#region AlienGun implementation

	public void Shoot ()
	{

		var spawnPoint = transform.FindChild ("BulletSpawn").transform.position;

		var bullet = (GameObject) GameObject.Instantiate (BulletToFire, spawnPoint, transform.rotation);

	}

	#endregion
	
	// Update is called once per frame
	void Update () {
	
	}
}
