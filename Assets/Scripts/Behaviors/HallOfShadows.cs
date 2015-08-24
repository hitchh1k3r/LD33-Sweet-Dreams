using UnityEngine;
using System.Collections;

public class HallOfShadows : MonoBehaviour
{

  public Transform endWall;
  public GameObject tile;
  public float spacing = 22;

  void Start()
  {
    StartCoroutine(LoadShadows());
  }

  public IEnumerator LoadShadows()
  {
    WWW www = new WWW("http://api.hitchh1k3rsguide.com/LD33/get");
    yield return www;
    string[] data = www.data.Split('~');
    for (int i = 0; i < data.Length - 2; i += 3)
    {
      string name = data[i];
      string author = data[i + 1];
      string serial = data[i + 2];
      if (serial.GetHashCode() != ShadowSerializer.serialHash)
      {
        endWall.position += new Vector3(-spacing, 0, 0);
        GameObject newTile = (GameObject)Instantiate(tile, endWall.position, Quaternion.identity);
        Transform shadowText = newTile.transform.FindChild("about shadow");
        Transform shadowBox = newTile.transform.FindChild("toybox");
        ShadowSerializer.Deserialize(serial, shadowBox);
        TextMesh text = shadowText.GetComponent<TextMesh>();
        text.text = name + "\nby\n" + author;
      }
    }
    yield break;
  }

}