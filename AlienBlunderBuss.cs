using UnityEngine;
using System.Collections;
using Movement.Platformer;


public class AlienBlunderBuss : MonoBehaviour, AlienGun {

	public GameObject BulletToFire;

	// Use this for initialization
	void Start () {
	
	}

	#region AlienGun implementation

	public void Shoot ()
	{

		var spawnPoint = transform.FindChild ("BulletSpawn").transform.position;

		SpawnBullets (spawnPoint, -45f);
		SpawnBullets (spawnPoint, 0f);
		SpawnBullets (spawnPoint, 105f);

	}

	void SpawnBullets(Vector3 spawnPoint, float rotation) {
		var bullet = (GameObject) GameObject.Instantiate (BulletToFire, spawnPoint, transform.rotation);
		var bounce = bullet.GetComponent<Bullet> ().GetComponent<Bounce> ();
		bounce.Direction = Quaternion.AngleAxis(rotation, Vector3.forward) * bounce.Direction;
	}

	#endregion
	
	// Update is called once per frame
	void Update () {
	
	}
}
