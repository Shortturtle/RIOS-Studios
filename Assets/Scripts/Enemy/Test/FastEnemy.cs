using UnityEngine;

public class FastEnemy : BaseEnemyClass
{

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public override void Die()
    {
        base.Die();
    }
}
