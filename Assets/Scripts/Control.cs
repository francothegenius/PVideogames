using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    private AudioSource audio;
    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
        audio.Stop();
    }
}
