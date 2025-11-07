using System.Collections;
using UnityEngine;

public class Removal : MonoBehaviour
{
    private int timesTapped;
    private bool isThrown = false;
    [SerializeField] private float grav;
    private float initialJumpTime;
    private float initialJumpSpeed;
    //public float iJFMin;
    //public float iJFMax;
    //public float rSpeedMin;
    //public float rSpeedMax;
    //public float iJTMin;
    //public float iJTMax;
    //public float mDMin;
    //public float mDMax;
    //public float iJSMin;
    //public float iJSMax;
    //public float decaySpeed;
    private float decaySpeed = 50f;
    private float initialJumpForce;
    private bool isJumping;
    private float moveDir;
    private float randomSpeed;
    private ConstantForce cForce;
    private Vector3 forceDirection = Vector3.zero;

    public GameObject objectBelow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cForce = GetComponent<ConstantForce>();
        cForce.force = forceDirection;
    }

    // Update is called once per frame
    void Update()
    {
        JumpCheck();
        Jump();
    }

    //when tapped by player
    public void Tapped()
    {
        timesTapped++;
        if(timesTapped == 5)
        {
            if (isThrown == false)
            {
                Throw();
                isThrown = true;
                objectBelow.GetComponent<DragDrop>().isBlocked = false;
                Destroy(gameObject, 4f);
            }
        }
    }

    private void Throw()
    {
        Debug.Log("times thrown");
        initialJumpForce = Random.Range(4f, 6f);
        initialJumpTime = Random.Range(0.2f, 0.6f);
        initialJumpSpeed = Random.Range(100f, 130f);
        moveDir = Random.Range(-7, 7);
        randomSpeed = Random.Range(25, 75);
        isJumping = true;
    }
    //For jump
    private void Jump()
    {
        if (isJumping)
        {
            forceDirection = new Vector3(moveDir * randomSpeed, initialJumpForce * initialJumpSpeed, 0);
            cForce.force = forceDirection;
        }
    }
    private void JumpCheck()
    {
        if (isJumping == true)
        {
            initialJumpTime -= Time.deltaTime;
            if(initialJumpSpeed >= 0)
            {
                initialJumpSpeed -= Time.deltaTime * decaySpeed;
            }
            
        }
        if (initialJumpTime <= 0 && timesTapped >= 5)
        {
            isJumping = false;
            forceDirection = new Vector3(0, grav, 0);
            cForce.force = forceDirection;
        }
    }
}
