
using System; //Laberyntia
class Program
{
  private static int movimientos;

  public static int MOVIMIENTOS
  {get{return movimientos;} set{movimientos = value;}}

    static void Main()
    {
      Console.WriteLine("");
      Console.WriteLine("Bienvenidos al mundo de Laberinthya, un juego de laberinto tipo multijugador");
      Inicializar();
    }

    static void Inicializar()
    {
      Console.WriteLine("Elige una opción. 1(Iniciar Partida) 2(Ver tablero) 3(Ver Reglas) 4(Salir)");

      int opcion_inicial = Validar_int();
      opcion_inicial = While_int(opcion_inicial, 1, 4);

      if (opcion_inicial == 1)
      {
        int i = 0;
        int j = 0;
        Random random = new Random();
        int fil = random.Next(1, 30);
        int col = random.Next(1, 152);
        bool end = false;

        Player [] players = new Player [3];
        players = Crear_Jugadores(players);
        Board campo = new Board();
        
        
        for(i = 0; i < players.Length; i++)
        {
          while (campo.TABLERO[fil][col] != '.')
          {
            fil = random.Next(1, 30);
            col = random.Next(1, 152);
          }
          campo.TABLERO[fil][col] = players[i].LETRA;
          players[i].FIL = fil;
          players[i].COL = col;
        }

        Mostrar_Tablero(campo.TABLERO);

        while(end == false)
        {
          for( i = 0; i < players.Length; i++)
          { 
            if (end == false)
           {  
            Program.MOVIMIENTOS = players[i].VELOCIDAD;
            Console.WriteLine($"Es el turno de {players[i].NOMBRE}");           

            while( Program.MOVIMIENTOS > 0 && end == false)
            {
              Console.WriteLine($"{players[i].NOMBRE} te quedan {Program.MOVIMIENTOS} movimientos");  

              char accion = Validar_char();
              while(accion != 'w'&& accion != 's'&& accion != 'a'&& accion != 'd'&& accion != 'e'&& accion != 'q'&& accion != 'l')
              {accion = Validar_char();}

              if(accion == 'e')
              {campo.TABLERO = Actions.Usar_Habilidades(players[i], campo.TABLERO);}

              else if(accion == 'q')
              {Actions.Ver_Habilidades(); Program.MOVIMIENTOS ++;}

              else if(accion == 'l')
              {
                Console.WriteLine("Ha salido del juego. Hasta la próxima partida");
                end = true;
              }

              else if (accion == 'w'|| accion == 's'|| accion == 'a'|| accion =='d')
              {campo.TABLERO = Actions.Caminar(players[i], campo.TABLERO, accion);}

              if (end == false )
              {
                Program.MOVIMIENTOS--;
                Console.ReadLine();
                Console.Clear();
                Mostrar_Tablero(campo.TABLERO);
              }

              for(j = 0; j < players.Length; j++)
              {
                if(players[j].LLAVES == 3)
                {
                  Console.WriteLine("");
                  Console.WriteLine($"Felicidades, {players[i].NOMBRE} ha ganado, hasta la proxima partida.");
                  end = true;
                }
              }
            }

            for (int k = 0; k < 3; k++)
            {
              players[i].OPCIONES[k]--;

              if (players[i].OPCIONES[k] < 0)
              {players[i].OPCIONES[k] = 0;}
            }
           }
          }
        }
      }

      else if (opcion_inicial == 2)
      {
        Board campo = new Board();
        Mostrar_Tablero(campo.TABLERO);
        Console.WriteLine("¿Desea hacer algo más?");
        Console.WriteLine("");
        Inicializar();
      }
      else if (opcion_inicial == 3)
      {
      Console.WriteLine("");
      Console.WriteLine("El objetivo del juego es conseguir 3 de las 5 llaves dispersas por el mapa antes de que tu rival lo haga");
      Console.WriteLine("El jugador 1 juega con la letra (x), el 2 con la (o) y el 3, si existe, con la (z)");
      Console.WriteLine("Los lugares del mapa marcados con (#) se consideran muros y no pueden traspasarse o destruirse");
      Console.WriteLine("Los lugares del mapa marcados con (@) se consideran cofres. Estos guardan tanto bonificaciones como trampas, tendrás que arriesgarte para descubrirlo");
      Console.WriteLine("Para moverte por el mapa presiona [w, s, a, d] dependiendo de a donde quieras ir [arriba, abajo, izquierda, derecha], luego presiona [ENTER] dos veces");
      Console.WriteLine("");
      Console.WriteLine("Al elegir tu empleo, eliges tu velocidad inicial (la cantidad de espacios a los que puedes moverte en tu turno) y tu habilidad especial");
      Console.WriteLine("Además de tu habilidad especial, puedes escoger otras dos habilidades distintas"); 
      Console.WriteLine("Ya dentro del juego, puedes consultar nuevamente el funcionamiento de todas las habilidades pulsando (q). Para usar las habilidades pulsa (e)");
      Console.WriteLine("Cada habilidad tiene un tiempo de enfriamiento, o sea, una cantidad de turnos necesarios para activarla nuevamente");
      Console.WriteLine("");
      Console.WriteLine("Para que pase un turno ambos jugadores deben jugar");
      Console.WriteLine("Si deseas salir mientras estás jugando puedes presionar (l)");
      Console.WriteLine("Tu velocidad nunca será menor a 1, así que siempre podrás seguir jugando");
      Console.WriteLine("");
      Inicializar();
      }
      else 
      {
        Console.WriteLine("Hasta la próxima partida");
      }
    }

