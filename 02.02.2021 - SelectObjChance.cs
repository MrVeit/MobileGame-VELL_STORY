using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjChance : MonoBehaviour
{
    public int CoundToLeave = 1;

    private void Start() //уничтожает внутри себя все объекты, кроме заданного.
    {
        while (transform.childCount > CoundToLeave)
        {
            Transform childToDestroy = transform.GetChild(Random.Range(0, transform.childCount));
            DestroyImmediate(childToDestroy.gameObject);
        }
    }
}
