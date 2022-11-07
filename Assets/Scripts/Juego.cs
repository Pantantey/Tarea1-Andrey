using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Juego : MonoBehaviour
{
    public Ruta[] bancoDecisiones;
    public TextMeshProUGUI enunciado;
    public TextMeshProUGUI[] respuestas;
    public int ruta;
    public Decision rutaActual;

    private int rutaSeleccionada = 0;
    

    public Button[] btnEleccion;


    // Start is called before the first frame update
    void Start()
    {
        ruta = 0;
        cargarBancoDecisiones();
        setDecision();
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
        //int rutaSeleccionada = 0;

        rutaActual = bancoDecisiones[ruta].preguntas[rutaSeleccionada];
        enunciado.text = rutaActual.enunciado;
        for (int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i].text = rutaActual.respuestas[i].eleccion;
        }
    }

}
