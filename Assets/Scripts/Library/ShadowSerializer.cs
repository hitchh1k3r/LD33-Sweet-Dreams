using UnityEngine;
using System.Collections;

public class ShadowSerializer: MonoBehaviour
{

  public GameObject[] prefabs;
  public Transform shadowbox;

  private static ShadowSerializer instance;

  public static int serialHash;
  private static string MY_SERIALIZE_DATA = "";

  void Start()
  {
    instance = this;
    if (MY_SERIALIZE_DATA != "")
    {
      Deserialize(MY_SERIALIZE_DATA, shadowbox);
      MY_SERIALIZE_DATA = "";
    }
  }

  public static void SerializeMyShadow(Transform shadowBox)
  {
    MY_SERIALIZE_DATA = "";
    foreach (Transform caster in shadowBox)
    {
      MY_SERIALIZE_DATA += caster.name.Substring(0, caster.name.IndexOf('(')) + "|" + caster.transform.localPosition.x + ":" + caster.transform.localPosition.y + ":" + caster.transform.localPosition.z + "|" + caster.transform.localRotation.eulerAngles.x + ":" + caster.transform.localRotation.eulerAngles.y + ":" + caster.transform.localRotation.eulerAngles.z + "|";
    }
    MY_SERIALIZE_DATA = MY_SERIALIZE_DATA.TrimEnd('|');
    serialHash = MY_SERIALIZE_DATA.GetHashCode();
  }

  public static void Deserialize(string data, Transform shadowBox)
  {
    string[] parts = data.Split('|');
    for (int i = 0; i < parts.Length - 2; i += 3)
    {
      GameObject obj = ParsePrefab(parts[i]);
      Vector3 pos = ParseVector(parts[i + 1]);
      Vector3 rot = ParseVector(parts[i + 2]);
      if (obj != null)
      {
        GameObject newObj = (GameObject)Instantiate(obj, Vector3.zero, Quaternion.identity);
        newObj.transform.SetParent(shadowBox);
        newObj.transform.localPosition = pos;
        newObj.transform.localRotation = Quaternion.Euler(rot);
      }
    }
  }

  private static GameObject ParsePrefab(string name)
  {
    foreach (GameObject obj in instance.prefabs)
    {
      if (obj.name == name)
      {
        return obj;
      }
    }
    return null;
  }

  private static Vector3 ParseVector(string data)
  {
    string[] bits = data.Split(':');
    if (bits.Length == 3)
    {
      float x;
      if (float.TryParse(bits[0], out x))
      {
        float y;
        if (float.TryParse(bits[1], out y))
        {
          float z;
          if (float.TryParse(bits[2], out z))
          {
            return new Vector3(x, y, z);
          }
        }
      }
    }
    return Vector3.zero;
  }

  public static IEnumerator Submit(string shadowsName, string authorsName, int finishLevel)
  {
    WWWForm data = new WWWForm();
    data.AddField("NAME", shadowsName);
    data.AddField("AUTHOR", authorsName);
    data.AddField("DATA", MY_SERIALIZE_DATA);
    WWW www = new WWW("http://api.hitchh1k3rsguide.com/LD33/submit", data);
    yield return www;
    Application.LoadLevel(finishLevel);
    yield break;
  }

}