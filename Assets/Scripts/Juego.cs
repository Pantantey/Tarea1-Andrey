using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System;

public class Juego : MonoBehaviour
{
    public Ruta[] bancoDecisiones;
    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI[] elecciones;

    public Button titulo;
    public Button borrar;

    public Decision decisionActual;
    public Button[] btnEleccion;

    public string rutaActual;
    public bool final = false;
    public bool revelar = false;
    public bool juego = false;

    private string revelarPrefsName = "revelo";

    private void Awake()
    {
        LoadData();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (juego)
        {
            rutaActual = "1";
            cargarBancoDecisiones();
            setDecision();
        }
        laVerdad();
    }

    private void Update()
    {
        revelacion();
    }

    private void OnDestroy()
    {
        SaveData();
    }

    public void cargarBancoDecisiones()
    {
        try
        {
            bancoDecisiones = JsonConvert.DeserializeObject<Ruta[]>(File.ReadAllText(Application.streamingAssetsPath + "/BancoDecisiones.json"));

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
            enunciado.text = ex.Message;
        }
    }

    public void setDecision()
    {
        
        for (int i = 0; i <= bancoDecisiones.Length-1; i++)
        {
            string rutaDecisiones = bancoDecisiones[i].ruta;

            if (rutaDecisiones == rutaActual)
            {
                decisionActual = bancoDecisiones[i].preguntas[0];

                enunciado.text = decisionActual.enunciado;
                for (int r = 0; r < elecciones.Length; r++)
                {
                    elecciones[r].text = decisionActual.elecciones[r].eleccion;
                    if (!revelar)
                    {
                        elecciones[2].text = "";
                    }
                    if (elecciones[0].text == "Menú de inicio")
                    {
                        final = true;
                    }
                }
            }
        }
    }

    public void reiniciar()
    {
        revelar = false;
    }

    public void revelacion()
    {
        if (rutaActual == "1-1-2-1" && !revelar)
        {
            revelar = true;
        }
    }

    public void laVerdad()
    {
        if (!juego && revelar)
        {
            titulo.gameObject.SetActive(true);
            borrar.gameObject.SetActive(true);
        }
        if (!juego && !revelar)
        {
            titulo.gameObject.SetActive(false);
            borrar.gameObject.SetActive(false);
        }
    }

    public void proximaRuta(int respuestaJugador)
    {
        if (respuestaJugador == 1 && !final)
        {
            rutaActual += "-1";
            setDecision();
        }
        else if (respuestaJugador == 2 && !final)
        {
            rutaActual += "-2";
            setDecision();
        }
        else if (respuestaJugador == 1 && final)
        {
            SceneManager.LoadScene("MenuInicio");
        }
        else if (respuestaJugador == 2 && final)
        {
            SceneManager.LoadScene("Creditos");
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(revelarPrefsName, Convert.ToInt32(revelar));
    }
    private void LoadData()
    {
        revelar = Convert.ToBoolean(PlayerPrefs.GetInt(revelarPrefsName));
    }

}
