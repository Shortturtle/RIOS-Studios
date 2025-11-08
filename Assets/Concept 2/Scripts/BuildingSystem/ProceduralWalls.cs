using System;
using System.Collections.Generic;
using UnityEngine;

namespace Concept2
{
    public class ProceduralWalls : MonoBehaviour
    {
        public GameObject wall;
        public Grid grid;

        private Vector3 currentPosition;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentPosition = transform.position;
        }

        private void Awake()
        {
            grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //List<Vector3> CheckNeighbours()
        //{
        //    List<Vector3> allNeighbours = new List<Vector3>();

        //    if (Physics.Raycast(currentPosition, Vector3.forward, 2f, out RaycastHit hit))
        //    {

        //    }
        //}
    }
}
