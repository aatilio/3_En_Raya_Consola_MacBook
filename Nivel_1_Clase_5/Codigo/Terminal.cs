using UnityEngine;
using System.Reflection;

public class Terminal : MonoBehaviour
{
    BufferDeDisplay bufferDeDisplay;
    InputBuffer bufferDeInput;

    static Terminal TerminalPrimaria;

    private void Awake()
    {
        if (TerminalPrimaria == null) { TerminalPrimaria = this; } 
        bufferDeInput = new InputBuffer();
        bufferDeDisplay = new BufferDeDisplay(bufferDeInput);
        bufferDeInput.onCommandSent += ManejadorDeNotificacionDeComandos;
    }

    public string GetBufferDeDisplay(int ancho, int alto)
    {
        return bufferDeDisplay.GetDisplayBuffer(Time.time, ancho, alto);
    }

    public void GetBufferDeInput(string input)
    {
        bufferDeInput.ReceiveFrameInput(input);
    }

    public static void BorrarPantalla()
    {
        TerminalPrimaria.bufferDeDisplay.Clear();
    }

    public static void EscribirLinea(string linea)
    {
        TerminalPrimaria.bufferDeDisplay.WriteLine(linea);
    }

    public void ManejadorDeNotificacionDeComandos(string input)
    {
        var TodosLosGameObjects = FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour mb in TodosLosGameObjects)
        {
            var flags = BindingFlags.NonPublic | BindingFlags.Instance;
            var metodoObjetivo = mb.GetType().GetMethod("OnUserInput", flags);
            if (metodoObjetivo != null)
            {
                object[] parameters = new object[1];
                parameters[0] = input;
                metodoObjetivo.Invoke(mb, parameters);
            }
        }
    }
}