using UnityEngine;

public class PlayerController : MonoBehaviour
{

  [HeaderAttribute("Special")]
  public bool needsTransition;

  [HeaderAttribute("Attachments")]
  public Transform camera;
  public ItemHolder itemHolder;

  [HeaderAttribute("Control Attributes")]
  public float rotSpeed = 50.0f;
  public float moveSpeed = 20.0f;

  [HeaderAttribute("Object Picking")]
  public LayerMask selectionLayer;

  private Rigidbody rigidbody;
  private Vector3 velocity;
  private float lookAngle;

  void Start()
  {
    rigidbody = GetComponent<Rigidbody>();
    if (needsTransition)
    {
      StartCoroutine(Tween.EaseCoroutine(camera, Tween.PROP_ROTATION, Quaternion.Euler(lookAngle, 0, 0), 1.0f, Tween.EASE_CUBE, Tween.EASE_CUBE, GainControl));
    }
  }

  public void GainControl()
  {
    needsTransition = false;
  }

  void Update()
  {
    if (!needsTransition && Cursor.lockState == CursorLockMode.Locked)
    {
      transform.localRotation *= Quaternion.Euler(0, Input.GetAxis("Look X") * Time.deltaTime * rotSpeed, 0);
      lookAngle += Input.GetAxis("Look Y") * Time.deltaTime * rotSpeed;
      lookAngle = Mathf.Clamp(lookAngle, -80, 80);
      camera.localRotation = Quaternion.Euler(lookAngle, 0, 0);
      velocity = transform.forward * Input.GetAxis("Move Y");
      velocity += transform.right * Input.GetAxis("Move X");
      if (velocity.sqrMagnitude > 1.0f)
      {
        velocity.Normalize();
      }
      velocity *= moveSpeed;
      if (Input.GetButtonDown("Action"))
      {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 5.0f, selectionLayer))
        {
          hit.collider.SendMessageUpwards("OnPicked");
        }
      }
    }
    else
    {
      velocity = Vector3.zero;
    }
  }

  void FixedUpdate()
  {
    rigidbody.MovePosition(transform.position + velocity * Time.deltaTime);
    if (transform.position.y < -10)
    {
      transform.position = new Vector3(0, 5, 0);
    }
  }

}