using UnityEngine;

public class Billboard : MonoBehaviour
{

  public Camera trackCamera;

  void Start()
  {
    if (trackCamera == null)
    {
      trackCamera = Camera.main;
    }
  }

  void Update()
  {
    Vector3 rot = trackCamera.transform.rotation.eulerAngles;
    rot.x = 0;
    transform.rotation = Quaternion.Euler(rot);
  }

  void OnDrawGizmosSelected()
  {
    Gizmos.DrawSphere(transform.position, 0.25f);
  }

}