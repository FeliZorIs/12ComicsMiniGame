using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteSound : MonoBehaviour
{
    public void Mute()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
