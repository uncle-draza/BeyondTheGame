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
    [SerializeField] public int animationState = 1; //1-moving, 2-detected, 3-attack
    public float attackDistance;
    public Animator anim;
    public float attackTurnSpeed;
    public GameObject soundManager;
    private Vector3 defaultSoundPosition;
    private Vector3 soundPosition;
    public float killTolerance;

    void Start()
    {
        targetIndex = 0;
        defaultSoundPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        soundPosition = soundManager.GetComponent<CurrentSound>().soundPosition;

        if (soundPosition == defaultSoundPosition)
        {
            Patrol();
        }
        else
        {
            AttackTarget();
        }

        /*if(Vector3.Distance(this.transform.position, soundPosition) <= killDistance)
        {
            if(Vector3.Distance(soundPosition, player.transform.position) < killTolerance)
            {
                //kill player
            }
        }*/

        //provera da li je cudoviste kod mete
        //ako jeste, proveri da li je igrac tu
        //ako jeste igrac tu, ubij ga
        //ako nije igrac tu, vrati se da patroliras
    }

    private void Patrol()
    {
        isAttacking = false;
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
    private void AttackTarget()
    {
        isAttacking = true;
        transform.position = Vector3.MoveTowards(transform.position, soundPosition, attackSpeed * Time.deltaTime);
        var targetRotation = Quaternion.LookRotation(soundPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
