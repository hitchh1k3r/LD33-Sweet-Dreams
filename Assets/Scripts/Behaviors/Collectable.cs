using UnityEngine;

public class Collectable : MonoBehaviour
{

  [HeaderAttribute("Attachments")]
  public Transform gameCamera;
  public ItemHolder itemHolder;

  [HeaderAttribute("Menu GFx")]
  public Vector3 menuRotation;
  public GameObject prefab;
  public float scaling = 2.0f;
  public Vector3 menuOffset;

  void OnPicked()
  {
    SoundEffects.PlayPickup();
    itemHolder.AddItem(gameObject, prefab, Quaternion.Euler(menuRotation), scaling, menuOffset);
    gameObject.SetActive(false);
  }

}