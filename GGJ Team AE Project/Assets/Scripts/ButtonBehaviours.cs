using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviours : MonoBehaviour
{
    //disables parent gameobject
    public void DisableParent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
