using System;
using Traffic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Editor
{
    public class WaypointManagerWindow : EditorWindow
    {
        Vector2 _scrollPos;

        [MenuItem("Tools/Waypoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        [SerializeField] private Transform _waypointRoot;

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

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
            EditorGUILayout.EndScrollView();

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

                GUILayout.Label("Road");
                if (GUILayout.Button("Create Road"))
                {
                    CreateRoad();
                }
                if (GUILayout.Button("Create Road Before"))
                {
                    CreateRoadBefore();
                }
                if (GUILayout.Button("Create Road After"))
                {
                    CreateRoadAfter();
                }
                if (GUILayout.Button("Remove Road"))
                {
                    RemoveRoad();
                }

                GUILayout.Label("System");
                if (GUILayout.Button("Make Crosswalk"))
                {
                    MakeCrosswalk();
                }
                if (GUILayout.Button("Make Stop"))
                {
                    MakeStop();
                }

                if (GUILayout.Button("Make Despawn"))
                {
                    MakeDespawnPoint();
                }

                if (GUILayout.Button("Remove Waypoint"))
                {
                    RemoveWaypoint();
                }

            }
        }

        private void RemoveRoad()
        {
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            Waypoint prevWaypoint = _waypointRoot.GetChild(selectedWaypoint.transform.GetSiblingIndex() - 1).GetComponent<Waypoint>();

            if (prevWaypoint != null && selectedWaypoint.NextWaypoint != null)
            {
                prevWaypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            }

            DestroyImmediate(selectedWaypoint.gameObject);
        }

        private void CreateRoadBefore()
        {
            GameObject waypointObject = new GameObject("Road Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            Waypoint prevWaypoint = _waypointRoot.GetChild(selectedWaypoint.transform.GetSiblingIndex() - 1).GetComponent<Waypoint>();

            waypoint.transform.position = selectedWaypoint.transform.position;
            waypoint.transform.forward = selectedWaypoint.transform.forward;
            waypoint.Width = selectedWaypoint.Width;

            prevWaypoint.NextWaypoint = waypoint;
            waypoint.NextWaypoint = selectedWaypoint;

            waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = waypoint.gameObject;
        }
        private void CreateRoadAfter()
        {
            GameObject waypointObject = new GameObject("Road Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();

            waypoint.NextWaypoint = selectedWaypoint.NextWaypoint;
            selectedWaypoint.NextWaypoint = waypoint;

            waypoint.transform.position = selectedWaypoint.transform.position;
            waypoint.transform.forward = selectedWaypoint.transform.forward;
            waypoint.Width = selectedWaypoint.Width;

            waypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex() + 1);

            Selection.activeGameObject = waypoint.gameObject;
        }

        private void CreateRoad()
        {
            GameObject waypointObject = new GameObject("Road Waypoint " + _waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(_waypointRoot, false);

            Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
            if (_waypointRoot.childCount > 1)
            {
                Waypoint prevWaypoint = _waypointRoot.GetChild(_waypointRoot.childCount - 2).GetComponent<Waypoint>();
                prevWaypoint.NextWaypoint = waypoint;

                waypoint.transform.position = prevWaypoint.transform.position;
                waypoint.transform.forward = prevWaypoint.transform.forward;
                waypoint.Width = prevWaypoint.Width;
            }

            Selection.activeGameObject = waypoint.gameObject;
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

            if (Selection.activeGameObject == null)
            {
                Debug.LogError("No GameObject selected.");
                return;
            }

            Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();

            if (branchedFrom.Branches == null)
            {
                Debug.LogError("Selected GameObject does not have a Waypoint component.");
                return;
            }

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

        void MakeDespawnPoint()
        {
            GameObject selectedObject = Selection.activeGameObject;
            Waypoint selectedWaypoint = selectedObject.GetComponent<Waypoint>();

            selectedObject.AddComponent<DespawnWaypoint>();
            DespawnWaypoint despawnWaypoint = selectedObject.GetComponent<DespawnWaypoint>();

            despawnWaypoint.Collider = selectedObject.GetComponent<Collider>();
            despawnWaypoint.Collider.isTrigger = true;

        }

    }
}