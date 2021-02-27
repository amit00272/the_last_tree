using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CardOpen : MonoBehaviour
{
    // Start is called before the first frame update
     public Sprite[] commonCards;
    public Sprite[] rareCards;
    public Sprite[] legendryCards;
    public Image cardImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void unlockCommonCards(int number){
        cardImage.sprite  =  commonCards[number - 1];
        StartCoroutine(startAnimation());
    }

    public void unlockRareCards(int number){
        cardImage.sprite =  rareCards[number - 1 ];
        StartCoroutine(startAnimation());
    }

    public void unlockLegenedryCards(int number){
        cardImage.sprite =  legendryCards[number - 1];
        StartCoroutine(startAnimation());
    }

    IEnumerator startAnimation(){

        
        cardImage.DOFade(1, 2.1f);
        cardImage.transform.DOScale(Vector3.one * 2, 2).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(3.1f);
        cardImage.DOFade(0, 2f);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return null;
    
    }
}
