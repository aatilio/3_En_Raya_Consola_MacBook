using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gato : MonoBehaviour
{
    pantalla pantallaActiva = pantalla.Start;

    string[,] matriz = new string[3, 3]{ { "    ", "    ","    " },{ "    ", "    ","    " },{ "    ", "    ", "    " } };

    int turno = 1;

    string[] posicion = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    string encabezado, simbol;

    enum pantalla { Start, QuieresJugar, Jugar, JugarMaquina, Termino };
    //Start = Menu Principal
    void Start()
    {
        string mensaje;
        Terminal.EscribirLinea("\n\n\n\t\t\t\t\t!!JUEGO 3 EN RAYA...!!!\n\n\n");
        Terminal.EscribirLinea("Jugar PERSONA vs. PERSONA --- Presione 1 \n");
        Terminal.EscribirLinea("Jugar PERSONA vs. MAQUINA --- Presione 2 \n\n");
    }

    void OnUserInput(string mensaje)
    {
        if (mensaje == "go")
        {
            borrarMostrarQuieresJugar();
        }
        else
        {
            if (pantallaActiva == pantalla.Start)
            {
                QuieresJugar(mensaje);
            }
            else if (pantallaActiva == pantalla.Jugar)
            {
                Jugar(mensaje);
            }
            else if (pantallaActiva == pantalla.JugarMaquina)
            {
                if (mensaje == "" && turno % 2 == 0) //si mensaje es vacio y es turno de la maquina
                {
                    JugarMaquina(posicion[Random.Range(0, posicion.Length)]);
                }
                else if (mensaje != "" && turno % 2 == 0) //si es turno de la maquina y la persona introduce una posicion (no acepta trampa)
                {
                    Terminal.EscribirLinea("NO!!!... puede escojer por la maquina, (°Intro!!!)");
                }
                else //turno de la persona que deve de introducir si o si una posicion
                {
                    JugarMaquina(mensaje);
                }
            }
            else if (pantallaActiva == pantalla.Termino)
            {
                Termino(mensaje);
            }
        }
    }
    // Diseña del tablero que se vera en el terminal
    void tablero()
    {
        Terminal.EscribirLinea("\n\t\t\t ");
        Terminal.EscribirLinea("\t\t\t1\t\t     |2\t\t   |3     ");
        Terminal.EscribirLinea("\t\t\t\t " + matriz[0, 0] + "    |\t   " + matriz[0, 1] + "    |\t " + matriz[0, 2]);
        Terminal.EscribirLinea("\t\t\t______|______|______");
        Terminal.EscribirLinea("\t\t\t4\t\t     |5\t\t   |6     ");
        Terminal.EscribirLinea("\t\t\t\t " + matriz[1, 0] + "    |\t   " + matriz[1, 1] + "    |\t " + matriz[1, 2]);
        Terminal.EscribirLinea("\t\t\t______|______|______");
        Terminal.EscribirLinea("\t\t\t7\t\t     |8\t\t   |9     ");
        Terminal.EscribirLinea("\t\t\t\t " + matriz[2, 0] + "    |\t   " + matriz[2, 1] + "    |\t " + matriz[2, 2]);
        Terminal.EscribirLinea("\t\t\t\t\t\t |\t\t       |      ");
        Terminal.EscribirLinea("\t\t\t ");
    }
    // pantallaActiva==pantalla.Start el mensaje se envia a este metodo
    void QuieresJugar(string mensaje)
    {
        if (mensaje == "1" || mensaje == "one")
        {
            encabezado = "\n\n\t\t\tPERSONA vs. PERSONA";
            repetitivo();
            pantallaActiva = pantalla.Jugar; //el mensaje se envia a este metodo Jugar(mensaje)
            Terminal.EscribirLinea("Turno del Jugardor 1 -> con el simbolo X");
            Terminal.EscribirLinea("Digite la posicion ");
        }
        else
        {
            if (mensaje == "2" || mensaje == "two")
            {
                encabezado = "\n\n\t\t\tPERSONA vs. MAQUINA";
                repetitivo();
                pantallaActiva = pantalla.JugarMaquina; //el mensaje se envia a este metodo JugarMaquina(mensaje)
                Terminal.EscribirLinea("Turno de la persona -> con el simbolo X");
                Terminal.EscribirLinea("Digite la posicion ");
            }
        }
    }
    //metodo que se repite constantemente
    void repetitivo()
    {
        Terminal.BorrarPantalla();
        Terminal.EscribirLinea(encabezado);
        tablero();
    }
    //metodo para escojer el tipo de simbolo a marcar
    void escojerPosicion()
    {
        if (turno % 2 == 1)
            simbol = " X ";
        if (turno % 2 == 0)
            simbol = " O ";
    }
    //metodo para asignarle un simbolo a la posicion de la matris
    void opcionesMarcar(string mensaje)
    {
        switch (mensaje)
        {
            case "1":
                matriz[0, 0] = simbol;
                break;
            case "2":
                matriz[0, 1] = simbol;
                break;
            case "3":
                matriz[0, 2] = simbol;
                break;
            case "4":
                matriz[1, 0] = simbol;
                break;
            case "5":
                matriz[1, 1] = simbol;
                break;
            case "6":
                matriz[1, 2] = simbol;
                break;
            case "7":
                matriz[2, 0] = simbol;
                break;
            case "8":
                matriz[2, 1] = simbol;
                break;
            case "9":
                matriz[2, 2] = simbol;
                break;
        }
    }
    //metodo bool, donde compara si hay 3 en raya, buscando a un supuesto ganador
    bool buscarGanador(string simbol)
    {   
        if ((matriz[0, 0] == simbol && matriz[0, 0] == matriz[0, 1] && matriz[0, 1] == matriz[0, 2]) ||
            (matriz[1, 0] == simbol && matriz[1, 0] == matriz[1, 1] && matriz[1, 1] == matriz[1, 2]) ||
            (matriz[2, 0] == simbol && matriz[2, 0] == matriz[2, 1] && matriz[2, 1] == matriz[2, 2]) ||
            (matriz[0, 0] == simbol && matriz[0, 0] == matriz[1, 0] && matriz[1, 0] == matriz[2, 0]) ||
            (matriz[0, 1] == simbol && matriz[0, 1] == matriz[1, 1] && matriz[1, 1] == matriz[2, 1]) ||
            (matriz[0, 2] == simbol && matriz[0, 2] == matriz[1, 2] && matriz[1, 2] == matriz[2, 2]) ||
            (matriz[0, 0] == simbol && matriz[0, 0] == matriz[1, 1] && matriz[1, 1] == matriz[2, 2]) ||
            (matriz[0, 2] == simbol && matriz[0, 2] == matriz[1, 1] && matriz[1, 1] == matriz[2, 0]))
        {
            return true;
        }
        else
            return false;
    }
    // Metodo bool para buscar que el numero este dentro del rango de 1 al 9
    bool buscarEnRango(string mensaje)
    {   
        if (mensaje == "1" || mensaje == "2" || mensaje == "3" ||
            mensaje == "4" || mensaje == "5" || mensaje == "6" ||
            mensaje == "7" || mensaje == "8" || mensaje == "9")
        {
            return true;
        }
        else
            return false;
    }
    // Metodo bool para indicar que la posicion ya esta ocupada
    bool buscarPocicionVacia(string mensaje)
    {   
        if ((mensaje == "1" && (matriz[0, 0] == " X " || matriz[0, 0] == " O ")) ||
            (mensaje == "2" && (matriz[0, 1] == " X " || matriz[0, 1] == " O ")) ||
            (mensaje == "3" && (matriz[0, 2] == " X " || matriz[0, 2] == " O ")) ||
            (mensaje == "4" && (matriz[1, 0] == " X " || matriz[1, 0] == " O ")) ||
            (mensaje == "5" && (matriz[1, 1] == " X " || matriz[1, 1] == " O ")) ||
            (mensaje == "6" && (matriz[1, 2] == " X " || matriz[1, 2] == " O ")) ||
            (mensaje == "7" && (matriz[2, 0] == " X " || matriz[2, 0] == " O ")) ||
            (mensaje == "8" && (matriz[2, 1] == " X " || matriz[2, 1] == " O ")) ||
            (mensaje == "9" && (matriz[2, 2] == " X " || matriz[2, 2] == " O ")))
        {
            return true;
        }
        else
            return false;
    }
    //Jugar Persona vs. Persona  // pantallaActiva = pantalla.Jugar el mensaje se envia a este metodo
    void Jugar(string mensaje)
    {
        {
            if (buscarEnRango(mensaje))
            {
                if (buscarPocicionVacia(mensaje))
                {
                    Terminal.EscribirLinea("Digite otra posicion, esta ocupado! ");
                }
                else
                {   // turnos impares jugara X, turnos pares jugara O
                    if (turno % 2 == 1) // el turno es impar, al ser impar por el residuo, jugara x
                    {   //llamada a los metodos
                        escojerPosicion();
                        opcionesMarcar(mensaje);
                        turno++;
                        repetitivo();
                        
                        if (buscarGanador(" X "))
                        {
                            pantallaActiva = pantalla.Termino;  // el mensaje se envia a este metodo termino()
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("Felicidades, gano el jugador 1 con el simbolo X !!! ");
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("                               (｡◕‿◕｡) ");
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                        }
                        // Condicional en caso de empate al parsarse de 9 turnos
                        else if (turno == 10)
                        {
                            pantallaActiva = pantalla.Termino;  // el mensaje se envia a este metodo termino()
                            Terminal.EscribirLinea("\t\t\t\t\t\tEmpate !!! ");
                            Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                        }
                        else
                        {
                            Terminal.EscribirLinea("Turno del Jugardor 2 -> con el simbolo O");
                            Terminal.EscribirLinea("Digite la posicion ");
                        }
                    }
                    else
                    {   // turnos impares jugara X, turnos pares jugara O
                        if (turno % 2 == 0) // como el turno es par, al ser par por el residuo, jugara O
                        {
                            escojerPosicion();
                            opcionesMarcar(mensaje);
                            turno++;
                            repetitivo();
                            
                            if (buscarGanador(" O "))
                            {
                                pantallaActiva = pantalla.Termino;
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("Felicidades, gano el jugador 2 con el Simbolo O !!! ");
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("                               (｡◕‿◕｡) ");
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                            }
                            // Compara si se pasaron mas de 9 turnos en la matriz de 3x3
                            else if (turno == 10)
                            {
                                pantallaActiva = pantalla.Termino;
                                Terminal.EscribirLinea("\t\t\t\t\t\tEmpate !!! ");
                                Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                            }
                            else
                            {
                                Terminal.EscribirLinea("Turno del Jugardor 1 -> con el simbolo X");
                                Terminal.EscribirLinea("Digite la posicion ");
                            }

                        }
                    }
                }
            }
            else
                Terminal.EscribirLinea("Digite una posicion, entre el rango del 1 al 9 ");
        }
    }
    //Jugar Persona vs. Maquina
    void JugarMaquina(string mensaje)
    {
        if (buscarEnRango(mensaje))
            {   
                if (buscarPocicionVacia(mensaje))
                {
                    if (turno % 2 == 1)
                    {
                        Terminal.EscribirLinea("Digite otra posicion, esta ocupado ");
                    }
                    else if (turno % 2 == 0)    //si la posicion esta ocupada (la maquina) vuelve al metodo con otro numero hasta encontrar una vacia
                    {
                        JugarMaquina(posicion[Random.Range(0, posicion.Length)]);
                    }
                }
                else
                {   // turnos impares jugara X, turnos pares jugara O
                    if (turno % 2 == 1) // el turno es impar, al ser impar por el residuo, jugara x
                    {   
                        escojerPosicion();
                        opcionesMarcar(mensaje);
                        turno++;
                        repetitivo();
                        
                        if (buscarGanador(" X "))
                        {
                            pantallaActiva = pantalla.Termino;  // el mensaje se envia a este metodo termino()
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("Felicidades, gano a la persona, con el simbolo X !!! ");
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("                               (｡◕‿◕｡) ");
                            Terminal.EscribirLinea("");
                            Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                        }
                        // Condicional en caso de empate al parsarse de 9 turnos
                        else if (turno == 10)
                        {
                            pantallaActiva = pantalla.Termino;  // el mensaje se envia a este metodo termino()
                            Terminal.EscribirLinea("\t\t\t\t\t\tEmpate !!! ");
                            Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                        }
                        else
                            Terminal.EscribirLinea("Turno de la mauquina -> con el simbolo O (°Intro)");
                    }
                    else
                    {   // turnos impares jugara X, turnos pares jugara O
                        if (turno % 2 == 0) // como el turno es par, al ser par por el residuo, jugara O
                        {
                            escojerPosicion();
                            opcionesMarcar(mensaje);
                            turno++;
                            repetitivo();
                            
                            if (buscarGanador(" O "))
                            {
                                pantallaActiva = pantalla.Termino;
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("Lastima, gano la maquina con el Simbolo 0 !!! ");
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("                               :-( ");
                                Terminal.EscribirLinea("");
                                Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                            }
                            // Compara si se pasaron mas de 9 turnos en la matriz de 3x3
                            else if (turno == 10)
                            {
                                pantallaActiva = pantalla.Termino;
                                Terminal.EscribirLinea("\t\t\t\t\t\tEmpate !!! ");
                                Terminal.EscribirLinea("Para volver al menu Principal presiona Enter ");
                            }
                            else
                            {
                                Terminal.EscribirLinea("Turno de la persona -> con el simbolo X");
                                Terminal.EscribirLinea("Digite la posicion ");
                            }
                        }
                    }
                }
            }
            else
                Terminal.EscribirLinea("Digite una posicion, entre el rango del 1 al 9 ");    
    }
    //PantallaActiva = Pantalla.Termino el mensaje se envia a este metodo
    void Termino(string mensaje)
    {
        if (mensaje == "")
            borrarMostrarQuieresJugar();
        else
            borrarMostrarQuieresJugar();
    }
    void borrarMostrarQuieresJugar()
    {
        Terminal.BorrarPantalla();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                matriz[i, j] = "    ";
            
        turno = 1;
        pantallaActiva = pantalla.Start;
        Start();
    }
}