using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject projectile;
    public GameObject stickyProjectile;
    public GameObject player;
    public float spawntime = 1f;
    private float timer;
    public float force = 50f;
    public float radius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = spawntime;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnProjectile();
    }

    public void SpawnProjectile()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Vector3 pos = RandomCircle(transform.position, radius);
            GameObject b;
            if (Random.Range(0,10) == 0)
            {
                b = Instantiate(stickyProjectile, transform.position - pos, transform.rotation);

            }
            else
            {

            b = Instantiate(projectile, transform.position - pos, transform.rotation);
            }
            b.GetComponent<Rigidbody>().AddForce((player.transform.position - b.transform.position ) * force);
            timer = spawntime;
            GameObject.Destroy(b, 1.5f);
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        return pos;
    }
}
