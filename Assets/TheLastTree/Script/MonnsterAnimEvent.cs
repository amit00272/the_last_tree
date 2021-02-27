using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonnsterAnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
     public GameObject monsterAnimator;
     public void engulfEvenyt() {
         monsterAnimator.GetComponent<Animator>().SetBool("isMonsterEngulf",true);
         monsterAnimator.GetComponent<Animator>().SetBool("isMonsterattack",false);
    }
}
