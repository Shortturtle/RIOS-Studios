using System;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawner : MonoBehaviour
{
    [Serializable]
    public class SpawnPosition
    {
        public List<Vector2> icePositions = new List<Vector2>();
    }

    public IceMGManager iceMGManager;
    public List<SpawnPosition> possibleSpawns;
    public GameObject Ice;
    private SpawnPosition selected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selected = possibleSpawns[UnityEngine.Random.Range(0, possibleSpawns.Count - 1)];
        iceMGManager.numberOfIceToMelt = selected.icePositions.Count;
        SpawnIce();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnIce()
    {
        foreach( var position in selected.icePositions)
        {
            var tempIce = Instantiate(Ice, this.gameObject.transform);
            tempIce.GetComponent<RectTransform>().localPosition = position;
        }
    }
}
