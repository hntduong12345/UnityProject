using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    public GameObject target;

    #region Enemy
    [SerializeField]
    private Rigidbody2D enermyRb;
    [SerializeField]
    private Animator enemyAnimation;

    //========Enermy Movement========
    public float moveSpeed;
    private Vector2 moveDirection;

    private float distance;
    [SerializeField]
    private float distanceBetween;

    [SerializeField]
    private float changeDirectionTime;
    private float remainingChangeTIme;
    [SerializeField]
    private bool isMoveHorizontal;

    //========Enermy Status========
    private bool isFixed;

    //========Enermy Type==========
    private enum Type
    {
        CHASE,
        MOVE_AROUND
    }

    [SerializeField]
    private Type enermyType;
    #endregion

    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource fixSound;

    private void Start()
    {
        isFixed = false;
        remainingChangeTIme = changeDirectionTime;
        if (enermyType == Type.MOVE_AROUND)
        {
            moveDirection = isMoveHorizontal ? Vector2.right * moveSpeed : Vector2.down * moveSpeed;
        }
    }

    void Update()
    {
        if (!isFixed)
        {
            switch (enermyType)
            {
                case Type.CHASE:
                    Chase();
                    break;
                case Type.MOVE_AROUND:
                    MoveAround();
                    break;
            }
        }
        else
        {
            Stand();
        }
    }

    private void FixedUpdate()
    {
        enermyRb.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    private void MoveAround()
    {
        remainingChangeTIme -= Time.deltaTime;

        if (remainingChangeTIme <= 0)
        {
            remainingChangeTIme += changeDirectionTime;
            moveDirection *= -1;
        }

        enemyAnimation.SetFloat("ForwardX", moveDirection.x);
        enemyAnimation.SetFloat("ForwardY", moveDirection.y);
        enemyAnimation.SetFloat("Speed", 1);
        walkSound.enabled = true;
    }

    #region MovementTypeFunction
    private void Chase()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();

        if (distance < distanceBetween)
        {
            moveDirection = direction * moveSpeed;
            enemyAnimation.SetFloat("ForwardX", direction.x);
            enemyAnimation.SetFloat("ForwardY", direction.y);
            enemyAnimation.SetFloat("Speed", 1);
            walkSound.enabled = true;
        }
        else
        {
            enemyAnimation.SetFloat("Speed", 0);
            moveDirection = Vector2.zero;
            walkSound.enabled = false;
        }
    }

    private void Stand()
    {
        moveDirection = Vector2.zero;
    }
    #endregion

    public void Fix()
    {
        enemyAnimation.SetTrigger("Fixed");
        isFixed = true;

        fixSound.Play();
        walkSound.enabled = false;
        //Add particle


        Destroy(gameObject, 2.5f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isFixed) return;

        Ruby ruby = collision.collider.GetComponent<Ruby>();
        if (ruby != null)
        {
            ruby.ChangeHealth(-1);
        }

        StartCoroutine(AttackDelay());
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2.5f);
    }
}
