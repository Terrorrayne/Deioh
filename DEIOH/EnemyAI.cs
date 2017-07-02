using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float enemyLineOfSight;
    public float enemyAggroRange;
    public float enemyAttackRange;
    public float enemyMovementSpeed;
    public float damping;
    public transform taget;
    Rigidbody theRigidBody;
    Renderer myRender;

    //initilize
    void Start () {
        theRigidBody = GetComponent<>();
        myRender = GetComponent<>();
}

    //Update
    void FixedUpdate () {
        enemyAggroRange = Vector3.Distance(target.position, transform.position);
        if (enemyAggroRange<enemyLineOfSight) {
            myRender.material.color=Color.yellow;
            lookAtPlayer();
            print("I'm watching you!");
        }
        if (enemyAggroRange<enemyAttackRange) {
            attackPlease();
            print ("Take this!");
        }
}

    void LookAtPlayer ()
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - tranform.position);
        transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime*damping);
    }
    void Attack ()
    {
        theRigidBody.AddForce(transform.forward*enemyMovementSpeed);
    }

    public void EnemyAI.Class1()
	{
	}
}
