using UnityEngine;
using System.Collections;

public class LegSlot : MonoBehaviour
{
	public GameObject leg; // attach point for the leg game object
	
	public bool HasLeg()
	{
		return leg.activeSelf;
	}
}

