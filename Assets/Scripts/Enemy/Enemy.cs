using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Attacking,
        Patrol,
        Dead
    }

    private EnemyState _currState = EnemyState.Idle;

    public GameObject playerObj;
    public GunController gun;
    public float health = 50.0f;
    public float attackDistance = 0f;
    public float retreatDistance = 15.0f;
    public float moveSpeed = 5.0f;






    // Use this for initialization
    void Start ()
    {
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        gun = FindObjectOfType<GunController>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (_currState)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Attacking:
                AttackingUpdate();
                break;
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Dead:
                DeadUpdate();
                break;
            default:
                break;
        }

        if (health <= 0)
        {
            ChangeState(EnemyState.Dead);
        }

    }

    private void ChangeState(EnemyState newState)
    {
        ExitState(_currState);
        _currState = newState;
        EnterState(_currState);
    }

    private void ExitState(EnemyState oldState)
    {

    }

    private void EnterState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Attacking:
                break;
            case EnemyState.Patrol:
                break;
            case EnemyState.Dead:
                // Generate particle system.
                EnemySpawner.instance.SpawnDeathParticleSystem(gameObject);
                break;
            default:
                break;
        }

    }

    private void IdleUpdate()
    {
        if (IsPlayerWithinRange())
        {
            ChangeState(EnemyState.Attacking);
            return;
        }

        //    // Check for if patrol path exists.
        //    // Transition to PatrolState if so.

        //    // Run idle animation
        //    // Maybe turn randomly.
    }

    private void AttackingUpdate()
    {
        // Targeting the player and moving towards it
        if(playerObj != null)
        MoveTowardsTarget(playerObj.transform.position);
        // Once with a certain distance do something.
        // Or maybe just try to collide with the player.


        // Track index of current patrol point.
        // MoveTowardsTarget(currPatrolPoint);
    }

    private void PatrolUpdate()
    {
        // Series of points to move between
        // Maybe a delay upon reaching each point.
        // Lots of other ways to implement some form of patrol behavior.

        //    if (IsPlayerWithinRange())
        //    {
        //        ChangeState(EnemyState.Attacking);
        //        return;
        //    }
    }

    private void DeadUpdate()
    {
        // Run death animation.
        // Cleanup and destroy.
        Destroy(this.gameObject);
        
    }

    private bool IsPlayerWithinRange()
    {
        if (playerObj != null)
            return (Vector3.Distance(transform.position, playerObj.transform.position) < attackDistance);
        else
            return false;
    }

    //private bool IsPlayerEscaped()
    //{
    //    return (Vector3.Distance(transform.position, playerObj.transform.position) > retreatDistance);
    //}

    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;

        dir.y = 0.0f;

        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == playerObj) 
        {
            ChangeState(EnemyState.Dead);
        }

        if (collision.gameObject.tag == ConstManager.TAG_BULLET)
        {
            health -= gun.damage;


        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ConstManager.TAG_KILLZONE)
        {
            ChangeState(EnemyState.Dead);
        }
    }
}
