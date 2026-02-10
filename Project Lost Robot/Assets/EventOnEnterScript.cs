using System;
using UnityEngine;
using UnityEngine.Events;
public class EventOnEnterScript : MonoBehaviour

{
    
    public UnityEvent onEnterMango, onEnterRalos, onEnterMidpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mango")) //should be mango & also now it senses the body child obj not parent...
        {
            onEnterMango.Invoke();
            Debug.Log("Mango entered");
        }
        
        if (other.gameObject.CompareTag("Ralos")) //should be mango & also now it senses the body child obj not parent...
        {
            onEnterRalos.Invoke();
            Debug.Log("Ralos entered");
        }
        
        if (other.gameObject.CompareTag("PlayersMidPoint")) //should be mango & also now it senses the body child obj not parent...
        {
            onEnterMidpoint.Invoke();
            Debug.Log("midpoint entered");
        }
        
    }
    
    
}