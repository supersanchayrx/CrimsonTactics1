using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    [SerializeField] float rayDistance, cursorDistance,height;
    public Vector3 TileSelected;
    public Light selectedTileLight;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = cursorDistance;
        //Vector3 simplifiedPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,cursorDistance));
        Vector3 simplifiedPos = cam.ScreenToWorldPoint(mousePos);

        simplifiedPos.y = height;

        transform.position = simplifiedPos;

        


        //raycasting downwards to detect the tile

        
        Vector3 rayDirection = -transform.up;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("Tiles")) 
            {
                Transform selectedTile = hit.collider.transform;
                Vector3 selectedTilePos = selectedTile.position;
                string selectedTileName = selectedTile.name;

                Debug.Log("Found "+selectedTileName+" at "+ selectedTilePos);

                TileSelected = selectedTilePos;
                selectedTileLight.transform.position = new Vector3(TileSelected.x,7f,TileSelected.z);
            }
        }

       
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }
}
