using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void CambiarEscenaClick(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }
}
