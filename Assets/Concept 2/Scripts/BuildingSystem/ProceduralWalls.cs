using System;
using System.Collections.Generic;
using UnityEngine;

namespace Concept2
{
    public class ProceduralWalls : MonoBehaviour
    {
        public GameObject wall;
        public LayerMask wallLayerMask;
        public Grid grid;

        private List<Vector3> allDirections = new List<Vector3>
        {
            Vector3.forward, Vector3.back, Vector3.left, Vector3.right
        };

        private Vector3 currentPosition;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentPosition = transform.position;
            CheckNeighbors();
        }

        private void Awake()
        {
            grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckNeighbors()
        {
            List<Vector3> allNeighbors = new List<Vector3>();

           foreach (var direction in allDirections)
            {
                if(Physics.Raycast(currentPosition, direction, out RaycastHit hit, grid.cellSize.x, wallLayerMask))
                {
                    allNeighbors.Add(grid.GetCellCenterWorld(grid.WorldToCell(hit.point)));
                }
            }

           foreach (var neighbor in allNeighbors)
            {
                AddWall(currentPosition, neighbor);
            }
        }

        void AddWall(Vector3 originalPost, Vector3 newPost)
        {
            Instantiate(wall,  ((originalPost + newPost)/2) , newPost.z - originalPost.z != 0 ? Quaternion.Euler(0,90,0) : Quaternion.identity);
        }
    }
}
