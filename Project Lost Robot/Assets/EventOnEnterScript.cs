using System;
using UnityEngine;
using UnityEngine.Events;
public class EventOnEnterScript : MonoBehaviour

{
    
    public UnityEvent onEnterMango, onEnterRalos, onEnterMidpoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mango"))
        {
            onEnterMango.Invoke();
            Debug.Log("Mango entered");
        }
        
        if (other.gameObject.CompareTag("Ralos"))
        {
            onEnterRalos.Invoke();
            Debug.Log("Ralos entered");
        }
        
        
        
        
        //För att aktivera onEnterMidpoint eventet nedan (t.ex. för scene transitions - både i fallet där spelarna befinner sig vid samma punkt OCH där de
        //måste befinna sig vid olika punkter) placera triggerzonen med detta script
        //exakt där spelarnas midpoint object skulle positioneras mellan spelarna då de står på rätt plats(er)
        //(leveldesign bör då ej tillåta en spelare att smita iväg för långt, t.ex. om de ska in i en grotta etc)
        if (other.gameObject.CompareTag("PlayersMidPoint"))
        {
            onEnterMidpoint.Invoke();
            Debug.Log("midpoint entered");
        }
        
    }
    
    
}