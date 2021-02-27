using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndVdoCont : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject vdo;
    void Start()
    {
        Invoke("DestryEndVdo", 0.1f);
    }
    private void DestryEndVdo(){

        StartCoroutine("StartSparteffects");
    }

    IEnumerator StartSparteffects(){

        yield return new WaitForSeconds(45);
        SceneManager.LoadScene("Main");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
