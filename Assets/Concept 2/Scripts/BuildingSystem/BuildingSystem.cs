using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Concept2
{
    public class BuildingSystem : MonoBehaviour
    {
        public Grid grid;
        public GameObject objectToPlace;
        public GameObject wall;
        public GameObject tower;
        public LayerMask groundLayerMask;
        private GameObject ghostObject;
        private HashSet<Vector3> occupiedWallPositions = new HashSet<Vector3>();
        private HashSet<Vector3> occupiedTowerPositions = new HashSet<Vector3>();
        private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();

        private enum BuildingStates
        {
            None, Walls, Towers
        }
        private BuildingStates state = BuildingStates.None;

        private Vector3 hoveredPosition;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (grid == null)
            {
                grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
            }

            CreateGhostObject();
        }

        // Update is called once per frame
        void Update()
        {
            if (state != BuildingStates.None)
            {
                UpdatePosition();
                UpdateGhostObject();

                if (Input.GetMouseButtonDown(0))
                {
                    PlaceObject();
                }
            }

            if (Input.GetButtonDown("1"))
            {
                state = BuildingStates.None;
                UpdateObjectToPlace();

            }

            if (Input.GetButtonDown("2"))
            {
                state = BuildingStates.Walls;
                UpdateObjectToPlace();

            }

            if (Input.GetButtonDown("3"))
            {
                state = BuildingStates.Towers;
                UpdateObjectToPlace();

            }
        }

        void CreateGhostObject()
        {
            ghostObject = Instantiate(objectToPlace);
            ghostObject.GetComponent<Collider>().enabled = false;

            Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                Material mat = r.material;
                Color color = mat.color;

                mat.SetFloat("_Mode", 2);
                mat.SetInt("_ScrBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }

        void UpdatePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

            if(Physics.Raycast(ray, out RaycastHit hit, 100, groundLayerMask))
            {
                Vector3 point = hit.point;

                Vector3Int snappedPosition = grid.WorldToCell(point);

                hoveredPosition = grid.GetCellCenterWorld(snappedPosition);

                Debug.Log(point);
            }
        }

        void UpdateGhostObject()
        {
            ghostObject.transform.position = hoveredPosition;

            if (occupiedPositions.Contains(hoveredPosition))
            {
                SetGhostColor(Color.red);
            }

            else
            {
                SetGhostColor(Color.green);
            }
        }

        void SetGhostColor(Color color)
        {
            Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                Material mat = r.material;
                mat.color = color;
            }
        }

        void PlaceObject()
        {
            Vector3 placementPosition = ghostObject.transform.position;

            if (!occupiedPositions.Contains(placementPosition))
            {
                Instantiate(objectToPlace, placementPosition, Quaternion.identity);

                occupiedPositions.Add(placementPosition);
            }
        }

        void UpdateObjectToPlace()
        {
            switch (state)
            {
                case BuildingStates.None:
                    objectToPlace = null;
                    if (ghostObject != null)
                    {
                        Destroy(ghostObject);
                    }
                    break;

                case BuildingStates.Walls:
                    objectToPlace = wall;
                    if (ghostObject != null)
                    {
                        Destroy(ghostObject);
                    }
                    CreateGhostObject();

                    break;

                case BuildingStates.Towers:
                    objectToPlace = tower;
                    if (ghostObject != null)
                    {
                        Destroy(ghostObject);
                    }
                    CreateGhostObject();
                    break;
            }

        }
    }
}
