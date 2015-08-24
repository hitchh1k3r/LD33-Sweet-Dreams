using UnityEngine;

public class PortalToRoom : MonoBehaviour
{

  void OnCollisionEnter(Collision collision)
  {
    Application.Quit();
  }

}