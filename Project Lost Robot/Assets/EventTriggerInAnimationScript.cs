using System;
using UnityEngine;
using UnityEngine.Events;

public class EventTriggerInAnimationScript : MonoBehaviour

{

    public UnityEvent animEvent1;

    [SerializeField] private bool triggerAnimEvent1 = false;


    void Update()
    {
          if(triggerAnimEvent1 == true)
          {

              animEvent1.Invoke();
              triggerAnimEvent1 = false;

          } 
    }
        
     

}