using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public Transform bulletSpawnpoint;
    AudioSource audio;

    public float startTimeBetweenShots;
    float TimeBetweenShots;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if(TimeBetweenShots <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Instantiate(bullet, bulletSpawnpoint.position, bulletSpawnpoint.rotation);
                audio.Play();
                TimeBetweenShots = startTimeBetweenShots;
            }
            
        }
        else
        {
            TimeBetweenShots -= Time.deltaTime;
        }


    }

   
}