    static Player [] Crear_Jugadores(Player [] entrada)
    {
      int opcion = 0;
      do
      {
        Console.WriteLine("");
        Console.WriteLine("Bien, empecemos.");
        Console.WriteLine("Lo primero que debemos hacer es escribir la cantidad de jugadores que nos acompañan (2-3)");
        int jugadores = Validar_int();
        jugadores = While_int(jugadores, 2, 3);

        Console.WriteLine("");
        Console.WriteLine($"Muy bien, serán {jugadores} jugadores entonces. Ahora vamos a personalizar a cada jugador");
        Player [] players = new Player [jugadores];

        for (int i = 0; i < jugadores; i++)
        {
          Console.WriteLine("");
          Console.WriteLine($"Jugador {i + 1}:");
          players[i] = new Player();

          if (i == 0)
          {
            
            players[i].LETRA = 'x';
          }
          else if (i == 1)
          {
            
            players[i].LETRA = 'o';
          }
          else if (i == 2)
          {
            
            players[i].LETRA = 'z';
          }


          Console.WriteLine("");
          Console.WriteLine($"Los datos del jugador {i + 1} son los siguientes:");
          players[i].Info();
        }

        entrada = players;
            
        Console.WriteLine("");
        Console.WriteLine("Todo listo, presiona 1(Comenzar) o 2(Volver al inicio)");
        opcion = Validar_int();
        opcion = While_int(opcion, 1, 2);

      } while(opcion == 2);

      return entrada;
    }

    public static int While_int(int entrada, int min, int max)
    {
      while(entrada < min || entrada > max)
      {
        Console.WriteLine("Numero incorrecto. Intenta otra vez");
        entrada = Validar_int();
      }
      return entrada;
    }
    public static int Validar_int()
    {
      int i = 0;
      try 
      {
        i = int.Parse(Console.ReadLine()!);
      }
      catch(Exception )
      {
        return Validar_int();
      }
      return i;
    }
    public static string Validar_string()
    {
        int j = 0;
        string i = Console.ReadLine()!;
        try
        {
          j = int.Parse(i);
        }
        catch(Exception )
        { 
          return i;
        }
        Console.WriteLine("La opción elegida debe ser una cadena de caracteres o un caracter. Intenta otra vez");
        return Validar_string();
    }
    public static char Validar_char()
    {
      char j = 'q';
        try
        {
          j = char.Parse(Console.ReadLine()!);
        }
        catch(Exception )
        {
        Console.WriteLine("La opción elegida debe ser un caracter. Intenta otra vez");
          return Validar_char();
        }
        return j;
    }
  static void Mostrar_Tablero(char [][] entrada)
  {
    
    int fil = 0;
    int col = 0;

    for(fil = 0; fil < 30; fil++)
    {
      for(col = 0; col < 152; col++)
      {
        Console.Write(entrada[fil][col]);
      }
      Console.WriteLine("");
    }
  }
}

class Player
{
    // Atributos
    private  string nombre;
    private  string empleo;
    private  int velocidad;
    private  string habilidad_1;
    private  string habilidad_2;
    private  string habilidad_Especial;
    private char letra;
    private int veneno;
    private int fil;
    private int col;
    private int llaves;
    private int [] opciones = new int [3];

    //Propiedades
    public string NOMBRE {get {return nombre;} set {nombre = value;}}
    public int VENENO {get {return veneno;} set {veneno = value;}}
    public string EMPLEO {get {return empleo;} set {empleo = value;}}
    public int VELOCIDAD {get {return velocidad;} set {velocidad = value;}}
    public string HABILIDAD_1 {get {return habilidad_1;} set {habilidad_1 = value;}}
    public string HABILIDAD_2 {get {return habilidad_2;} set {habilidad_2 = value;}}
    public string HABILIDAD_ESPECIAL {get {return habilidad_Especial;} set {habilidad_Especial = value;}}
    public char LETRA {get {return letra;} set {letra = value;}}
    public int FIL {get {return fil;} set {fil = value;}}
    public int COL {get {return col;} set {col = value;}}    
    public int LLAVES {get {return llaves;} set {llaves = value;}}
    public int [] OPCIONES {get{return opciones;} set{ opciones = value;}}
    
    // Constructor
    public Player()
    {
      Dar_nombre();
      Dar_empleo();
      Dar_habilidades();
    }  

