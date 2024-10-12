using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    void Update()   
    {
        if(transform.position.y < -20)
        {
            UiManager.Instance.SetLoseMenu(true);
            PauseManager.Instance.Pause();
            ResetPlayer();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       UiManager.Instance.SetWinMenu(true);
    }
    public void ResetPlayer()
    {
        transform.position = new Vector3(0, 1, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
