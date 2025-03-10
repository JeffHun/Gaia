using System;
using Traffic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Editor
{
    public class WaypointManagerWindow : EditorWindow
    {
        [MenuItem("Tools/Waypoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        [SerializeField] private Transform _waypointRoot;

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);

            EditorGUILayout.PropertyField(obj.FindProperty("_waypointRoot"));

            if (_waypointRoot == null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                try
                {
                    DrawButtons();
                }
                finally
                {
                    EditorGUILayout.EndVertical();
                }
            }

            obj.ApplyModifiedProperties();
        }

        void DrawButtons()
        {
            if (GUILayout.Button("Create Waypoint"))
            {
                CreateWaypoint();
            }
            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                GUILayout.Label("Waypoint");
                if (GUILayout.Button("Add Branch Waypoint"))
                {
                    CreateBranch();
                }
                if (GUILayout.Button("Create Waypoint Before"))
                {
                    CreateWaypointBefore();
                }
                if (GUILayout.Button("Create Waypoint After"))
                {
                    CreateWaypointAfter();
                }

                if (GUILayout.Button("Make Crosswalk"))
                {
                    MakeCrosswalk();
                }
                if (GUILayout.Button("Make Stop"))
                {
                    MakeStop();
                }

                if (GUILayout.Button("Remove Waypoint"))
                {
                    RemoveWaypoint();
                }
            }
        }

        void CreateWaypoint()
        {
            GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
            if (_waypointRoot.childCount > 1)
            {
                waypoint.PreviousWaypoint = _waypointRoot.GetChild(_waypointRoot.childCount - 2).GetComponent<Waypoint>();
                waypoint.PreviousWaypoint.NextWaypoint = waypoint;

                waypoint.transform.position = waypoint.PreviousWaypoint.transform.position;
                waypoint.transform.forward = waypoint.PreviousWaypoint.transform.forward;
                waypoint.Width = waypoint.PreviousWaypoint.Width;
            }

            Selection.activeGameObject = waypoint.gameObject;
        }

        void CreateWaypointBefore()
        {
            GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;
            newWaypoint.Width = selectedWaypoint.Width;

            if (selectedWaypoint.PreviousWaypoint != null)
            {
                newWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
                selectedWaypoint.PreviousWaypoint.NextWaypoint = newWaypoint;
            }

            newWaypoint.NextWaypoint = selectedWaypoint;

            selectedWaypoint.PreviousWaypoint = newWaypoint;

            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = newWaypoint.gameObject;
        }

        void CreateWaypointAfter()
        {
            GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position = selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;
            newWaypoint.Width = selectedWaypoint.Width;

            newWaypoint.PreviousWaypoint = selectedWaypoint;

            if (selectedWaypoint.NextWaypoint != null)
            {
                newWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
                selectedWaypoint.NextWaypoint.PreviousWaypoint = newWaypoint;
            }

            selectedWaypoint.NextWaypoint = newWaypoint;

            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex() + 1);

            Selection.activeGameObject = newWaypoint.gameObject;
        }

        void RemoveWaypoint()
        {
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            if (selectedWaypoint.NextWaypoint != null)
            {
                selectedWaypoint.NextWaypoint.PreviousWaypoint = selectedWaypoint.PreviousWaypoint;
            }
            if (selectedWaypoint.PreviousWaypoint != null)
            {
                selectedWaypoint.PreviousWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
                Selection.activeGameObject = selectedWaypoint.PreviousWaypoint.gameObject;
            }

            DestroyImmediate(selectedWaypoint.gameObject);
        }

        void CreateBranch()
        {
            GameObject waypointObject = new GameObject("Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
            Debug.Log("New branch : " + waypoint.name);

            Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
            branchedFrom.Branches.Add(waypoint);

            waypoint.transform.position = branchedFrom.transform.position;
            waypoint.transform.forward = branchedFrom.transform.forward;

            Selection.activeGameObject = waypoint.gameObject;
        }

        void MakeCrosswalk()
        {
            GameObject selectedObject = Selection.activeGameObject;
            Waypoint selectedWaypoint = selectedObject.GetComponent<Waypoint>();

            selectedObject.AddComponent<CrosswalkWaypoint>();
            CrosswalkWaypoint crosswalkWaypoint = selectedObject.GetComponent<CrosswalkWaypoint>();

            crosswalkWaypoint.CrossingCollider = selectedObject.GetComponent<Collider>();
            crosswalkWaypoint.CrossingCollider.isTrigger = true;
            crosswalkWaypoint.Waypoint = selectedWaypoint;
        }

        void MakeStop()
        {
            GameObject selectedObject = Selection.activeGameObject;
            Waypoint selectedWaypoint = selectedObject.GetComponent<Waypoint>();

            selectedObject.AddComponent<StopWaypoint>();
            StopWaypoint stopWaypoint = selectedObject.GetComponent<StopWaypoint>();

            stopWaypoint.Waypoint = selectedWaypoint;

            GameObject colliderChild = new GameObject("Collider " + selectedObject.transform.childCount, typeof(BoxCollider));


            stopWaypoint.CheckAreas.Add(CreateStopGameObject("CanEnterCheck", selectedObject.transform, stopWaypoint.OnComingVehicle));
            stopWaypoint.CheckAreas.Add(CreateStopGameObject("HavePlaceToMove", selectedObject.transform, stopWaypoint.OnFreeSpace));

            //colliderChild = new GameObject("CanEnterCheck", typeof(BoxCollider));
            //colliderChild.transform.SetParent(selectedObject.transform, false);
            //colliderChild.GetComponent<BoxCollider>().isTrigger = true;
            //colliderChild.AddComponent<CollisionCallback>();
            //colliderChild = new GameObject("HavePlaceToMove", typeof(BoxCollider));
            //colliderChild.transform.SetParent(selectedObject.transform, false);
            //colliderChild.GetComponent<BoxCollider>().isTrigger = true;
            //colliderChild.AddComponent<CollisionCallback>();
            //stopWaypoint.CheckAreas.Add(colliderChild.GetComponent<BoxCollider>());
        }

         Collider CreateStopGameObject(string name, Transform parent, UnityAction actionCallback)
        {
            GameObject newGameObject = new GameObject(name, typeof(BoxCollider));
            newGameObject.transform.SetParent(parent);
            newGameObject.GetComponent<BoxCollider>().isTrigger = true;
            CollisionCallback script = newGameObject.AddComponent<CollisionCallback>();
            script.CollisionEvent.AddListener(actionCallback);

            return newGameObject.GetComponent<BoxCollider>();
        }

    }
}