using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Users;
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
        public LayerMask wallLayerMask;
        private GameObject ghostObject;
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

            if (Input.GetKey("1"))
            {
                state = BuildingStates.None;
                UpdateObjectToPlace();

            }

            if (Input.GetKey("2"))
            {
                state = BuildingStates.Walls;
                UpdateObjectToPlace();

            }

            if (Input.GetKey("3"))
            {
                state = BuildingStates.Towers;
                UpdateObjectToPlace();

            }
        }

        void CreateGhostObject()
        {
            ghostObject = Instantiate(objectToPlace, new Vector3(99,99,99), Quaternion.identity);
            if (ghostObject.GetComponent<Collider>() != null)
            {
                ghostObject.GetComponent<Collider>().enabled = false;
            }

            if( ghostObject.transform.GetChild(0).GetComponent<OffenseTowerBase>() != null)
            {
                Destroy(ghostObject.transform.GetChild(0).GetComponent<OffenseTowerBase>());
            }

            else
            {
                ghostObject.transform.GetChild(0).gameObject.GetComponent<Collider>().enabled = false;
            }

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
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);

            RaycastCheck(ray);
        }

        void UpdateGhostObject()
        {
            ghostObject.transform.position = hoveredPosition;

            if (occupiedPositions.Contains(hoveredPosition) || occupiedTowerPositions.Contains(hoveredPosition))
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
            switch (state)
            {
                case BuildingStates.Walls:
                    WallPlacement();
                    break;

                case BuildingStates.Towers:
                    TowerPlacement();
                    break;
            }
        }

        void WallPlacement()
        {
            Vector3 placementPosition = ghostObject.transform.position;

            if (!occupiedPositions.Contains(placementPosition))
            {
                Instantiate(objectToPlace, placementPosition, Quaternion.identity);

                occupiedPositions.Add(placementPosition);
            }
        }

        void RaycastCheck(Ray ray)
        {
            switch (state)
            {
                case BuildingStates.Walls:

                    if (Physics.Raycast(ray, out RaycastHit hit, 100, groundLayerMask))
                    {
                        Vector3 point = hit.point;

                        Vector3Int snappedPosition = grid.WorldToCell(point);

                        Vector3 fixedPosition = grid.GetCellCenterWorld(snappedPosition);

                        hoveredPosition = new Vector3(fixedPosition.x, point.y, fixedPosition.z);
                    }

                    else
                    {
                        hoveredPosition = new Vector3(999, 999, 999);
                    }

                        break;

                case BuildingStates.Towers:

                    if (Physics.Raycast(ray, out RaycastHit wall, 100, wallLayerMask))
                    {
                        GameObject wallObject = wall.collider.gameObject;

                        Vector3 point = wall.point;

                        Vector3Int snappedPosition = grid.WorldToCell(point);

                        Vector3 fixedPosition = grid.GetCellCenterWorld(snappedPosition);

                        hoveredPosition = new Vector3(fixedPosition.x, wall.transform.localScale.y, fixedPosition.z);
                    }
                    else
                    {
                        hoveredPosition = new Vector3(999, 999, 999);
                    }


                    break;
            }
        }

        void TowerPlacement()
        {
            Vector3 placementPosition = ghostObject.transform.position;

            if (!occupiedTowerPositions.Contains(placementPosition))
            {
                Instantiate(objectToPlace, placementPosition, Quaternion.identity);

                occupiedTowerPositions.Add(placementPosition);
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
