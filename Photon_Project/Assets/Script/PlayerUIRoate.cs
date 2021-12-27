using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIRoate : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().LookAt(Camera.main.transform.position);
    }
}
