using UnityEngine;

public class ShadowController : MonoBehaviour
{

  [HeaderAttribute("Attachments")]
  public GameObject toggler;
  public Transform selector;
  public Transform itemSpawner;
  public GameObject shadowControls;
  public Transform camera;
  public Transform camWideTarget;
  public Transform camZoomTarget;
  public Transform toyContainer;
  public GUIInput guiInput;

  [HeaderAttribute("Control Attributes")]
  public float rotSpeed = 1.5f;
  public float shiftSpeed = 1.5f;
  public float flySpeedFraction = 0.25f;

  [HeaderAttribute("Puppet Zone")]
  public Collider[] movementZones;

  private int puppetIndex;
  private Transform[] puppets;
  private bool inactive;

  void OnEnable()
  {
    toggler.SetActive(false);
    selector.gameObject.SetActive(true);
    shadowControls.SetActive(true);
    puppets = new Transform[ItemHolder.held.Count];
    foreach (GameObject puppet in ItemHolder.held)
    {
      GameObject newObject = (GameObject)Instantiate(puppet, itemSpawner.position + Random.insideUnitSphere * 2.0f, Quaternion.identity);
      newObject.transform.localScale = Vector3.one;
      puppets[puppetIndex] = newObject.transform;
      newObject.transform.SetParent(toyContainer);
      ++puppetIndex;
    }
    --puppetIndex;
  }

  void Update()
  {
    if (!inactive)
    {
      if (Cursor.lockState == CursorLockMode.Locked)
      {
        puppets[puppetIndex].rotation = Quaternion.Euler(Input.GetAxis("Look Y") * rotSpeed, -Input.GetAxis("Look X") * rotSpeed, Input.GetAxis("Roll") * rotSpeed) * puppets[puppetIndex].rotation;
        Vector3 movement = new Vector3(-Input.GetAxis("Move X"), (Input.GetAxis("Jump") - Input.GetAxis("Shift")) * flySpeedFraction, -Input.GetAxis("Move Y")) * Time.deltaTime * shiftSpeed;
        RaycastHit hit;
        foreach (Collider collider in movementZones)
        {
          if (movement.sqrMagnitude > 0)
          {
            if (collider.Raycast(new Ray(puppets[puppetIndex].position + movement, -movement), out hit, movement.magnitude))
            {
              movement = puppets[puppetIndex].position - hit.point;
            }
          }
        }
        puppets[puppetIndex].position += movement;

        if (Input.GetButtonDown("Action"))
        {
          shadowControls.SetActive(!shadowControls.active);
        }

        if (Input.GetButtonDown("Finish"))
        {
          inactive = true;
          MouseLock.lockActive = false;
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          shadowControls.SetActive(false);
          StartCoroutine(Tween.EaseCoroutine(camera, Tween.PROP_POSITION, camZoomTarget, 1.0f, Tween.EASE_CUBE, Tween.EASE_CUBE));
          StartCoroutine(Tween.EaseCoroutine(camera, Tween.PROP_ROTATION, camZoomTarget, 1.0f, Tween.EASE_CUBE, Tween.EASE_CUBE, HideZoom));
        }

        if (Input.GetAxisRaw("ObjectSelect") < 0)
        {
          puppetIndex = (puppetIndex - 1 + puppets.Length) % puppets.Length;
        }
        else if (Input.GetAxisRaw("ObjectSelect") > 0)
        {
          puppetIndex = (puppetIndex + 1) % puppets.Length;
        }
        selector.localPosition = new Vector3(-4.9f, -2, 2 * puppetIndex);
      }
    }
  }

  public void HideZoom()
  {
    guiInput.enabled = true;
    guiInput.SetShadowController(this);
    foreach (Renderer renderer in toyContainer.GetComponentsInChildren<Renderer>())
    {
      renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
    }
  }

  public void CancelZoom()
  {
    guiInput.enabled = false;
    inactive = false;
    MouseLock.lockActive = true;
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
    StartCoroutine(Tween.EaseCoroutine(camera, Tween.PROP_POSITION, camWideTarget, 1.0f, Tween.EASE_CUBE, Tween.EASE_CUBE));
    StartCoroutine(Tween.EaseCoroutine(camera, Tween.PROP_ROTATION, camWideTarget, 1.0f, Tween.EASE_CUBE, Tween.EASE_CUBE));
    shadowControls.SetActive(true);
    foreach (Renderer renderer in toyContainer.GetComponentsInChildren<Renderer>())
    {
      renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
  }

}