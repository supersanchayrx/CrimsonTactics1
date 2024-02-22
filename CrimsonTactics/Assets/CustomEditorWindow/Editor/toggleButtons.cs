using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToggleButtons : EditorWindow
{
    private bool[,] buttonStates = new bool[10, 10];
    private static GameObject[,] instantiatedTrees = new GameObject[10, 10]; // Keep track of instantiated trees
    private static GameObject treePrefab; // Make it static for shared reference across all instances

    [MenuItem("Tools/Toggleable Buttons Editor")]
    public static void ShowWindow()
    {
        ToggleButtons window = EditorWindow.GetWindow<ToggleButtons>();
        window.titleContent = new GUIContent("Toggleable Buttons Editor");
        window.Show();
    }

    private void OnGUI()
    {
        if (buttonStates == null || buttonStates.Length == 0)
        {
            buttonStates = new bool[10, 10];
        }

        EditorGUILayout.Space();

        // Allow dragging and dropping of tree prefab
        DragAndDropObjectField();

        for (int i = 0; i < 10; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < 10; j++)
            {
                bool previousState = buttonStates[i, j];
                buttonStates[i, j] = GUILayout.Toggle(buttonStates[i, j], "");

                // Check if the button state has changed
                if (previousState != buttonStates[i, j])
                {
                    HandleToggleChange(i, j);
                }
            }

            EditorGUILayout.EndHorizontal();
        }
    }

    private void HandleToggleChange(int row, int col)
    {
        if (buttonStates[row, col])
        {
            // Button is now toggled on
            InstantiateTreeAtCube(row, col);
        }
        else
        {
            // Button is now toggled off
            DestroyTreeAtCube(row, col);
        }
    }

    private void InstantiateTreeAtCube(int row, int col)
    {
        if (treePrefab != null)
        {
            // Check if a tree is already instantiated at this cube
            if (instantiatedTrees[row, col] == null)
            {
                // Assuming you have a CubeToggleScript attached to each cube
                GameObject cube = GameObject.Find("Cube" + (row+1) + "_" + (col+1));

                if (cube != null)
                {
                    CubeToggleScript cubeScript = cube.GetComponent<CubeToggleScript>();

                    if (cubeScript != null)
                    {
                        // Instantiate the tree prefab at the cube's position
                        instantiatedTrees[row, col] = Instantiate(treePrefab, new Vector3(cube.transform.position.x,2f, cube.transform.position.z), Quaternion.identity);

                        // Mark the cube as toggled
                        cubeScript.isToggled = true;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Tree Prefab is not assigned. Please drag and drop a tree prefab.");
        }
    }

    private void DestroyTreeAtCube(int row, int col)
    {
        // Check if a tree is instantiated at this cube
        if (instantiatedTrees[row, col] != null)
        {
            // Destroy the instantiated tree
            DestroyImmediate(instantiatedTrees[row, col]);
            instantiatedTrees[row, col] = null;
        }
    }

    private void DragAndDropObjectField()
    {
        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0f, 50f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag & Drop Tree Prefab Here");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    break;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is GameObject)
                        {
                            treePrefab = (GameObject)draggedObject;
                            Debug.Log("Tree Prefab assigned: " + treePrefab.name);
                        }
                    }
                }
                Event.current.Use();
                break;
        }
    }
}
