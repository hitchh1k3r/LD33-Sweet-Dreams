using UnityEngine;

public class DreamEnter : MonoBehaviour
{

  [HeaderAttribute("Attachments")]
  public Transform enterText;
  public PlayerController playerController;
  public ShadowController shadowController;

  private bool dreamReady;

  public void SetDreamReady(bool state)
  {
    dreamReady = state;
    enterText.gameObject.SetActive(dreamReady);
  }

  void OnPicked()
  {
    if (dreamReady)
    {
      playerController.gameObject.SetActive(false);
      shadowController.gameObject.SetActive(true);
      enterText.gameObject.SetActive(false);
      enabled = false;
    }
  }

}