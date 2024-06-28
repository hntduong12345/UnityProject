using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavedEnermy : MonoBehaviour
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

    //========Enermy Status========
    private bool isFixed;
    #endregion

    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource fixSound;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Ruby");
        isFixed = false;
    }

    void Update()
    {
        if (!isFixed)
        {
            Chase();
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
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        enemyAnimation.SetTrigger("Fixed");
        isFixed = true;

        fixSound.Play();
        walkSound.enabled = false;
        //Add particle

        EnermyWaveManager.Instance.IncreaseCounter();
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