    //Metodos
   public void Dar_nombre()
   {
    Console.WriteLine("Escribe el nombre de este jugador");
    Console.WriteLine("");
    nombre = Program.Validar_string()!;
   }
   public void Dar_empleo( )
   {
       Console.WriteLine("Ahora elija su empleo");
       Console.WriteLine("");
       Console.WriteLine("1 Explorador (velocidad 10)");
       Console.WriteLine("(Descubrimiento): Convierte un cofre cercano en una mejora de 3 de velocidad o regresa tus puntos de veneno a 0  (Enfriamiento 2 turnos)"); 

       Console.WriteLine("2 Buscatesoros (velocidad 9)"); 
       Console.WriteLine("Habilidad especial (Tesoro): Convierte un cofre en una bonificacion de velocidad 5                               (Enfriamiento 3 turnos)");

       Console.WriteLine("3 Minero (velocidad 9)");
       Console.WriteLine("Habilidad especial (Derrumbe): Destruye los cofres que esten cerca y aleja dos casillas a los jugadores cercanos (Enfriamiento 4 turnos)");

       Console.WriteLine("4 Trotamundos (velocidad 8)");
       Console.WriteLine("Habilidad especial (Bebida energetica) Convierte un cofre cercano en una bonificacion permanente de velocidad 1  (Enfriamiento 0 turnos)");

       Console.WriteLine("5 Habitante (velocidad 9)");
       Console.WriteLine("Habilidad especial (Imitar): Imita de manera aleatoria una habilidad de las antes mencionadas                    (Enfriamiento 3 turnos)");

       Console.WriteLine("");
       Console.WriteLine("Empleo:");

       int opcion = Program.Validar_int()!;
       opcion = Program.While_int(opcion, 1, 5);

       switch (opcion)
       {
            case 1: empleo = "Explorador";   velocidad = 10; habilidad_Especial = "Descubrimiento";   break;
            case 2: empleo = "Buscatesoros"; velocidad = 9; habilidad_Especial = "Tesoro";            break;
            case 3: empleo = "Minero";       velocidad = 9; habilidad_Especial = "Derrumbe";          break;
            case 4: empleo = "Habitante";    velocidad = 9; habilidad_Especial = "Imitar";            break;
            case 5: empleo = "Trotamundos";  velocidad = 8; habilidad_Especial = "Bebida energetica"; break;                
       }
   }

   public void Dar_habilidades()
   {
     //Habilidades
      Console.WriteLine ("Por ultimo, elige dos habilidades distintas de entre las siguientes ");
      Console.WriteLine("");
      Console.WriteLine ("1 Carrera (Aumenta tu velocidad en 3 este turno)                                                              (Enfriamiento 2 turnos)");
      Console.WriteLine ("2 Respaldo (Restaura el Enfriamiento de tu Habilidad Especial)                                                (Enfriamiento 4 turnos)");
      Console.WriteLine ("3 Precauciones (Destruye un cofre cercano en la posicion que elijas [w, a, s, d] y aumenta en 2 tu velocidad) (Enfriamiento 2 turnos)");
      Console.WriteLine ("4 Descanso (Te devuelve a tu velocidad original)                                                              (Enfriamiento 2 turnos)");
      Console.WriteLine("");

        int opcion1 = Program.Validar_int()!;
        int opcion2 = Program.Validar_int()!;

        while (opcion1 < 1|| opcion1 > 4|| opcion2 < 1|| opcion2 > 4 || opcion1 == opcion2)
        {
          Console.WriteLine("Habilidades incorrectas, elige nuevamente");

          opcion1 = Program.Validar_int()!;
          opcion2 = Program.Validar_int()!;
        }

        switch (opcion1)
        {
          case 1: habilidad_1 = "Carrera";      break;
          case 2: habilidad_1 = "Descanso";     break;
          case 3: habilidad_1 = "Precauciones"; break;
          case 4: habilidad_1 = "Respaldo";     break;
        }
        switch (opcion2)
        {
          case 1: habilidad_2 = "Carrera";      break;
          case 2: habilidad_2 = "Descanso";     break;
          case 3: habilidad_2 = "Precauciones"; break;
          case 4: habilidad_2 = "Respaldo";     break;
        }
    }

   public void Info()
   {
    Console.WriteLine($"Nombre: {NOMBRE}");
    Console.WriteLine($"Empleo: {EMPLEO}");
    Console.WriteLine($"Velocidad: {VELOCIDAD.ToString()}");
    Console.WriteLine($"Habilidad 1: {HABILIDAD_1}");
    Console.WriteLine($"Habilidad 2: {HABILIDAD_2}");
    Console.WriteLine($"Habilidad Especial: {HABILIDAD_ESPECIAL}");
    Console.WriteLine($"Letra: {LETRA}");
   } 
}

class Board
{
  private  char [][] tablero;

  public char [] [] TABLERO
  {get {return tablero;} set{tablero = value;}}

  public Board()
  {
    TABLERO = new char [30][];
    for (int i = 0; i < 30; i++)
    {
      TABLERO[i] = new char [152];
    }
    TABLERO = Inicializar_Tablero(TABLERO);
  }

