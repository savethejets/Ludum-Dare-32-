using UnityEngine;
using System.Collections;
using HUD;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IHUDObserver {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region IHUDObserver implementation

	public void UpdateFloat (float oldValue, float newValue)
	{
		Debug.Log (newValue);
		transform.FindChild("Slider").GetComponent<Slider> ().value = newValue / 100;
	}

	public void UpdateBool (bool oldValue, bool newValue)
	{

	}

	public void UpdateString (string oldString, string newString) 
	{

	}

	#endregion
}
