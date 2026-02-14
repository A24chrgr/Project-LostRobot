using System;
using UnityEngine;
using UnityEngine.Events;
public class EventOnEnterScript : MonoBehaviour

{
    public UnityEvent onEnterMango, onEnterRalos, onEnterMidpoint, onEnterCustomTagged, onStayCustomTagged, onExitCustomTagged;
    
    //[SerializeField] private String customTagString = "PushBlock", customTagStringStay = "EditorOnly", customTagStringExit = "Mango";
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
        
        // if (other.gameObject.CompareTag(customTagString))
        // {
        //     onEnterCustomTagged.Invoke();
        //     Debug.Log("Object with tag "+(customTagString)+" entered");
        // }
        
        if (other.gameObject.CompareTag("PushBlock"))
        {
            onEnterCustomTagged.Invoke();
            Debug.Log("Pushblock entered");
        }
        
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     if (other.gameObject.CompareTag(customTagStringStay))
    //     {
    //         onStayCustomTagged.Invoke();
    //         Debug.Log("Object with tag "+(customTagStringStay)+" entered and stayed");
    //     }
    //     
    //
    // }

    private void OnTriggerExit(Collider other)
    {
        // if (other.gameObject.CompareTag(customTagStringExit))
        // {
        //     onExitCustomTagged.Invoke();
        //     Debug.Log("Object with tag " + (customTagStringExit) + " exited");
        // }
        
        if (other.gameObject.CompareTag("Mango"))
        {
            onExitCustomTagged.Invoke();
            Debug.Log("Object with tag Mango exited");
        }
    }
}