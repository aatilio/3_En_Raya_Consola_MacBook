using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    [SerializeField] Terminal TerminalConectada;

    // TODO calculate these two if possible
    [SerializeField] int nroCaracteresLargo = 40;
    [SerializeField] int nroCaracteresAncho = 25;

    Text TextoEnPantalla;

    private void Start()
    {
        TextoEnPantalla = GetComponentInChildren<Text>();
        AvisoDeTerminalDesconectada();
    }

    private void AvisoDeTerminalDesconectada()
    {
        if (!TerminalConectada)
        {
            Debug.LogWarning("El display no esta conectado a la terminal");
        }
    }

    // Akin to monitor refresh
    private void Update()
    {
        if (TerminalConectada)
        {
            TextoEnPantalla.text = TerminalConectada.GetBufferDeDisplay(nroCaracteresLargo, nroCaracteresAncho);
        }
    }
} 