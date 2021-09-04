using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBossmusic : MonoBehaviour
{
    AudioSource Audio;
    public GameObject Oldmusic;

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(Oldmusic);
            Audio.Play();
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
