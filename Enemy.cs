using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    int health = 100;
    private Rigidbody2D rigidbody2D;

    public ParticleSystem ps;

    private Vector3 pos1 = new Vector3(9.0f, 2.6f, 68.6f);
    private Vector3 pos2 = new Vector3(26.24f, 2.6f, 68.6f);
    private float speed = 0.2f;

    public AudioSource ads;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ads = GetComponent<AudioSource>();
    }
    void Update()
    {
       transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time*speed, 1.0f));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        //ps.Play();
        ads.Play();
        Debug.Log(health);
        

        if(health <= 0)
        {
            
            Die();  
        }
    }
    void Die()
    {
        Debug.Log("Die");
        Instantiate(ps, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
