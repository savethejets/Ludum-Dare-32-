using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	private Health _playerHealth;

	public GameObject Player;
	public Texture2D CursorImage;
	public Texture2D CursorRedImage;

	private static LevelController _controller;

	public static LevelController Instance {
		get{
			string name = "LevelController";

			if (_controller == null) {

				if (GameObject.Find (name) != null) {
					_controller = GameObject.Find (name).GetComponent<LevelController>();
				} else {
					var obj = new GameObject ();
					obj.name = name;
					_controller = obj.AddComponent<LevelController> ();
				}
			} 

			return _controller;
		}
	}
		
	// Use this for initialization
	void Start () {
		_playerHealth = Player.GetComponentInChildren<Health> ();
		CursorMode cursorMode = CursorMode.Auto;
		Vector2 hotSpot = Vector2.zero;
		Cursor.SetCursor(CursorImage,hotSpot, cursorMode );
	}

	public void setMouseRed() {
		CursorMode cursorMode = CursorMode.Auto;
		Vector2 hotSpot = Vector2.zero;
		Cursor.SetCursor(CursorRedImage, hotSpot, cursorMode );
	}

	public void setMouseWhite() {
		CursorMode cursorMode = CursorMode.Auto;
		Vector2 hotSpot = Vector2.zero;
		Cursor.SetCursor(CursorImage, hotSpot, cursorMode );
	}
	
	// Update is called once per frame
	void Update () {
	
		if (_playerHealth.IsDead ()) {
			var reloadWhenDead = GetComponentsInChildren<ReloadWhenDead> ();

			ReloadWhenDead activeReload = null;

			foreach (var item in reloadWhenDead) {
				if (item.enabled) {
					activeReload = item;
				}
			}

			activeReload.ResetPlayer ();	
			activeReload.ReloadAliens ();
		}

	}
}
