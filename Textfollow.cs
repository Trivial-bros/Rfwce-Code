using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textfollow : MonoBehaviour
{
    public Transform canvas;
    public Vector3 offset;
    void Update()
    {

        canvas.position = transform.position + offset;
    }
}
