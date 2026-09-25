using UnityEngine;

public class LightProjectile : MonoBehaviour
{
   [SerializeField] private float speed = 20f;
   [SerializeField] private float lifetime = 3f;

   private void Start()
   {
      Destroy(gameObject, lifetime);
   }
   private void OnTriggerEnter(Collider other)
   {
      GhostEnemy ghost = other.GetComponentInParent<GhostEnemy>();
      if (ghost != null)
      {
         ghost.Hit();
         Destroy(gameObject);
        // Destroy(ghost.gameObject);
      }
   }
}
