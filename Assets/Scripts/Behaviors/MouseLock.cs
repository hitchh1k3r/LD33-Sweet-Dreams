using UnityEngine;

public class MouseLock : MonoBehaviour
{

  public static bool lockActive;

  void Start()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    lockActive = true;
  }

  void Update()
  {
    if (lockActive)
    {
      if (Input.GetButtonDown("Action"))
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      if (Input.GetButtonDown("Menu"))
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
    }
  }

}