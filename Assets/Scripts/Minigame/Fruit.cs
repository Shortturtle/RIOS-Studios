using System.Collections;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private bool isTapped;
    //private bool isThrown = false;
    [SerializeField] private float grav;
    private float initialJumpTime;
    private float initialJumpSpeed;
    public float iJFMin;
    public float iJFMax;
    public float rSpeedMin;
    public float rSpeedMax;
    public float iJTMin;
    public float iJTMax;
    public float mDMin;
    public float mDMax;
    public float iJSMin;
    public float iJSMax;
    public float decaySpeed;
    //private float decaySpeed = 50f;
    private float initialJumpForce;
    private bool isJumping;
    private float moveDir;
    private float randomSpeed;
    private ConstantForce cForce;
    private Vector3 forceDirection = Vector3.zero;

    public GameObject deathThingy;
    private Rigidbody rb;

    private void Awake()
    {
        cForce = GetComponent<ConstantForce>();
        cForce.force = forceDirection;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Throw();
    }

    // Update is called once per frame
    void Update()
    {
        
        JumpCheck();
        Jump();
    }

    

    public void TouchedAppropriately()
    {
        isJumping = false;
        forceDirection = new Vector3(0, 0, 0);
        rb.isKinematic = true;
        cForce.force = forceDirection;
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        deathThingy.SetActive(true);
        FindAnyObjectByType<FNMGManager>().ProgressCheck();
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void Throw()
    {
        Debug.Log("times thrown");
        //initialJumpForce = 5;
        //initialJumpTime = 0.5f;
        //initialJumpSpeed = 60;
        //moveDir = 5;
        //randomSpeed = 5;
        initialJumpForce = Random.Range(iJFMin, iJFMax);
        initialJumpTime = Random.Range(iJTMin, iJTMax);
        initialJumpSpeed = Random.Range(iJSMin, iJSMax);
        moveDir = Random.Range(mDMin, mDMax);
        randomSpeed = Random.Range(rSpeedMin, rSpeedMax);
        isJumping = true;
    }
    //For jump
    private void Jump()
    {
        if (isJumping && isTapped == false)
        {
            Debug.Log("jumping");
            forceDirection = new Vector3(moveDir * randomSpeed, initialJumpForce * initialJumpSpeed, 0);
            cForce.force = forceDirection;
        }
    }
    private void JumpCheck()
    {
        if (isJumping == true && isTapped == false)
        {
            Debug.Log("jump time decreasing");
            initialJumpTime -= Time.deltaTime;
            if (initialJumpSpeed >= 0)
            {
                Debug.Log("ijs decreasing");
                initialJumpSpeed -= Time.deltaTime * decaySpeed;
            }

        }
        if (initialJumpTime <= 0 && isTapped == false)
        {
            isJumping = false;
            forceDirection = new Vector3(0, grav, 0);
            cForce.force = forceDirection;
            Destroy(gameObject, 5f);
        }
    }
}
