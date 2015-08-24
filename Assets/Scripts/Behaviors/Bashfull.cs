using UnityEngine;

public class Bashfull : MonoBehaviour
{

  public Transform camera;
  public float range;
  public TextMesh property;
  public string text;

  void Update()
  {
    if ((camera.position - transform.position).sqrMagnitude < range * range)
    {
      property.text = text.Replace("<br />", "\n");
    }
    else
    {
      property.text = "";
    }
  }

}