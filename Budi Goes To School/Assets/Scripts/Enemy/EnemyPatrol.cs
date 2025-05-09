using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] GameObject objPointA;
    [SerializeField] GameObject objPointB;
    [SerializeField] float fltSpeed;

    private Rigidbody2D rb;
    private Transform transCurrentPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transCurrentPoint = objPointB.transform;
    }

    void Update()
    {
        Vector2 point = transCurrentPoint.position - transform.position;
        if (transCurrentPoint == objPointB.transform)
        {
            rb.velocity = new Vector2(fltSpeed,0);
        }
        else
        {
            rb.velocity = new Vector2(-fltSpeed,0);
        }

        if (Vector2.Distance(transform.position, transCurrentPoint.position) < 0.5f && transCurrentPoint == objPointB.transform)
        {
            Flip();
            transCurrentPoint = objPointA.transform;
        }

        if (Vector2.Distance(transform.position, transCurrentPoint.position) < 0.5f && transCurrentPoint == objPointA.transform)
        {
            Flip();
            transCurrentPoint = objPointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(objPointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(objPointB.transform.position, 0.5f);
        Gizmos.DrawLine(objPointA.transform.position, objPointB.transform.position);
    }
}
