using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()   
    {
        if(transform.position.y < -20)
        {
            GameManager.Instance.ResetLevel();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       GameManager.Instance.won = true;
    }
}
