using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurdlesEnableMgr : MonoBehaviour
{

    public string childTag;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("st", 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void st()
    {
        StartCoroutine(enableHurdle());
    }


    IEnumerator enableHurdle()
    {

        Debug.Log("childCount"+transform.childCount);
        foreach (Transform child in transform)
        {
            
            if (child.tag == childTag)
            {

                child.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);

        }

        yield return null;
    }
}
