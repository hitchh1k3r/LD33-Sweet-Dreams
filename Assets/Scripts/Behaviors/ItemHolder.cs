using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

  [HeaderAttribute("Attachments")]
  public Transform bg;
  public DreamEnter dreamEnter;

  private static List<GameObject> originals;
  public static List<GameObject> held;

  private Vector3 newPos = new Vector3(0, 0, 0);

  private const int MAX_ITEMS = 7;

  void Start()
  {
    originals = new List<GameObject>();
    held = new List<GameObject>();
  }

  public void AddItem(GameObject orig, GameObject item, Quaternion rotation, float scaling, Vector3 menuOffset)
  {
    originals.Add(orig);
    GameObject newItem = (GameObject)Instantiate(item, Vector3.zero, rotation);
    newItem.transform.SetParent(transform);
    newItem.transform.localPosition = newPos + menuOffset;
    held.Add(newItem);
    newItem.transform.localScale = newItem.transform.localScale * scaling;
    if (held.Count > MAX_ITEMS)
    {
      for (int i = 1; i <= MAX_ITEMS; ++i)
      {
        held[i].transform.localPosition -= new Vector3(0, 0, 2);
      }
      Destroy(held[0]);
      held.RemoveAt(0);
      originals[0].SetActive(true);
      originals.RemoveAt(0);
    }
    else
    {
      newPos += new Vector3(0, 0, 2);
      transform.localPosition = transform.localPosition + new Vector3(-2, 0, 0);
    }
    bool state = false;
    foreach (GameObject holding in held.ToArray())
    {
      if (holding.name.StartsWith(Dream.currentDream))
      {
        state = true;
      }
    }
    dreamEnter.SetDreamReady(state);
  }

}