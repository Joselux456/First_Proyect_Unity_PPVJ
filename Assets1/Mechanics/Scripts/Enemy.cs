using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] bool direction;
    [SerializeField] float health;
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject healthBarDeplete;
    [SerializeField] float debuffTime;
    

    float minX, maxX, tamX, tamY, canMove, space;
    float formerSpeed;
    List<GameObject> hearts = new List<GameObject>();
    GameObject removal;

    // Start is called before the first frame update
    void Start()
    {
        tamX = GetComponent<SpriteRenderer>().bounds.size.x;
        tamY = GetComponent<SpriteRenderer>().bounds.size.y;
        formerSpeed = speed;
        space = (health / 2) - health;

        for (int i = (int)health; i >= 1; i -= 1)
        {
            if (health%2==0)
            {
                hearts.Add(Instantiate(healthBar, transform.position + new Vector3(-space - (0.5f), tamY, 0), transform.rotation, transform));
            }
            else
            {
                hearts.Add(Instantiate(healthBar, transform.position + new Vector3(-space-1, tamY,0), transform.rotation, transform));
            }
            space += 1;
        }

        Vector2 esqDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esqDer.x;
        //Debug.Log(esqSupDer.ToString());
        Vector2 esqIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esqIzq.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction)
            transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        else
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        if (transform.position.x >= maxX)
            direction = false;
        else if(transform.position.x <= minX)
            direction = true;
        
            ParalysisStop();
        
        

    }

    private void ParalysisStop()
    {
        if (Time.time >= canMove)
        {
            speed = formerSpeed;
            canMove = Time.time + 2*debuffTime;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if(health != 0)
            {
                health -= 1;
                HeartDeduction();
            }
            if(health == 0)
            {
                Destroy(this.gameObject);
            }
            
        }
        if (collision.gameObject.CompareTag("Bullet2"))
        {
            speed = 0;
        }
    }
    private void HeartDeduction()
    {
        
        foreach (GameObject gameObject in hearts)
        {
            if (hearts.IndexOf(gameObject) >= health)
            {
                GameObject.Destroy(gameObject);
                GameObject.Instantiate(healthBarDeplete, gameObject.GetComponent<Transform>().position, transform.rotation, transform);
            }
            removal = gameObject;
            
        }
        hearts.Remove(removal);
    }
}
