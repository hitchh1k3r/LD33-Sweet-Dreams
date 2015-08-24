using UnityEngine;

public class CameraLight : MonoBehaviour
{

  public GameObject renderLight;

  void OnPreCull()
  {
    renderLight.SetActive(true);
  }

  void OnPostRender()
  {
    renderLight.SetActive(false);
  }

}