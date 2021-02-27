using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    // Start is called before the first frame update

public GameObject prefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         var player=collision.gameObject.GetComponent<PlayerMovement>();
        if (collision.gameObject.tag == "Player" && !player.playerHit )
        { 
            Debug.Log("In trigger");
            player.onPlayerHit();

            if(gameObject.tag=="snow" || gameObject.tag=="fireWheel" || gameObject.tag== "plastic")
            {
              
              if(prefab){
                GameObject parent=transform.parent.gameObject;
                 GameObject tyre=Instantiate(prefab,parent.transform.position,Quaternion.identity);
                 parent.SetActive(false);
                 GameObject.Destroy(parent);
              }
            }
           
            //var direction=transform.position.x-player.transform.position.x;
          //  collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,6f));
        }
    }
        
}
