using UnityEngine;

public class SoundEffects : MonoBehaviour
{

  [HeaderAttribute("Music")]
  public AudioSource music;

  [HeaderAttribute("Sounds")]
  public AudioSource pickup;

  private static AudioSource[] allSounds;

  void Start()
  {
    allSounds = new AudioSource[]
    {
      music,
      pickup
    };
  }

  public static void PlayPickup()
  {
    if (allSounds != null)
    {
      allSounds[1].Play();
    }
  }

}