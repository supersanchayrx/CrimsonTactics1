using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousseSelector : MonoBehaviour
{

    public LayerMask grid;
    public Material hightlightMat, mat1;
    private RaycastHit hitInfo;
    private Transform currentTile;
    public Vector3 TileSelected;
    public float rayDistance;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, grid))
        {

            Transform tile = hitInfo.collider.transform;

            if (tile != currentTile)
            {
                mat1 = tile.GetComponent<Renderer>().material;
                if (currentTile != null)
                {
                    Removehightlight(currentTile);
                }
            }
            currentTile = tile;
            TileSelected = tile.position;
            HighlightTile(tile);

            /*if(currentTile==tile) 
            {
                Removehightlight(currentTile);
            }*/
        }

        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
    }

    void HighlightTile(Transform tile)
    {
        Renderer renderer = tile.GetComponent<Renderer>();
        //mat1 = renderer.material;
        if (renderer != null)
        {
            renderer.material = hightlightMat;
        }
    }

    void Removehightlight(Transform tile)
    {
        Renderer renderer = tile.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = mat1;
        }
    }
}