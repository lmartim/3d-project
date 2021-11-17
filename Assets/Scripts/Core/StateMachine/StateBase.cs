using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter()
        {
            //Debug.Log("Enter");
        }

        public virtual void OnStateStay()
        {
            //Debug.Log("Stay");
        }

        public virtual void OnStateExit()
        {
            //Debug.Log("Exit");
        }
    }
}