  public char [][] Inicializar_Tablero(char [][] entrada)
  {
    int fil = 0;
    int col = 0;

    //Muros exteriores
    for( fil = 0; fil < 30; fil++)
    {
      for( col = 0; col < 152; col++)
      {
        entrada[0][col] = '#';
        entrada[29][col] = '#';

        entrada[fil][0] = '#';
        entrada[fil][151] = '#';
      }
    }

    //Muros interiores
    for ( fil = 0; fil < 30; fil++)
    {
      entrada[fil][75] = '#';
      entrada[fil][37] = '#';
      entrada[fil][113]= '#';
    }  
    for( col = 0; col < 152; col ++)
    {
      entrada[14][col] = '#';
    }
    
    //Puertas verticales
    Random random = new Random();
    int posibilidad = random.Next(1, 10);
    
    for(fil = 1; fil < 29; fil ++)
    {
      for (col = 1; col < 151; col ++)
      {
        if( entrada [fil][col] == '#' && posibilidad == 7)
        {
          entrada[fil][col] = '.';
        }
        random = new Random();
        posibilidad = random.Next(1,10);
      }
    } 

    //Puertas horizontales
    entrada = Fila_14(entrada); 

    //Caminos 
    for(fil = 0; fil < 30; fil++)
    {
      for(col = 0; col < 152; col++)
      {
        if(entrada[fil][col] != '#')
        {
          entrada[fil][col] = '.';
        }
      }
    }  

    //Estructuras
    entrada = Estructuras(entrada);

    //Constantes
    entrada[14][37] = '#';
    entrada[14][75] = '#';
    entrada[14][113]= '#';

    entrada[10][37] = '.';
    entrada[11][75] = '.';
    entrada[12][113] ='.';

    entrada[25][37] = '.';
    entrada[24][75] = '.';
    entrada[28][113] ='.';

    return entrada;
  }

  public char [][] Fila_14(char [][] entrada_es)
  {
    Random dado = new Random();
    int cortes = dado.Next(1, 4);
    int puertas = dado.Next(2, 6);
    int i = 0;
    int j = 0;

    for (i = 1; i < 37; i++)
    {
      j = 1;
      entrada_es [14][i] = Cortes(cortes, puertas, i, j);
      puertas = dado.Next(2, 6);
    }

    dado = new Random(); 
    cortes = dado.Next(1, 4);
    puertas = dado.Next(2, 6);
    for (i = 37; i < 75; i++)
    {
      j = 37;
      entrada_es [14][i] = Cortes(cortes, puertas, i, j);
    }

    dado = new Random(); 
    cortes = dado.Next( 1, 4);
    puertas = dado.Next(2, 6);
    for (i = 75; i < 113; i++)
    {
      j = 75;
      entrada_es [14][i] = Cortes(cortes, puertas, i, j);
    }

    dado = new Random(); 
    cortes = dado.Next(1, 4);
    for (i = 113; i < 151; i++)
    {
      j = 113;
      entrada_es [14][i] = Cortes(cortes, puertas, i, j);
      puertas = dado.Next(2, 6);
    }

    return entrada_es;
  }

  public char Cortes(int cortes_es, int puertas_es, int i_es, int j_es)
  {

    if(cortes_es == 1 && i_es > j_es + 14 && i_es <= j_es + 14 + puertas_es)
    {
      return '.';
    }

    else if(cortes_es == 2 && i_es > j_es + 3 && i_es <= j_es + 3 + puertas_es || i_es > j_es + 26 && i_es <= j_es + 26 + puertas_es)
    {
      return '.';
    }   

    else if(cortes_es == 3 && i_es > j_es + 3 && i_es <= j_es + 3 + puertas_es || i_es > j_es + 26 && i_es <= j_es + 26 + puertas_es || i_es > j_es + 14 && i_es <= j_es + 14 + puertas_es)
    {
      return '.';
    }
    else return '#';
  }

  public char [][] Estructuras(char [][] entrada_es)
  {
    int col_i = 1;
    int col_s = 37;
    Random dado = new Random();
    int tiro = dado.Next (1,6);

    int [] tipo1 = {1, 2, 3, 4, 5, 6, 7, 8};
    int [] tipo2 = {2, 4, 6, 8, 1, 3, 5, 7};
    int [] tipo3 = {8, 7, 6, 5, 4, 3, 2, 1};
    int [] tipo4 = {1, 3, 5, 7, 2, 4, 6, 8};
    int [] tipo5 = {5, 8, 4, 1, 2, 7, 3, 6};

    int [] tipo = new int [8];

    if (tiro == 1) {tipo = tipo1;}
    else if (tiro == 2) {tipo = tipo2;}
    else if (tiro == 3) {tipo = tipo3;}
    else if (tiro == 4) {tipo = tipo4;}
    else if (tiro == 5) {tipo = tipo5;}


    for (int i = 0; i < 4; i++)
    {
      int j = tipo [i];
      for (int fil = 1; fil < 14; fil ++)
      {
        for( int col = col_i; col < col_s; col ++)
        {
          entrada_es [fil][col] = Generar_Estructuras (j, fil, col, col_s);
        }
      }
      
      j = tipo [i+4];
      for (int fil = 15; fil < 29; fil ++)
      {
        for( int col = col_i; col < col_s; col ++)
        {
          entrada_es [fil][col] = Generar_Estructuras (j, fil, col, col_s);
        }
      }
      col_i = col_i + 38;
      col_s = col_s + 38;
    }
    return entrada_es;
  }

