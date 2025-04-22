using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enterarea : MonoBehaviour
{
    public static bool dest;
    public static bool detour;
    public static bool exiting;
    public static bool exited;
    public static bool outside;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AI") || other.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Finish"))
            {
                dest = true;
                Debug.Log("Destination reached!");
            }
            if (gameObject.CompareTag("Detour"))
            {
                detour = true;
                Debug.Log("detour detected");
            }
            if (gameObject.CompareTag("Exit"))
            {
                exiting = true;
                Debug.Log("You're exiting");
                
            }
            if (gameObject.CompareTag("Exited"))
            {
                outside = true;
                PlayerScript.resetPlayer = true;
                Debug.Log("You're exited");
                
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("AI") || other.gameObject.CompareTag("Player"))
        {

            if (gameObject.CompareTag("Exit"))
            {
                exited = true;
                Debug.Log("You exited");
            }



        }



    }


}
