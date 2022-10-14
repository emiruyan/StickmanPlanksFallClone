using UnityEngine;

public class FinishFloorController : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag("Player"))
      {
         GameManager.Instance.NextLevel();
      }
   }
}
