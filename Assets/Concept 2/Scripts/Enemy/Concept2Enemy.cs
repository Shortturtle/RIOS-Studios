using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Analytics;

namespace Concept2
{
    [RequireComponent (typeof(NavMeshAgent))]
    public class Concept2Enemy : BaseEnemyClass
    {
        public NavMeshAgent agent;
        public Vector3 exit;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        protected override void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            exit = GameObject.FindGameObjectWithTag("PlayerBase").transform.position;
            currentHealth = enemyStats.maxHealth;
            agent.speed = enemyStats.speed;
            agent.SetDestination(exit);
        }

        // Update is called once per frame
        protected override void Update()
        {
            if (agent.remainingDistance <= 1f)
            {
                Die();
            }
        }

        protected override void DistanceTracker()
        {
            percentageDistance = agent.remainingDistance;
        }
    }
}
