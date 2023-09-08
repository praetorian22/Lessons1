using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private KeyCode up;
    //[SerializeField] private KeyCode down;
    //[SerializeField] private KeyCode right;
    //[SerializeField] private KeyCode left;
    [SerializeField] private float speed;

    private Vector2 movement;
    private Vector3 inputValueMovement;
    private Rigidbody2D RBplayer;
    private Controls inputControls;

    private WeaponScript weaponScript;
    private Vector2 borderLD;
    private Vector2 borderRU;

    private void Awake()
    {
        RBplayer = GetComponent<Rigidbody2D>();
        inputControls = new Controls();
        weaponScript = GetComponent<WeaponScript>();
    }

    private void OnEnable()
    {
        inputControls.Enable();
        inputControls.KeyBoard.Shot.performed += Shot;
    }

    private void Start()
    {
        borderLD = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        borderRU = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    }

    private void OnDisable()
    {
        inputControls.Disable();
        inputControls.KeyBoard.Shot.performed -= Shot;
    }

    void Rotation()
    {
        // ѕолучаем координаты мышки, переводим их в мировые координаты и мен€ем систему координат относительно игрока        
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Debug.Log(difference);        
        // ¬ычисл€ем угол между вектором diference и осью ’
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);
        // ѕолучаем вращение из углов от осей координат
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        // ¬ращаем игрока
        transform.rotation = rot;
    }

    private void Move()
    {
        inputValueMovement = inputControls.KeyBoard.Move.ReadValue<Vector2>();
        movement = Vector3.Lerp(movement, inputValueMovement, 0.01f);
        /*
        if (Input.GetKey(up))
        {
            if (movement.y < 1) movement.y += Time.deltaTime * speed;
            else movement.y = 1;
        }
        else
        {
            if (Input.GetKey(down))
            {
                if (movement.y > -1) movement.y -= Time.deltaTime * speed;
                else movement.y = -1;
            }
            else
            {
                if (movement.y > 0) movement.y -= Time.deltaTime * speed;
                else
                {
                    if (movement.y < 0) movement.y += Time.deltaTime * speed;
                }
                if (Mathf.Abs(movement.y) < 0.02f) movement.y = 0;
            }
        }
        
        if (Input.GetKey(right))
        {
            if (movement.x < 1) movement.x += Time.deltaTime * speed;
            else movement.x = 1;
        }
        else
        {
            if (Input.GetKey(left))
            {
                if (movement.x > -1) movement.x -= Time.deltaTime * speed;
                else movement.x = -1;
            }
            else
            {
                if (movement.x > 0) movement.x -= Time.deltaTime * speed;
                else
                {
                    if (movement.x < 0) movement.x += Time.deltaTime * speed;
                }
                if (Mathf.Abs(movement.x) < 0.02f) movement.x = 0;
            }
        }
        */
    }

    private void Shot(InputAction.CallbackContext callbackContext)
    {
        GameObject shot = weaponScript.Shot();
        if (shot != null) shot.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();
        Move();
    }

    private void FixedUpdate()
    {
        RBplayer.velocity = movement * speed;
        if (transform.position.x > borderRU.x) transform.position = new Vector3(borderRU.x, transform.position.y, transform.position.z);
        if (transform.position.x < borderLD.x) transform.position = new Vector3(borderLD.x, transform.position.y, transform.position.z);
        if (transform.position.y > borderRU.y) transform.position = new Vector3(transform.position.x, borderRU.y, transform.position.z);
        if (transform.position.y < borderLD.y) transform.position = new Vector3(transform.position.x, borderLD.y, transform.position.z);
        //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);
    }
}
