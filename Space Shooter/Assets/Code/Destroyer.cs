using UnityEngine;

namespace SpaceShooter
{
	public class Destroyer : MonoBehaviour
	{
		void OnTriggerEnter2D(Collider2D other)
		{
            // Destroy the object regardless
			Destroy (other.gameObject);
		}
	}
}