using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensitiveSight : MonoBehaviour
{
    public Transform[] waypoints;
    private int targetIndex;
    public float movingSpeed;
    public float turnSpeed;
    public float attackSpeed;
    public GameObject player;
    [SerializeField] private bool isAttacking = false;
    public float killDistance;
    public float fovAngle;
    [SerializeField] private float distanceToPlayer;
    public float angle;
    [SerializeField] private bool isPlayerInFOV;
    [SerializeField] private bool clearLOStoPlayer;
    [SerializeField] private float maxSightDistance;
    public GameObject playervisible;
    public float attackDistance;
    public float attackTurnSpeed;

    void Start()
    {
        targetIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        distanceToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer < killDistance && isAttacking)
        {
            player.GetComponent<PlayerController>().Die("You have been slainded by otherworldly monsters...");
        }

        //Proveri da li je igrac u vidnom polju
        Vector3 toPlayerVector = player.transform.position - this.transform.position;
        Vector3 toPlayerVectorNormalized = toPlayerVector.normalized;
        angle = Vector3.Angle(transform.forward, toPlayerVectorNormalized);

        if (angle < fovAngle)
            isPlayerInFOV = true;
        else
            isPlayerInFOV = false;

        //Proveri da li je igrac vidljiv
        RaycastHit hit;
        Vector3 rayDirection = playervisible.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out hit))
        {
            Debug.DrawRay(transform.position, rayDirection, Color.green);
            if (hit.transform == playervisible.transform)
            {
                clearLOStoPlayer = true;
            }
            else
            {
                clearLOStoPlayer = false;
            }
        }

        //Napadni igraca ako se pomera, ako je uopste viljdiv, ako je u vidnom polju i ako je dovoljno blizu
        if (player.GetComponent<PlayerController>().isMoving == true && clearLOStoPlayer == true && isPlayerInFOV && distanceToPlayer < maxSightDistance)
        {
            isAttacking = true;
            turnSpeed = attackTurnSpeed;
        }

        if (isAttacking)
        {
            Attack();
        }
        else
        {
            if (transform.position == waypoints[targetIndex].position)
            {
                nextTargetPoint();
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movingSpeed * Time.deltaTime);

            var targetRotation = Quaternion.LookRotation(waypoints[targetIndex].transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void nextTargetPoint()
    {
        targetIndex++;
        if (targetIndex >= waypoints.Length)
            targetIndex = 0;
    }

    private void Attack()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, attackSpeed * Time.deltaTime);
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }
}
