using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObstacleManager : EditorWindow
{
    string objectName;
    GameObject obstacle;

    [MenuItem("Tools/Obstacle Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ObstacleManager));
    }

    private void OnGUI()
    {
        GUILayout.Label("Obstacle Placer", EditorStyles.boldLabel);

        objectName = EditorGUILayout.TextField("Name", objectName);
        obstacle = EditorGUILayout.ObjectField("Put in Obstacle", obstacle, typeof(GameObject),false) as GameObject;

        if (GUILayout.Button("Put in obstacles"))
        {
            SpawnObjects();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects()
    {
        if(obstacle != null) 
        {

        }
    }
}
