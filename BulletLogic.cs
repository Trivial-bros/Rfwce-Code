using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    //Floats & booleans
    public float BulletSpeed;
    public float Bulletlifetime;
    public float explosionRadius;
    public float explosionForce;
    float collisionRadius; [SerializeField]
    bool hasExploded = false;
    bool isColliding = false; [SerializeField]    

    //Refrences and Layermasks
    public GameObject destroyEffect;
    public LayerMask whatToCollideWith;
    public LayerMask whatToLaunch;
    public AudioSource audio;
   
    
    private void Start()
    {

        audio = GetComponent<AudioSource>();
        Invoke("destroy", Bulletlifetime);        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        if (enemy != null)
        {

            enemy.TakeDamage(1);
        }

    }

    void Update()
    {
        transform.Translate(Vector2.up * BulletSpeed * Time.deltaTime);
        isColliding = Physics2D.OverlapCircle(transform.position, collisionRadius, whatToCollideWith);
        if(isColliding == true && hasExploded == false)
        {
            hasExploded = true;
            destroy();
        }
    }

    void destroy()
    {
        audio.Play();
        Collider2D[] hitStuff = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatToLaunch);
        

        foreach(Collider2D obj in hitStuff)
        {
            Vector2 direction = obj.transform.position - transform.position;           
            obj.GetComponent<Rigidbody2D>().AddForce(direction * explosionForce);
        }

        GameObject impactGameobject = Instantiate(destroyEffect, transform.position, transform.rotation);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(impactGameobject, 4f);
        Destroy(gameObject, 4f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
