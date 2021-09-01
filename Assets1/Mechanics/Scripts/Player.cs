using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bullet2;
    [SerializeField] float nextFire;

    float minX, minY, maxX, maxY, tamX, tamY, canFire;

    // Start is called before the first frame update
    void Start()
    {
         tamX = (GetComponent<SpriteRenderer>()).bounds.size.x;
         tamY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        Vector2 esqSupDer = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        maxX = esqSupDer.x - tamX/2;
        maxY = esqSupDer.y - tamY/2;
        //Debug.Log(esqSupDer.ToString());
        Vector2 esqInfIzq = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        minX = esqInfIzq.x + tamX/2;
        minY = esqInfIzq.y + 7;
  
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Fire();
    }

    void Movement()
    {
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        transform.Translate(new Vector2(movH * Time.deltaTime * speed, movV * Time.deltaTime * speed));

        float newX = Mathf.Clamp(transform.position.x, minX, maxX);
        float newY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector2(newX, newY);
    }
    void Fire()
    {
        
        if(Input.GetKeyDown(KeyCode.Space) && Time.time >= canFire)
        {
            Instantiate(bullet, transform.position - new Vector3(0,tamY/2,0), transform.rotation);
            canFire = Time.time + nextFire; //nextFire = nextFire + Time.time
        }
        if (Input.GetKeyDown(KeyCode.Z) && Time.time >= canFire)
        { 
                Instantiate(bullet2, transform.position - new Vector3(0, tamY / 2, 0), transform.rotation);
                canFire = Time.time + 2 * nextFire; //nextFire = 2*nextFire + Time.time
        }
    }
}
