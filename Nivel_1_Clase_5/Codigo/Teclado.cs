using System;
using UnityEngine;
using UnityEngine.Assertions;

public class Teclado : MonoBehaviour
{
    [SerializeField] AudioClip[] SonidosDeTeclas;
    [SerializeField] Terminal TerminalConectada;

    AudioSource fuenteDeAudio;

    private void Start()
    {
        fuenteDeAudio = GetComponent<AudioSource>();
        QualitySettings.vSyncCount = 0; 
        Application.targetFrameRate = 1000; 
        AvisoDeTerminalDesconectada();
    }

    private void AvisoDeTerminalDesconectada()
    {
        if (!TerminalConectada)
        {
            Debug.LogWarning("Teclado no conectado a la terminal");
        }
    }

    private void Update()
    {
        bool esTeclaValida = Input.inputString.Length > 0;
        if (esTeclaValida)
        {
            ReproducirSonidoDeTelcaRandom();
        }
        if (TerminalConectada)
        {
            TerminalConectada.GetBufferDeInput(Input.inputString);
        }
    }

    private void ReproducirSonidoDeTelcaRandom()
    {
        int nroRandom = UnityEngine.Random.Range(0, SonidosDeTeclas.Length);
        fuenteDeAudio.clip = SonidosDeTeclas[nroRandom];
        fuenteDeAudio.Play();
    }
}
