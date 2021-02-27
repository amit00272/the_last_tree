using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class MonsterAnimation : MonoBehaviour
{
    public bool isPlayerCatched = false;
   // public UnityArmatureComponent armatureComponent;
    public GameObject monsterAnimator;
    //public GameObject _aStar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player" && !isPlayerCatched)
        {
            isPlayerCatched = true;
            GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<MonsterFlip>().enabled = false;
            // GetComponent<MonsterFlip>().isHit=true;
            if(monsterAnimator){

                monsterAnimator.GetComponent<Animator>().Play("monsterEngulf");
             //   _aStar.SetActive(false);

              // monsterAnimator.GetComponent<Animator>().SetBool("isMonsterattack",true);
                Color tmp = collision.gameObject.GetComponent<SpriteRenderer>().color;
                    tmp.a = 0f;
                collision.gameObject.GetComponent<SpriteRenderer>().color = tmp;
                collision.gameObject.GetComponent<PlayerMovement>().catchedByMonster();
               //collision.gameObject.SetActive(false);
                }
           //if(armatureComponent)
           //  armatureComponent.animation.FadeIn("monster-win", 0.25f, -1);
        }
    }


}
