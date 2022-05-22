using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float walkRadius;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;
    Vector3 spawnPoint;
    

    public LayerMask whatIsGround;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;

    public float walkPointRange;

    //Attacking
    public bool canAttack = true;
    public float attackCooldown = 1f;

    //References
    private Animator anim;
    public CharacterStats myStats;
    public GameObject healthBar;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        spawnPoint = gameObject.transform.position;
        anim = GetComponentInChildren<Animator>();
        myStats = GetComponent<CharacterStats>();
        walkRadius = 10f;
        healthBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (myStats.currentHealth > 0)
        {
            float distance = Vector3.Distance(target.position, transform.position);


            if (distance <= lookRadius)
            {
                
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance)
                {
                    //attack the target
                    healthBar.SetActive(true);
                    //facet the target
                    CharacterStats targetStats = target.GetComponent<CharacterStats>();
                    if (targetStats != null)
                    {
                        if (canAttack)
                        {
                            combat.Attack(targetStats);
                            anim.SetTrigger("Attack");
                            canAttack = false;
                            StartCoroutine(ResetAttackCooldown());
                        }
                    }

                    FaceTarget();
                }
            }
            else
            {  
                healthBar.SetActive(false);
                Patrol();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            float distanceToSpawn = Vector3.Distance(spawnPoint, walkPoint);


            if (distanceToSpawn >= 20f)
            {
                walkPoint = spawnPoint;
            }

            agent.SetDestination(walkPoint);
            anim.SetFloat("Speed", 0.5f);
        }


        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        // float randomZ = Random.Range(-5f, 5f);
        // float randomX = Random.Range(-5f, 5f);
        //
        //
        // walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //
        //
        // if (!Physics.Raycast(walkPoint, transform.up, 2f) && Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        //     walkPointSet = true;
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1))
        {
            Vector3 finalPosition = hit.position;
            walkPoint = finalPosition;
            walkPointSet = true;
        }
        
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}