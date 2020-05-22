using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.Animations;

public class EnemyController : MonoBehaviour
{
    public Transform[] navPoint;
    public NavMeshAgent agent;
    public Transform goal;
    public Transform player;
    private Animator anim;

    //public ParticleSystem flames;
    //public ParticleSystem hurt;
   
    float playerDistance;
    public float awareAI = 14f;
    public float AIMoveSpeed;
    //public float damping = 6.0f;    
    int destPoint = 0;
    
    bool ischasing = false;
    bool isfollowing = false;
    float angleToPlayer;
    float startSpeed;

    float timer = 15f; //tempo inseguimento se non visto
    float timeleft;

    bool seenplayer = false;
    bool closeattack = false;

    public GameObject monster_Right_Fist;

    
    //public Transform attackPoint;
    //public float attackRange;
    //public LayerMask playerLayer;
    //public int timeToAttack;
    //Coroutine currentCoroutine;
    //public bool attackFlag;

    //variabili animazione



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = goal.position;
        agent.autoBraking = false;
        startSpeed = agent.speed;
        timeleft = timer;
        MoveToNextPoint();

        // controllo su animator
        anim = this.GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("no animator");
            return;
        }
        if (anim != null)
            UpdateAnimations();
    }

    void Update()
    {
        //if (attackFlag == false)
        //{
        //    if(currentCoroutine != null)
        //        StopCoroutine(currentCoroutine);
        //}
        playerDistance = Vector3.Distance(player.position, transform.position);
        Vector3 targetDir = player.position - transform.position;
        angleToPlayer = (Vector3.Angle(targetDir, transform.forward));

        if (playerDistance < 2f)
        {
            //attackFlag = true;
            closeattack = true;
            //Debug.Log("enemyattack");
            anim.SetTrigger("Attack"); // RISOLVERE PROBLEMA
            //if(attackFlag == true)
            //    currentCoroutine = StartCoroutine(TimeToAttackMethod(timeToAttack));
        }
        else
        {
            closeattack = false;
        }

        /*if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stabbing"))
        {
            agent.speed = 0.3f;
        }
        else
        {
            agent.speed = startSpeed; // RISOLVERE PROBLEMA
        }*/

        /*if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60)
        {
            LookAtPlayer();
        }*/

        if (playerDistance < awareAI && angleToPlayer >= -60 && angleToPlayer <= 60) //se in cono visivo (120) ed entro distanza
        {
            seenplayer = true;

            timeleft = timer;
            LookAtPlayer();

            if (playerDistance > 2f)
            {
                //ischasing = true;
                if(ischasing == false)
                {
                    ischasing = true;
                    isfollowing = false;
                    StartCoroutine(ExecuteAfterTime(2f));
                }
                //StartCoroutine(ExecuteAfterTime(2f));
            }
            /*else
            { 
                MoveToNextPoint();
            }*/
        }
        else if(ischasing == true) //timer inseguimento se non in cono visivo e in certa distanza
        {
            if(timeleft > 0f)
            {
                timeleft -= Time.deltaTime;
                //Debug.Log(Mathf.Round(timeleft));
                Chase();
            }
            else //quando scade timer
            {
                ischasing = false;
            }
        }
        else //patrol
        {
            if(ischasing == false)
            {
                MoveToNextPoint();
            }
            //MoveToNextPoint();
        }

        void LookAtPlayer()
        {
            if(ischasing == false || isfollowing == false) //se non lo sta già inseguendo si ferma a guardare player
            {
                agent.speed = 0f;
            }
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            StartCoroutine(FollowAfterTime(2f)); //dopo 2 secondi inizia a seguire player
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f && ischasing == false) //patrol
        {
            MoveToNextPoint();
        }
    }

    private void UpdateAnimations()
    {
        anim.SetFloat("Speed", agent.speed);
        anim.SetBool("SeenPlayer", seenplayer);
    }

    void MoveToNextPoint()
    {
        if (ischasing == true)
            ischasing = false;
        if (agent.stoppingDistance != 0f) //tutti reset condizione patrol, non inseguimento
            agent.stoppingDistance = 0f;
        if (agent.speed == AIMoveSpeed)
            agent.speed = 3.4f;
        if (timeleft != timer)
            timeleft = timer;

        seenplayer = false;

        if (navPoint.Length == 0)
            return;
        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        Chase();

        //Debug.Log("insegue veloce più veloce");
    }

    IEnumerator FollowAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        if(agent.speed < startSpeed) //aumento velocità da quando si ferma
        {
            agent.speed = startSpeed;
        }
        isfollowing = true;
        //Debug.Log("sta seguendo");
    }

    void Chase() //inseguimento player, ischasing settato prima, in update
    {
        //transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
        agent.SetDestination(goal.position);
        agent.speed = AIMoveSpeed;
        agent.stoppingDistance = 0.7f;
    }

    private void UpdateAnimation() // metodo dove implementare animazioni  da richiamare dell'update
    { 
     
        //if(!isFollowingPlayer)
    }

    //private IEnumerator TimeToAttackMethod(int t_before_attack)
    //{

    //    yield return new WaitForSeconds(t_before_attack);
    //    Collider[] playerCollider = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);
    //    if (playerCollider.Length != 0)
    //    {
    //        Debug.Log("colpisco player");
    //        Debug.Log(playerCollider[0]);
    //        playerCollider[0].GetComponent<PlayerHealthManager>().TakeDamage(30);
    //        closeattack = false;

    //    }
    //    attackFlag = false;




    //}

    public void activateFist()
    {
        monster_Right_Fist.GetComponent<Collider>().enabled = true;
    }

    public void deactivateFist()
    {
        monster_Right_Fist.GetComponent<Collider>().enabled = false;
    }
}
