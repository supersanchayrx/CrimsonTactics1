using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileInfo : MonoBehaviour
{
    public playerMovement playerMovementScript;
    public mousseSelector selectorScript;
    Label current, selected;

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        current = root.Q<Label>("currTilePos");
        selected = root.Q<Label>("selectedTilePos");


    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current.text = playerMovementScript.currentTileInfoString;
        selected.text = selectorScript.selectedTileInfoString;
    }
}