  char Generar_Estructuras(int j_es, int fil_es, int col_es, int col_s_es)
  {
    if (j_es == 1 || j_es == 2 || j_es == 3)
    {
      if ( fil_es > 1 && fil_es < 5 || fil_es > 15 && fil_es < 19 || fil_es > 9 && fil_es <= 12 || fil_es > 24 && fil_es <= 27)
      {
        if ( col_es > col_s_es - 34 && col_es < col_s_es - 30 || col_es > col_s_es - 7 && col_es < col_s_es - 3)
        {
          return '#';
        }
      }

      if ( fil_es >= 5 && fil_es < 9 || fil_es > 19 && fil_es < 22)
      {
        if (col_es > col_s_es - 28 && col_s_es < col_s_es - 5)
        {
          return '@';
        }
      }

      if (fil_es == 3 || fil_es == 11 || fil_es == 17 || fil_es == 26)
      {
        if ( col_es > col_s_es - 31 && col_es < col_s_es - 6)
        {
          return '@';
        }
      }

      if ( fil_es >= 5 && fil_es <= 9 || fil_es >= 19 && fil_es <= 24)
      {
        if( col_es == col_s_es - 32 || col_es == col_s_es - 5)
        {
          return '@'; 
        }
      }

      if (fil_es > 5 && fil_es < 9 && fil_es != 7 || fil_es > 19 && fil_es < 23 && fil_es != 21 )
      {
        if(col_es > col_s_es - 21 && col_es < col_s_es - 14)
        {
          return '@';
        }
      } 

      if (fil_es == 7 || fil_es == 21  )
      {
        if (col_es == col_s_es - 18)
        {return '!';}
      }
    }

    else if (j_es == 4 || j_es == 5)
    {
      if (fil_es > 1 && fil_es < 4 || fil_es > 9 && fil_es < 12 || fil_es > 5 && fil_es < 8 ||fil_es > 15 && fil_es < 18 || fil_es > 24 && fil_es < 27 || fil_es > 19 && fil_es < 22)
      {
        if (col_es > col_s_es - 33 && col_es < col_s_es - 22 || col_es > col_s_es - 15 && col_es < col_s_es - 4)
        {
          return '@';
        }
      }

      if( fil_es == 7 || fil_es == 21 )
      {
        if (col_es == col_s_es - 18)
        {
          return '!';
        }
      }

        else if (fil_es >= 4 && fil_es <= 5 || fil_es > 17 && fil_es < 20 )
        {
          if (col_es > col_s_es - 33 && col_es < col_s_es - 22  )
          {
            return '#';
          }
        }

        else if ( fil_es > 6 && fil_es <= 9|| fil_es > 21 && fil_es < 25)
        {
          if( col_es > col_s_es - 15 && col_es < col_s_es - 5)
          {return '#';}
        }

        if (fil_es > 1 && fil_es < 4 || fil_es > 9 && fil_es < 12 || fil_es > 15 && fil_es < 18 || fil_es > 24 && fil_es < 27 )
        {
          if ( col_es >= col_s_es - 22 && col_es <= col_s_es - 15)
          {
            return '#';
          }
        }
    }

    else if (j_es == 6 )
    {
      if ( (col_es  + fil_es) % 5 == 0 )
      {return '@';}
    }
    else if (j_es == 7)
    {
      if (fil_es == 7 || fil_es == 22)
      {
        if(col_es == col_s_es - 19)
        {
          return '@';
        }
      }

      if (fil_es > 1 && fil_es < 14 || fil_es > 14 && fil_es < 29 )   
      {
        if(col_es > col_s_es - 20 && col_es < col_s_es - 18)
        {
          return '.';
        }
      }

      if ( fil_es == 7 || fil_es == 22 )
      {
        return '.';
      }

      if( fil_es > 4 && fil_es < 10 || fil_es > 18 && fil_es < 25)
      {
        if (col_es > col_s_es - 26 && col_es < col_s_es - 12 )
        {
          return '#';
        }
      }

      if (fil_es > 1 &&  fil_es < 5|| fil_es > 9 && fil_es < 13 || fil_es > 16 && fil_es < 20 || fil_es > 24 && fil_es < 28)
      {
        if( col_es > col_s_es - 34 && col_es < col_s_es - 5)
        {
          return '#';
        }
      }
    }

    else return '.';
    return '.';
 }
}

