using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    public Transform[] waypoints;
    private int targetIndex;
    public float movingSpeed;
    public float turnSpeed;
    public float attackSpeed;
    public GameObject player;
    [SerializeField] private bool isAttacking;
    public float killDistance;
    [SerializeField] private float maxHearingDistance;
    [SerializeField] private float minHearingDistance;
    [SerializeField] public int animationState = 1; //1-moving, 2-detected, 3-attack
    public float attackDistance;
    public Animator anim;
    public float attackTurnSpeed;
    public GameObject soundManager;
    private Vector3 defaultSoundPosition;
    private Vector3 soundPosition;
    public float killTolerance;
    private float distanceToSound;
    private float distanceToPlayer;

    void Start()
    {
        targetIndex = 0;
        defaultSoundPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        anim.SetInteger("animationState", animationState);

        soundPosition = soundManager.GetComponent<CurrentSound>().soundPosition;
        distanceToSound = Vector3.Distance(this.transform.position, soundPosition);
        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer < killDistance && isAttacking)
        {
            player.GetComponent<PlayerController>().Die("You have been slainded by otherworldly monsters...");
        }

        if (distanceToPlayer<minHearingDistance && player.GetComponent<PlayerController>().isTranslating == true)
        {
            Attack(player.transform.position);
        }
        else
        {
            if (soundPosition == defaultSoundPosition)
            {
                Patrol();
            }
            else
            {
                if (distanceToSound < maxHearingDistance)
                {
                    Attack(soundPosition);
                }
                else
                {
                    Patrol();
                }

            }
        }
        //pustanje animacije napada
        if (distanceToSound < killDistance + attackDistance && isAttacking)
        {
            animationState = 3;
        }
    }

    private void Patrol()
    {
        isAttacking = false;
        animationState = 1;
        if (transform.position == waypoints[targetIndex].position)
        {
            nextTargetPoint();
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movingSpeed * Time.deltaTime);

        var targetRotation = Quaternion.LookRotation(waypoints[targetIndex].transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
    private void nextTargetPoint()
    {
        targetIndex++;
        if (targetIndex >= waypoints.Length)
            targetIndex = 0;
    }
    private void Attack(Vector3 target)
    {
        isAttacking = true;
        animationState = 2;
        transform.position = Vector3.MoveTowards(transform.position, target, attackSpeed * Time.deltaTime);
        var targetRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
