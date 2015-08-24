using UnityEngine;

public class GUIInput : MonoBehaviour
{

  private string shadowsName = "Mysterious Shadow";
  private string yourName = "The Bogeyman";
  private ShadowController shadowContoller;

  private const int HALL_OF_SHADOWS = 1;
  private bool finished;

  void OnGUI()
  {
    if (!finished)
    {
      Rect moveRect = new Rect((Screen.width - 300) - 25, Screen.height * 0.5f, 300, 30);
      GUI.Label(moveRect, "Shadow's Name:");
      moveRect.Set(moveRect.left, moveRect.top + 30, moveRect.width, moveRect.height);
      shadowsName = GUI.TextField(moveRect, shadowsName, 28);
      moveRect.Set(moveRect.left, moveRect.top + 45, moveRect.width, moveRect.height);
      GUI.Label(moveRect, "Your Name:");
      moveRect.Set(moveRect.left, moveRect.top + 30, moveRect.width, moveRect.height);
      yourName = GUI.TextField(moveRect, yourName, 28);
      moveRect.Set(moveRect.left, moveRect.top + 45, moveRect.width, moveRect.height);
      if (GUI.Button(moveRect, "Submit to Hall of Shadows"))
      {
        ShadowSerializer.SerializeMyShadow(shadowContoller.toyContainer);
        StartCoroutine(ShadowSerializer.Submit(shadowsName, yourName, HALL_OF_SHADOWS));
        finished = true;
      }
      moveRect.Set(moveRect.left, moveRect.top + 45, moveRect.width, moveRect.height);
      if (GUI.Button(moveRect, "Don't Submit (But Finish)"))
      {
        ShadowSerializer.SerializeMyShadow(shadowContoller.toyContainer);
        Application.LoadLevel(HALL_OF_SHADOWS);
      }
      moveRect.Set(moveRect.left, moveRect.top + 45, moveRect.width, moveRect.height);
      if (GUI.Button(moveRect, "Continue Working on Shadow"))
      {
        shadowContoller.CancelZoom();
      }
    }
  }

  public void SetShadowController(ShadowController controller)
  {
    shadowContoller = controller;
  }

}