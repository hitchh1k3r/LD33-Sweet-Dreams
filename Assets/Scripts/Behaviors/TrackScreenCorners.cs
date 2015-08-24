using UnityEngine;

[RequireComponent(typeof(Camera))]
public class TrackScreenCorners : MonoBehaviour
{

  [HeaderAttribute("Corner Anchors")]
  public Transform bottom_left;
  public Transform bottom_right;

  private Camera camera;

  void Start()
  {
    camera = GetComponent<Camera>();
  }

  void Update()
  {
    bottom_left.position = camera.ScreenToWorldPoint(new Vector3(0, 0, 5));
    bottom_right.position = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 5));
  }

}