class Actions
{
  public static char [][] Caminar( Player jugador_es, char [][] tablero_es, char entrada)
  {
    int fil = jugador_es.FIL;
    int col = jugador_es.COL;
    char temp = tablero_es[fil][col];

    if(entrada == 'w')
    {
      if(tablero_es[fil-1][col] == '.')
      {tablero_es[fil-1][col] = jugador_es.LETRA;
      tablero_es[fil][col] = '.';
      jugador_es.FIL --;}

      else if (tablero_es[fil-1][col] == '@')
      {tablero_es[fil-1][col] = jugador_es.LETRA; 
      tablero_es [fil][col] = '.';
      Cofres_Y_Trampas(jugador_es);
      jugador_es.FIL --;
      }

      else if (tablero_es[fil-1][col] =='!')
      {tablero_es[fil-1][col] = jugador_es.LETRA;
      tablero_es[fil][col] = '.';
      jugador_es.FIL--;
      jugador_es.LLAVES++;
      if(jugador_es.LLAVES == 1)
      {
        Console.WriteLine($"Genial, has conseguido una llave. Aún te faltan {3 - jugador_es.LLAVES} más");
      }
      else if(jugador_es.LLAVES == 2)
      {
        Console.WriteLine($"Venga {jugador_es.NOMBRE}, ya solo te falta {3 - jugador_es.LLAVES}");
      }
      }

      else if (tablero_es[fil-1][col] == '#')
      {Console.WriteLine("Ha chocado con una pared");}
    }

    else if(entrada == 's')
    {

      if(tablero_es[fil+1][col] == '.')
      {tablero_es[fil+1][col] = jugador_es.LETRA;
        tablero_es[fil][col] = '.';
        jugador_es.FIL ++;
      }

      else if (tablero_es[fil+1][col] == '@')
      {tablero_es [fil+1][col] = jugador_es.LETRA;
        tablero_es[fil][col] = '.'; 
      jugador_es.FIL++;
      Cofres_Y_Trampas(jugador_es);
      }

      else if (tablero_es[fil+1][col] =='!')
      {tablero_es[fil+1][col] = jugador_es.LETRA;
      tablero_es[fil][col] = '.';
      jugador_es.FIL++;
      jugador_es.LLAVES++;
      if(jugador_es.LLAVES == 1)
      {
        Console.WriteLine($"Genial, has conseguido una llave. Aún te faltan {3 - jugador_es.LLAVES} más");
      }
      else if(jugador_es.LLAVES == 2)
      {
        Console.WriteLine($"Venga {jugador_es.NOMBRE}, ya solo te falta {3 - jugador_es.LLAVES}");
      }
      }

      else if (tablero_es[fil+1][col] == '#') 
      {Console.WriteLine("Ha chocado con una pared");}
    }

    else if(entrada == 'a')
    {

      if(tablero_es[fil][col-1] == '.')
      {tablero_es[fil][col-1] = jugador_es.LETRA;
        tablero_es[fil][col] = '.';
        jugador_es.COL--;
      }

      else if (tablero_es[fil][col-1] == '@')
      {tablero_es [fil][col-1] = jugador_es.LETRA;
        tablero_es[fil][col] = '.'; 
      jugador_es.COL--;
      Cofres_Y_Trampas(jugador_es);
      }

      else if (tablero_es[fil][col-1] =='!')
      {tablero_es[fil][col-1] = jugador_es.LETRA;
      tablero_es[fil][col] = '.';
      jugador_es.COL--;
      jugador_es.LLAVES++;
      if(jugador_es.LLAVES == 1)
      {
        Console.WriteLine($"Genial, has conseguido una llave. Aún te faltan {3 - jugador_es.LLAVES} más");
      }
      else if(jugador_es.LLAVES == 2)
      {
        Console.WriteLine($"Venga {jugador_es.NOMBRE}, ya solo te falta {3 - jugador_es.LLAVES}");
      }
      }

      else if (tablero_es[fil][col--] == '#')
      {Console.WriteLine("Ha chocado con una pared");}
    }

    else if (entrada == 'd')
    {

      if(tablero_es[fil][col+1] == '.')
      {tablero_es[fil][col+1] = jugador_es.LETRA;
        tablero_es[fil][col] = '.';
        jugador_es.COL++;
      }

      else if (tablero_es[fil][col+1] == '@')
      {tablero_es [fil][col+1] = jugador_es.LETRA;
       tablero_es[fil][col] = '.'; 
      jugador_es.COL++;
      Cofres_Y_Trampas(jugador_es);
      }

      else if (tablero_es[fil][col+1] =='!')
      {tablero_es[fil][col+1] = jugador_es.LETRA;
      tablero_es[fil][col] = '.';
      jugador_es.COL++;
      jugador_es.LLAVES++;
      if(jugador_es.LLAVES == 1)
      {
        Console.WriteLine($"Genial, has conseguido una llave. Aún te faltan {3 - jugador_es.LLAVES} más");
      }
      else if(jugador_es.LLAVES == 2)
      {
        Console.WriteLine($"Venga {jugador_es.NOMBRE}, ya solo te falta {3 - jugador_es.LLAVES}");
      }
      }

      else if (tablero_es[fil][col+1] == '#')
      {Console.WriteLine("Ha chocado con una pared");}
    }        

    return tablero_es;
  }

  public static char [][] Usar_Habilidades(Player jugador, char [][] tablero)
  {
    Console.WriteLine("¿Que habilidad quieres usar?");
    Console.WriteLine($"1 ({jugador.HABILIDAD_1})  2({jugador.HABILIDAD_2})  3({jugador.HABILIDAD_ESPECIAL}) 4(salir) )");

    int opcion = Program.Validar_int();
    opcion = Program.While_int(opcion, 1, 4);

    if(opcion == 1)
    {tablero = Habilidades(jugador.HABILIDAD_1,        jugador, tablero, opcion);}

    else if(opcion == 2)
    {tablero = Habilidades(jugador.HABILIDAD_2,        jugador, tablero, opcion);}

    else if(opcion == 3)
    {tablero = Habilidades(jugador.HABILIDAD_ESPECIAL, jugador, tablero, opcion);}

    else if (opcion == 4)
    {Program.MOVIMIENTOS++;}

  return tablero;
  }

