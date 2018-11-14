using UnityEngine;
using System.Collections;

public class Boundaries : MonoBehaviour
{
	void OnTriggerExit (Collider other) 
	{
		Destroy(other.gameObject);
	}
}