using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Animator anim;
    Rigidbody rb;
    //Transform mouseHover;
    [SerializeField] float speed, rotVelocity;
    public mousseSelector tileSelectorScript;
    public Transform mouseHoverer;
    private Vector3 lastMousePos, lastPosition;
    private float rotSpeed;
    

    void Start()
    {
        anim.SetBool("isWalking", false);
        lastPosition = transform.position;
        //Cursor.visible = false;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        //transform.LookAt(new Vector3(0f,tileSelectorScript.TileSelected.y,0f));

        Vector3 targetPos = tileSelectorScript.TileSelected;
        Vector3 newPos = transform.position;
        Vector3 rotateZ = (targetPos - transform.position).normalized;

        newPos.z = Mathf.MoveTowards(transform.position.z, targetPos.z, speed * Time.deltaTime);
        newPos.x = Mathf.MoveTowards(transform.position.x, targetPos.x, speed * Time.deltaTime);

        Vector3 moveDirn = (transform.position - lastPosition).normalized;
        float targetAngle = Mathf.Atan2(moveDirn.x, moveDirn.z)*Mathf.Rad2Deg;
        float smoothRotAngle = 0f;
        smoothRotAngle = Mathf.SmoothDampAngle(smoothRotAngle,targetAngle, ref rotSpeed, rotVelocity*Time.deltaTime);
        //transform.Rotate(transform.rotation.x,moveDirn.y,transform.rotation.z);
        Quaternion targetRotation = Quaternion.Euler(0f, smoothRotAngle, 0f);

        transform.rotation = targetRotation;

        lastPosition = transform.position;



        if(!anim.GetBool("isWalking") && Input.GetKeyDown(KeyCode.F) && ((tileSelectorScript.TileSelected.z != transform.position.z) || (tileSelectorScript.TileSelected.x != transform.position.x)))
        {
            anim.SetBool("isWalking", true);
        }

        /*if (tileSelectorScript.TileSelected != transform.position && Input.GetMouseButton(0)) 
        {
            anim.SetBool("isWalking", true);
            transform.position = new Vector3(transform.position.x,transform.position.y,newPos.z);

            if(transform.position.z==targetPos.z) 
            {
                transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
            }
        } */

        if(anim.GetBool("isWalking"))
        {
            LockMouseCursor();
            /*if(Cursor.lockState == CursorLockMode.Locked)
            {
                Vector3 mouseDel = Input.mousePosition - lastMousePos;
                lastMousePos = Input.mousePosition;
            }*/
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
            

            if (transform.position.z == targetPos.z)
            {
                transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);

            }



        }
        
        if((tileSelectorScript.TileSelected.z == transform.position.z) && (tileSelectorScript.TileSelected.x == transform.position.x))

        {
            anim.SetBool("isWalking", false);
            UnlockMouseCursor();
        }
    }


    void LockMouseCursor()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        tileSelectorScript.enabled = false;
    }

    void UnlockMouseCursor()
    {
        mouseHoverer.position = tileSelectorScript.TileSelected;
        //Cursor.lockState = CursorLockMode.None;
        tileSelectorScript.enabled = true;
    }
}
