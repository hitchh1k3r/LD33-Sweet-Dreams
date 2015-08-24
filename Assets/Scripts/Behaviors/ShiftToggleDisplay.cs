using UnityEngine;

public class ShiftToggleDisplay : MonoBehaviour
{

  public Sprite sprite_default;
  public Sprite sprite_shifted;

  private SpriteRenderer target;

  void Start()
  {
    target = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (Input.GetButton("Shift"))
    {
      target.sprite = sprite_shifted;
    }
    else
    {
      target.sprite = sprite_default;
    }
  }

}