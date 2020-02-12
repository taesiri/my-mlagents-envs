using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeHeadScript : MonoBehaviour
{
    public bool HeadBottom = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.name == "Floor")
        {
            HeadBottom = true;
        }
    }

}
