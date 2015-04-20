using UnityEngine;
using System.Collections;


public class IMyDamage : MonoBehaviour {
	public int Damage;
	public int ReflectedDamage = 100;

	public void Bounce ()
	{
		Damage = ReflectedDamage;
	}
}
