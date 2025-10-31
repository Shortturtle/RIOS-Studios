using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OffenseTowerbase : BaseTowerClass
{
    // Range variables
    protected float rangeValue;
    protected SphereCollider rangeSphere;

    // Target variables
    protected List<GameObject> targets = new List<GameObject>();
    protected GameObject currentTarget;

    protected enum TargettingModes
    {
        First, Last, Close, Strong
    }
    protected TargettingModes targettingMode;
    protected int targetModeNum = 0;

    protected enum ChangeDirection
    {
        Next, Prev
    }

    // Damage variables
    protected float damageValue;
    protected float timeBetweenAttacks;

    // Projectile variables
    protected GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemy>() != null)
        {
            targets.Add(other.gameObject);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.gameObject))
        {
            targets.Remove(other.gameObject);
        }
    }

    protected void ChangeTargettingMode(ChangeDirection direction)
    {
        targetModeNum = direction == ChangeDirection.Next ? targetModeNum++ : targetModeNum--;

        if (targetModeNum < 0)
        {
            
        }
    }

    protected void GetTargetEnemy()
    {

    }
}
