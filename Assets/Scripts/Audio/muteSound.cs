using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteSound : MonoBehaviour
{
    static public float theVolume = 1f;
    static public bool muted = false;
    
    void Update()
    {
        AudioListener.volume = theVolume;

        //Get a reference to the slider. If we mute the volume, make the slider value = to 0.
    }
    public void Mute()
    {
        if (theVolume == 1f)
        {
            muted = true;
            theVolume = 0f;
        }
        else
        {
            muted = false;
            theVolume = 1f;
        }
       // AudioListener.volume = yourVolume;

        //AudioListener.pause = !AudioListener.pause;
    }

    public void adjustVolume(float slideVolume)
    {
        if(slideVolume == 0)
        {
            muted = true;
            //This will be for us forcing the mute picture.
        }
        else
        {
            muted = false;
        }
        theVolume = slideVolume;
    }
}