  public static char [][] Habilidades(string entrada, Player jugador_es, char [][] tablero_es, int opcion_es)
  {
    int fil = jugador_es.FIL;
    int col = jugador_es.COL;

    if (jugador_es.OPCIONES[opcion_es - 1] > 0)
    {
      Program.MOVIMIENTOS++;
      Console.WriteLine($"Quedan {jugador_es.OPCIONES[opcion_es - 1]} turnos para usar esta habilidad.");
    }
    else
    {
      if ( entrada == "Carrera")
      {
        Program.MOVIMIENTOS += 3;
        jugador_es.OPCIONES[opcion_es - 1] = 3;
      }

      else if (entrada == "Descanso")
      {
        if(jugador_es.EMPLEO == "Explorador")
        {
          jugador_es.VELOCIDAD = 10;
        }
        else if(jugador_es.EMPLEO == "Trotamundos")
        {
          jugador_es.VELOCIDAD = 8;
        }
        else
        {
          jugador_es.VELOCIDAD = 9;
        }
        jugador_es.OPCIONES[opcion_es - 1] = 3;
      }

      else if (entrada == "Respaldo")
      {
        jugador_es.OPCIONES[2] = 0;
        jugador_es.OPCIONES[opcion_es - 1] = 5;
      }

      else if (entrada == "Descubrimiento")
      {
        Random dado = new Random();
        int tiro = dado.Next(1,3);

        if (tablero_es[fil-1][col] == '@'|| tablero_es[fil+1][col] == '@'|| tablero_es[fil][col+1] == '@'|| tablero_es[fil][col-1] == '@')
        {
          if (tiro == 1)
          {
            jugador_es.VENENO = 0;
          }
          if (tiro == 2)
          {
            jugador_es.VELOCIDAD +=4;
          }
        }

        if (tablero_es[fil-1][col] == '@')
        {
          tablero_es[fil-1][col] = '.';
        }
        else if (tablero_es[fil+1][col] == '@')
        {
          tablero_es[fil+1][col] = '.';
        }
        else if (tablero_es[fil][col+1] == '@')
        {
          tablero_es[fil][col+1] = '.';
        }
        else if (tablero_es[fil][col-1] == '@')
        {
          tablero_es[fil][col-1] = '.';
        }

        jugador_es.OPCIONES[opcion_es - 1] = 3;
      }

      else if (entrada == "Tesoro")
      {
        if (tablero_es[fil-1][col] == '@'|| tablero_es[fil+1][col] == '@'|| tablero_es[fil][col+1] == '@'|| tablero_es[fil][col-1] == '@')
        {
          Program.MOVIMIENTOS +=6;
        }

        if (tablero_es[fil-1][col] == '@')
        {
          tablero_es[fil-1][col] = '.';
        }
        else if (tablero_es[fil+1][col] == '@')
        {
          tablero_es[fil+1][col] = '.';
        }
        else if (tablero_es[fil][col+1] == '@')
        {
          tablero_es[fil][col+1] = '.';
        }
        else if (tablero_es[fil][col-1] == '@')
        {
          tablero_es[fil][col-1] = '.';
        }

        jugador_es.OPCIONES[opcion_es - 1] = 4;
      }


      else if (entrada == "Derrumbe")
      {
        if (tablero_es[fil-1][col] == '@') 
        {
          tablero_es[fil-1][col] = '.';
        }
        if (tablero_es[fil+1][col] == '@')
        {
          tablero_es[fil+1][col] = '.';
        } 
        if (tablero_es[fil][col+1] == '@') 
        {
          tablero_es[fil][col+1] = '.';
        }
        if (tablero_es[fil][col-1] == '@')
        {
          tablero_es[fil][col-1] = '.';
        } 
        if (tablero_es[fil-1][col-1] == '@')
        {
          tablero_es [fil-1][col-1] = '.';
        } 
        if (tablero_es[fil+1][col+1] == '@')
        {
          tablero_es [fil+1][col+1] = '.';
        } 
        if (tablero_es[fil-1][col+1] == '@')
        {
          tablero_es[fil-1][col+1] = '.';
        } 
        if (tablero_es[fil+1][col-1] == '@')
        {
          tablero_es[fil+1][col-1] = '.';
        }

        jugador_es.OPCIONES[opcion_es - 1] = 4;
      }

      else if (entrada == "Bebida energetica")
      {
        if (tablero_es[fil-1][col] == '@'|| tablero_es[fil+1][col] == '@'|| tablero_es[fil][col+1] == '@'|| tablero_es[fil][col-1] == '@')
        {
          jugador_es.VELOCIDAD ++;
        }

        if (tablero_es[fil-1][col] == '@')
        {
          tablero_es[fil-1][col] = '.';
        }
        else if (tablero_es[fil+1][col] == '@')
        {
          tablero_es[fil+1][col] = '.';
        }
        else if (tablero_es[fil][col+1] == '@')
        {
          tablero_es[fil][col+1] = '.';
        }
        else if (tablero_es[fil][col-1] == '@')
        {
          tablero_es[fil][col-1] = '.';
        }

        jugador_es.OPCIONES[opcion_es - 1] = 1;
      }

      else if ( entrada == "Imitar")
      {
        string [] nombres = {"Carrera", "Respaldo", "Precauciones", "Descanso", "Descubrimiento", "Tesoro", "Derrumbe", "Bebida energetica"};
        Random dado = new Random();
        int tiro = dado.Next(1,9);

        Console.WriteLine($"La habilidad escogida fue {nombres[tiro-1]}");

        Habilidades(nombres[tiro-1], jugador_es, tablero_es, opcion_es);

        jugador_es.OPCIONES[opcion_es - 1] = 4;
      }

      else if (entrada == "Precauciones")
      {
        Console.WriteLine("Presionsa una de estas teclas [w, a, s, d] para elegir la dirección del cofre");
        char opcion = Program.Validar_char();
        while( opcion != 'w' && opcion != 's' && opcion != 'a' && opcion != 'd')
        {opcion = Program.Validar_char();}

        if(opcion == 'w')
        {
          if (tablero_es [fil-1][col] == '@')
          {
            tablero_es [fil-1][col] = '.';
            Program.MOVIMIENTOS+=3;
          }
        }

        if(opcion == 's')
        {
          if (tablero_es [fil+1][col] == '@')
          {
            tablero_es [fil+1][col] = '.';
            Program.MOVIMIENTOS+=3;
          }
        }

        if(opcion == 'a')
        {
          if (tablero_es [fil][col-1] == '@')
          {
            tablero_es [fil][col-1] = '.';
            Program.MOVIMIENTOS+=3;
          }
        }

        if(opcion == 'd')
        {
          if (tablero_es [fil][col+1] == '@')
          {
            tablero_es [fil][col+1] = '.';
            Program.MOVIMIENTOS+=3;
          }
        }    

        jugador_es.OPCIONES[opcion_es - 1] = 3;
      }
    }
return tablero_es;
  }
  public static void Ver_Habilidades( )
  {
    Console.WriteLine("");
    Console.WriteLine ("Carrera (Aumenta tu velocidad en 2 este turno)                                                                  (Enfriamiento 2 turnos)");
    Console.WriteLine ("Respaldo (Restaura el Enfriamiento de tu Habilidad Especial)                                                    (Enfriamiento 4 turnos)");
    Console.WriteLine ("Precauciones (Destruye un cofre cercano en la posicion que elijas [w, a, s, d] y aumenta en 2 tu velocidad)     (Enfriamiento 2 turnos)");
    Console.WriteLine ("Descanso: Te devuelve a tu velocidad original                                                                   (Enfriamiento 2 turnos)"); 
    Console.WriteLine ("");
    Console.WriteLine ("(Descubrimiento): Convierte un cofre cercano en una mejora de 3 de velocidad o regresa tus puntos de veneno a 0 (Enfriamiento 2 turnos)");
    Console.WriteLine ("(Tesoro): Convierte un cofre en una bonificacion de velocidad 5                                                 (Enfriamiento 3 turnos)");
    Console.WriteLine ("(Derrumbe): Destruye los cofres que esten cerca                                                                 (Enfriamiento 4 turnos)");
    Console.WriteLine ("(Bebida energetica) Convierte un cofre cercano en una bonificacion permanente de velocidad 1                    (Enfriamiento 1 turno)");
    Console.WriteLine ("(Imitar): Imita de manera aleatoria una habilidad de las antes mencionadas                                      (Enfriamiento 3 turnos)");

  
  } 
  static void Cofres_Y_Trampas(Player entrada)
  {
    Random dado = new Random();
    int tiro = dado.Next(1,22);

    if (tiro >= 1 && tiro <=5)
    {
      Console.WriteLine("Heridas Graves: Parece que era una trampa, tu velocidad ha disminuido en 1 de forma permanente");
      entrada.VELOCIDAD--;
    }
    else if (tiro >= 6 && tiro <= 10 )
    {
      entrada.VENENO++;
      Console.WriteLine($"Dardos venenosos: Tus puntos de veneno actuales son {entrada.VENENO}. Si llegan a 3, perderas 2 de velocidad de forma permanente");

      if(entrada.VENENO == 3)    {entrada.VELOCIDAD -= 2;}
      if(entrada.VENENO == 3)    {entrada.VENENO = 0;}
      if(entrada.VELOCIDAD <= 0) {entrada.VELOCIDAD = 1;}
    }
    else if (tiro >= 11 && tiro <= 15)
    {
      Console.WriteLine("Afortunado: Encontraste algo valioso. Tu velocidad ha aumentado en 1 de forma permanente");
      entrada.VELOCIDAD++;
    }
    else if (tiro >= 16 && tiro <= 20)
    {
      Console.WriteLine("Escena escalofriante: Parece que alguien intentó pasar por aquí y no le fue muy bien.");
    }
    else 
    {
      Console.WriteLine($"Pequeña sorpresa: Espera, esto es... UNA LLAVE");
      entrada.LLAVES++;
      Console.WriteLine($"Llaves conseguidas: {entrada.LLAVES}. Te faltan {3 - entrada.LLAVES} por conseguir");
    }
  }
}