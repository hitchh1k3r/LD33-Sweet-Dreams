using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Dream : MonoBehaviour
{

  public Sprite[] sprites;

  public static string currentDream;

  private SpriteRenderer spriteRenderer;

  void Start()
  {
    spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
    currentDream = spriteRenderer.sprite.name;
  }

}