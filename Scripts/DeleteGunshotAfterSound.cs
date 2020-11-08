using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteGunshotAfterSound : MonoBehaviour
{

    private float totalTimeBeforeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        var sound = this.GetComponent<AudioSource>();
        totalTimeBeforeDestroy = sound.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        totalTimeBeforeDestroy -= Time.deltaTime;

        if(totalTimeBeforeDestroy <= 0f)
            Destroy(this.gameObject);
    }
}
