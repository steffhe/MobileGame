using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Touch touch;
    public float speedModifier;
    private Player player;
    private int hit = 0;
    public GameObject ps;


    // Start is called before the first frame update
    void Start()
    {
        speedModifier = 1f;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit >= 10)
        {
            player.stuck = false;
            hit = 0;
        }

        if (player.stuck)
        {
            if (Input.touchCount > 0)
            {
                foreach (Touch t in Input.touches)
                {

                    Ray raycast = Camera.main.ScreenPointToRay(t.position);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(raycast, out raycastHit))
                    {
                        Debug.Log("Something Hit");
                        if (raycastHit.collider.tag == "Projectile")
                        {
                            hit += 1;
                            Destroy(raycastHit.collider.gameObject);
                            GameObject p = Instantiate(ps, raycastHit.point, transform.rotation, null);
                            p.GetComponent<ParticleSystem>().Play();
                        }

                    }
                }
            }
        }
        else
        {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(
                    transform.position.x + touch.deltaPosition.x * speedModifier / 100,
                    transform.position.y,
                    transform.position.z + touch.deltaPosition.y * speedModifier / 100);
            }
        }
        }
    }
}
