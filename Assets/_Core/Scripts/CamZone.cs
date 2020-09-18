using Cinemachine;
using UnityEngine;

// Attach to an empty GameObject to create a zone which,
//when entered by a player, shifts to another cinemachine virtual camera

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{

  [SerializeField]
  private CinemachineVirtualCamera virtualCamera = null;

  void Start ()
  {
    virtualCamera.enabled = false;
  }

  void OnTriggerEnter (Collider other)
  {
    if (other.CompareTag("Player"))
      virtualCamera.enabled = true;
  }

  void OnTriggerExit (Collider other)
  {
    if (other.CompareTag("Player"))
      virtualCamera.enabled = false;
  }

  // This ensures that the collider attached to this GameObject has IsTrigger checked
  private void OnValidate ()
  {
    GetComponent<Collider>().isTrigger = true;
  }
}