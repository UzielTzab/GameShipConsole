using NAudio.Wave;
using Newtonsoft.Json;
using System.Data.SqlClient;
Console.Clear();
Console.ForegroundColor = ConsoleColor.Green;

VideoGame videoGame = new VideoGame();
DrawTitles drawTitles = new DrawTitles();

int windowWidth = Console.WindowWidth;
int WindowHeight = Console.WindowHeight;

bool hadPerfil = false;
bool IsMenu = true;                 
bool Level1Congratulations = false;
bool Level2Congratulations = false;
bool Level3Congratulations = false;
bool Level4Congratulations = false;


string musicToDetectLevel1 = @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel1-UT.wav";
string musicToDetectLevel2 = @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel2-UT.wav";
string musicToDetectLevel3 = @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel3-UT.wav";
string musicToDetectLevel4 = @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel4-UT.wav";

List<string> musicList = new List<string>
{
    @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Menu-UT.wav",
    @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel1-UT.wav",
    @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel2-UT.wav",
    @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel3-UT.wav",
    @"C:\\Users\\Usuario 1\\Downloads\\+CACIONES HECHAS POR MI\\Nivel4-UT.wav",
};
// Establece la pista actual en la primera pista de la lista
int currentTrack = 0;
// Crea un objeto WaveOutEvent para reproducir el sonido
WaveOutEvent waveOut = new WaveOutEvent();
// Establece el modo de reproducción en bucle
waveOut.PlaybackStopped += (sender, eventArgs) =>
{
    bool seADetenidoPasarAOtroNivel = true;
    if(musicList[currentTrack]==musicToDetectLevel1)
    {
        videoGame.isLevel1 = true;
        Level1Congratulations = true;
        
    }
    if(Level1Congratulations)
    {
        
        if(musicList[currentTrack]==musicToDetectLevel2)
        {
            videoGame.isLevel1 = true;
            Level2Congratulations = true;
        }
    }
    if(Level2Congratulations)
    {
        if(musicList[currentTrack]==musicToDetectLevel3)
        {
            videoGame.isLevel2 = true;
            Level3Congratulations = true;
        }
    }
    
    
    
    if (IsMenu)
    {
        currentTrack= 0;
        Level1Congratulations = false;
        Level2Congratulations = false;
        Level3Congratulations = false;
        Level4Congratulations = false;
    }
    else
    {
        if(Level4Congratulations)
        {
            videoGame.isLevel4 = true;
            currentTrack = 5;
        }
        else
        {
            if(Level3Congratulations)
            {
                videoGame.isLevel4 = true;
                currentTrack = 4;
            }
            else
            {
                if(Level2Congratulations)
                {
                    videoGame.isLevel3 = true;
                    currentTrack = 3;
                }
                else
                {
                    if(Level1Congratulations)
                    {
                        videoGame.isLevel2 = true;
                        currentTrack = 2;
                    }
                    else
                    {
                        videoGame.isLevel1 = true;
                        currentTrack = 1;
                    }
                }
            }
        }
       
       
    }
    waveOut.Init(new AudioFileReader(musicList[currentTrack]));
    waveOut.Play();
};
// Inicia la primera pista
waveOut.Init(new AudioFileReader(musicList[currentTrack]));
waveOut.Play();

Console.ForegroundColor = ConsoleColor.Red;
//Start screen
drawTitles.DrawStartScreen();


Console.OutputEncoding = System.Text.Encoding.UTF8;
bool s = true;
string respuesta;
string? TagDelJugadorActual = null;
string transformarRespuesta = "";
Console.ForegroundColor = ConsoleColor.Magenta;
Console.BackgroundColor = ConsoleColor.Black;
while(s)
{
    Console.Clear();
    //videoGame.PerfilDeSalida(TagDelJugadorActual); 
    Console.CursorLeft = 0;

    videoGame.VerUsuario();
    
    if(hadPerfil)
    {
        //videoGame.PerfilJugador(TagDelJugadorActual);
        while(videoGame.esPerfil)
        {
            Console.Clear();
            
            System.Console.WriteLine(videoGame.scoreDelJugador);
            IsMenu = true;
            //videoGame.PerfilDeSalida(TagDelJugadorActual); 
            while(videoGame.GameFinish)
            {
                waveOut.Stop();
                Level1Congratulations = false;
                Level2Congratulations = false;
                Level3Congratulations = false;
                Level4Congratulations = false;
                videoGame.isLevel1 = false;
                videoGame.isLevel2 = false;
                videoGame.isLevel3 = false;
                videoGame.isLevel4 = false;
                IsMenu = true;
                currentTrack = 0;
                Console.Clear();
                if(videoGame.gameOver == false)
                {
                    drawTitles.DrawWin();
                }
                else
                {
                    //screen de game over
                    drawTitles.DrawGameOver();
                }
                

                string input = "";
                ConsoleKeyInfo keyInfo;
                while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                {
                    input += keyInfo.KeyChar;
                }
                videoGame.gameOver = false; 
                videoGame.GameFinish = false;
                videoGame.isMenuInterfaces = true;
                Console.Clear();
            
            }
            
            
            
            List<string> messages = new List<string> {
            "JUGAR CAMPAÑA","CERRAR JUEGO","CERRAR PERFIL","MANUAL",
            };
            int currentSelection = 1;
            int top = Console.WindowHeight / 2 - messages.Count / 2;
            
            while (videoGame.isMenuInterfaces)
            {
                Console.Clear();
                drawTitles.DrawTitleScreen(windowWidth, windowWidth);
                Console.ForegroundColor = ConsoleColor.Cyan;

                for (int i = 0; i < messages.Count; i++)
                {
                    string message = messages[i];
                    int left = (Console.WindowWidth / 2) - (message.Length / 2);
                    Console.SetCursorPosition(left, top + i);
                    
                    if (i == currentSelection - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        Console.Write(message);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(message);
                    }
                }
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    currentSelection--;
                    
                    if (currentSelection < 1)
                    {
                        currentSelection = messages.Count;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    currentSelection++;
                    
                    if (currentSelection > messages.Count)
                    {
                        currentSelection = 1;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    switch (currentSelection)
                    {
                        case 1:
                            Console.Clear();
                    
                        Console.Clear();
                        drawTitles.message = "En un futuro lejano los humanos ya habian colonizado gran parte del sistema solar";
                        drawTitles.DramTextLevels();
                        Console.ReadKey();
                        Console.Clear();
                        drawTitles.message = "su gran afan de explorar el inifinito era altamente deseado";
                        drawTitles.DramTextLevels();
                        Console.ReadKey();
                        Console.Clear();
                        drawTitles.message = "Consideraban al universo como un vasto vacío interminable lleno de misterios oscuros";
                        drawTitles.DramTextLevels();
                        Console.ReadKey();
                        Console.Clear();
                        drawTitles.message = "Un piloto despega para su primera exploracion espacial, este viaje puede ser su ultima partida";
                        drawTitles.DramTextLevels();
                        Console.ReadKey();
                        Console.Clear();
                        
                        IsMenu = false;
                        waveOut.Stop();
                        IsMenu = false;
                        

                        //Screen de Inicio
                        drawTitles.DrawTitleScreen(windowWidth,WindowHeight);
                        videoGame.EjecutarJuego();
                        waveOut.Stop();
                    
                            break;
                        case 2:
                            Environment.Exit(0);
                            
                            break;
                        case 3:
                        videoGame.Jugador2Activo = false;
                        videoGame.enJuego = false;
                        videoGame.esPerfil = false;
                        hadPerfil = false;
                        videoGame.isMenuInterfaces = false;
                        
                            break;
                        case 4:
                            bool isInstruccions = true;
                        while (isInstruccions)
                        {
                            Console.Clear();
                            //screen de instrucciones
                            drawTitles.DrawInstructions();
                            string input = "";
                            ConsoleKeyInfo keyInfor;
                            while ((keyInfor = Console.ReadKey(true)).Key != ConsoleKey.Enter)
                            {
                                input += keyInfor.KeyChar;
                            }
                            isInstruccions = false; 
                        }
                            break;
                        default:
                            break;
                    }
                }
            }
            
        }

    }
    else
    {
        System.Console.WriteLine("");
        string message = "___COLOCA TU PERFIL EXISTENTE O CREA UNO NUEVO___"; // Texto a centrar
        // Calcular la posición para centrar el texto
        windowWidth = Console.WindowWidth;
        int messageWidth = message.Length;
        int left = (windowWidth / 2) - (messageWidth / 2);
        int top = Console.CursorTop;
        Console.SetCursorPosition(left, top);
        Console.WriteLine(message);
        respuesta = Console.ReadLine()!;
        TagDelJugadorActual = respuesta.ToUpper();
        videoGame.Registrar(TagDelJugadorActual);
        Console.ReadKey();

        if(TagDelJugadorActual == "")
        {
            Console.SetCursorPosition(left, top);
            message = "La casilla esta vacia";
            System.Console.WriteLine(message);
        }
        else
        {
            videoGame.Registrar(TagDelJugadorActual);
            hadPerfil = true;
            videoGame.isMenuInterfaces = true;
        }
    }
}

//Musica
class Musica
{
    
}
//Perfiles y lista de clasificaciones
class Usuario
{
    public string tag;
    public int scoreIndividual = 0;
    public bool jugadorAJugado = false;
   
}
class equipo
{
    public string nombreEquipo;
    public int c_DeEquipo = 0;
    public int score_duo = 0;

    public equipo(int c_DeEquipo, string nombreDeEquipo)
    {
        this.nombreEquipo = nombreDeEquipo;
        this.c_DeEquipo = c_DeEquipo;
    }
}
class RankingDePuntuacion
{
    public string? tagJugador = null;
    public int score = 0;
    public int posicionDeClasificacion = 0;
    public RankingDePuntuacion(string tagJugador)
    {
        this.tagJugador = tagJugador;
    }
}
class RankingDePuntuacionPorEquipos
{
    public string? tagJugador1 = null;
    public string? tagJugador2 = null;
    public string? nombreDeEquipo = null;
    public int score = 0;
    public int posicionDeClasificacion = 0;
    public RankingDePuntuacionPorEquipos(string tagJugador1, string tagJugador2, string nombreDeEquipo)
    {
        this.tagJugador1 = tagJugador1;
        this.tagJugador2 = tagJugador2;
        this.nombreDeEquipo = nombreDeEquipo;
    }
}
class DrawTitles
{
    public string message = "";
    int windowWidth = 0;
    int top = 0;
    int totalHeight =0;
    public void DrawStartScreen()
    {
        List<string> messages = new List<string> {
        "██████╗░░█████╗░░█████╗░██╗░░██╗  ░██████╗██╗░░██╗██╗██████╗░  ",
        "██╔══██╗██╔══██╗██╔══██╗██║░██╔╝  ██╔════╝██║░░██║██║██╔══██╗ ",
        "██████╔╝██║░░██║██║░░╚═╝█████═╝░  ╚█████╗░███████║██║██████╔╝░",
        "██╔══██╗██║░░██║██║░░██╗██╔═██╗░  ░╚═══██╗██╔══██║██║██╔═══╝░ ",
        "██║░░██║╚█████╔╝╚█████╔╝██║░╚██╗  ██████╔╝██║░░██║██║██║░░░░░  ",
        "╚═╝░░╚═╝░╚════╝░░╚════╝░╚═╝░░╚═╝  ╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░  ",
        "       ~+                            ",
        "                *       +            ",
        "            '                  |     ",
        "       ()    .-.,= `` =.    - o -    ",
        "           '=/_           |        ",
        "        *   |  '=._    |             ",
        "                    `=./`,        '",
        "            .   '=.__.=' `='      *  ",
        "    +                         +      ",
        "        O      *        '       .    ",  
        "[Preciona cualquier tecla]",


        }; // Lista de mensajes a centrar
        int windowWidth = Console.WindowWidth;
        int totalHeight = messages.Count;
        int top = (Console.WindowHeight /4) - (totalHeight / 2);
        foreach (string message in messages) 
        {
            int left = (windowWidth / 2) - (message.Length / 2);
            Console.SetCursorPosition(left, top++);
            Console.Write(message);
            //Thread.Sleep(1);
        }
        Console.ReadKey();
    }
    public void DrawTitleScreen(int width, int height)
    {
        List<string> messages = new List<string> {
        "██████╗░░█████╗░░█████╗░██╗░░██╗  ░██████╗██╗░░██╗██╗██████╗░  ",
        "██╔══██╗██╔══██╗██╔══██╗██║░██╔╝  ██╔════╝██║░░██║██║██╔══██╗ ",
        "██████╔╝██║░░██║██║░░╚═╝█████═╝░  ╚█████╗░███████║██║██████╔╝░",
        "██╔══██╗██║░░██║██║░░██╗██╔═██╗░  ░╚═══██╗██╔══██║██║██╔═══╝░ ",
        "██║░░██║╚█████╔╝╚█████╔╝██║░╚██╗  ██████╔╝██║░░██║██║██║░░░░░  ",
        "╚═╝░░╚═╝░╚════╝░░╚════╝░╚═╝░░╚═╝  ╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░  ",
        "       ~+                            ",
        "                *       +            ",
        "            '                  |     ",
        "       ()    .-.,= `` =.    - o -    ",
        "           '=/_           |        ",
        "        *   |  '=._    |             ",
        "                    `=./`,        '",
        "            .   '=.__.=' `='      *  ",
        "    +                         +      ",
        "        O      *        '       .    "
        }; // Lista de mensajes a centrar
            
            // Calcular la posición para centrar el texto
        windowWidth = Console.WindowWidth;
        totalHeight = messages.Count;
        top = (Console.WindowHeight / 4) - (totalHeight / 2);
            
            // Imprimir el texto centrado en la consola
        foreach (string message in messages) 
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int left = (windowWidth / 2) - (message.Length / 2);
            Console.SetCursorPosition(left, top++);
            Console.WriteLine(message);
        }
    }
    public void DrawInstructions()
    {
        List<string> messagesOver = new List<string> 
        {
            "INSTRUCCIONES Y TIPS",
            "-----CONTROLES-----",
            "1- 'A' para ir hacia la izquierda",
            "2- 'D' para ir hacia la derecha",
            "3- 'W' para ir hacia la adelantea",
            "4- 'S' para ir hacia la abajo",
            "---MECANICAS DEL JUEGO---",
            "",
            "1-Inicias con 5 vidas",
            "1-Sobrevive hasta que termine la melodia",
            "2-Existen mejoras para sobrevivir mas en el juego",
            "¿Podras llegar al jefe final?",
            "---OBJETOS DEL JUEGO---",
            "",
            "1- MEJORA DE DISPARO ▄ proporciona la habilidad de disparar por un cierto tiempo para destruir los obstaculos",
            "2- MEJORA DE VIDA ♥ te proporciona una vida extra",
            "",
            "¡QUE TE DIVIERTAS MUCHO!",
            "[Preciona enter]"
        }; // Lista de mensajes a centrar
            
            // Calcular la posición para centrar el texto
        int windowWidthOver = Console.WindowWidth;
        int totalHeightOver = messagesOver.Count;
        int topOver = (Console.WindowHeight / 4) - (totalHeightOver / 2);
            
            // Imprimir el texto centrado en la consola
        foreach (string messageOver in messagesOver) 
        {
            int left = (windowWidthOver / 2) - (messageOver.Length / 2);
            Console.SetCursorPosition(left, topOver++);
            Console.Write(messageOver);
        }
    }
    public void DrawGameOver()
    {
        List<string> messagesOver = new List<string> 
        {
        "████ ███ █▄┼▄█ ███ ┼┼ ███ █▄█ ███ ███",
        "█┼▄▄ █▄█ █┼█┼█ █▄┼ ┼┼ █┼█ ███ █▄┼ █▄┼",
        "█▄▄█ █┼█ █┼┼┼█ █▄▄ ┼┼ █▄█ ┼█┼ █▄▄ █┼█",
        "       ~+                            ",
        "                *       +            ",
        "            '                  |     ",
        "       ()    .-.,= `` =.    - o -    ",
        "           '=/_           |        ",
        "        *   |  '=._    |             ",
        "                    `=./`,        '",
        "            .   '=.__.=' `='      *  ",
        "    +                         +      ",
        "        O      *        '       .    ",  
        "¡Oh no!, no te rindas, por suerte podemos regresar al pasado", 
        "[Preciona enter]"

        }; // Lista de mensajes a centrar
            
            // Calcular la posición para centrar el texto
        int windowWidthOver = Console.WindowWidth;
        int totalHeightOver = messagesOver.Count;
        int topOver = (Console.WindowHeight / 4) - (totalHeightOver / 2);
            
            // Imprimir el texto centrado en la consola
        foreach (string messageOver in messagesOver) 
        {
            int left = (windowWidthOver / 2) - (messageOver.Length / 2);
            Console.SetCursorPosition(left, topOver++);
            Console.Write(messageOver);
        }
    }
    public void DrawWin()
    {
        List<string> messagesOver = new List<string> 
        {
        "█┼┼┼█ ███ █┼┼█",
        "█┼█┼█ ┼█┼ ██▄█",
        "█▄█▄█ ▄█▄ █┼██",
        "       ~+                            ",
        "                *       +            ",
        "            '                  |     ",
        "       ()    .-.,= `` =.    - o -    ",
        "           '=/_           |        ",
        "        *   |  '=._    |             ",
        "                    `=./`,        '",
        "            .   '=.__.=' `='      *  ",
        "    +                         +      ",
        "        O      *        '       .    ",  
        "¡Magnifico! completaste la campaña, como recompensa añadimos 10000pts de bonificacion a tu perfil",
        "Gracias por jugar",
        "[Preciona enter]"

        }; // Lista de mensajes a centrar
            
            // Calcular la posición para centrar el texto
        int windowWidthOver = Console.WindowWidth;
        int totalHeightOver = messagesOver.Count;
        int topOver = (Console.WindowHeight / 4) - (totalHeightOver / 2);
            
            // Imprimir el texto centrado en la consola
        foreach (string messageOver in messagesOver) 
        {
            int left = (windowWidthOver / 2) - (messageOver.Length / 2);
            Console.SetCursorPosition(left, topOver++);
            Console.Write(messageOver);
        }
    }
    public void DrawScreenStartGame(bool primerFrame)
    {
        List<string> messages = new List<string> {
        "██████╗░░█████╗░░█████╗░██╗░░██╗  ░██████╗██╗░░██╗██╗██████╗░  ",
        "██╔══██╗██╔══██╗██╔══██╗██║░██╔╝  ██╔════╝██║░░██║██║██╔══██╗ ",
        "██████╔╝██║░░██║██║░░╚═╝█████═╝░  ╚█████╗░███████║██║██████╔╝░",
        "██╔══██╗██║░░██║██║░░██╗██╔═██╗░  ░╚═══██╗██╔══██║██║██╔═══╝░ ",
        "██║░░██║╚█████╔╝╚█████╔╝██║░╚██╗  ██████╔╝██║░░██║██║██║░░░░░  ",
        "╚═╝░░╚═╝░╚════╝░░╚════╝░╚═╝░░╚═╝  ╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░  ",
        

        }; // Lista de mensajes a centrar
            
            // Calcular la posición para centrar el texto
        int windowWidth = Console.WindowWidth;
        int totalHeight = messages.Count;
        int top = (Console.WindowHeight / 2) - (totalHeight / 2);
        
            // Imprimir el texto centrado en la consola
        foreach (string message in messages) 
        {
            int left = (windowWidth / 2) - (message.Length / 2);
            Console.SetCursorPosition(left, top++);
            Console.Write(message);
            Thread.Sleep(1);
            
            //Console.WriteLine(message);
        }
        if(primerFrame)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            //Screen start
            
            Console.SetCursorPosition(((windowWidth /2)-5),top++);
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.Write("╩▄╩");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Beep(600, 700);
            System.Console.Write("▄══");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep(600, 700);
            System.Console.Write("▄╩▄");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Beep(600, 700);
            System.Console.Write("▄══");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Beep(800, 1000);
            System.Console.Write("╩▄╩");
            primerFrame = false;
        }
    }
    public void DramTextLevels()
    {
        // Calcular la posición para centrar el texto
        windowWidth = Console.WindowWidth;
        int messageWidth = message.Length;
        int left = (windowWidth / 2) - (messageWidth / 2);
        int top = Console.CursorTop;
        Console.SetCursorPosition(left, top);
        foreach (char caracter in message)
        {
            Console.Write(caracter);
            Thread.Sleep(1); // Espera 100 milisegundos entre cada caracter
        }
    }
}
//Obstaculos, naves y bosters
class NaveV2
{
    public int v2ShipX = 0;
    public int v2ShipY = 0;
    public int v2ProyectilX = 0;
    public int v2ProyectilY = 0;
    public int v2ProyectilX2 = 0;
    public int v2ProyectilY2 = 0;

    int  width = Console.WindowHeight;
    int height = Console.WindowHeight;

    bool isCheck = false;
    public NaveV2()
    {
        v2ShipX = width/2;
        v2ShipY = 5;
    }
    public void Draw(int widthNaveEnemy)
    {
        
        Console.SetCursorPosition(v2ShipX, v2ShipY);
        System.Console.Write("╩▄╩");
       
           
        

    }
    public void disparar(int shipX, int shipY)
    {
        DispararSeundoProyectil(shipX,shipY);
        Console.SetCursorPosition(v2ProyectilX, v2ProyectilY);
        System.Console.Write("O");
        if(v2ProyectilY ==  height -2 || (v2ProyectilY > (v2ShipY +40))||(v2ProyectilX == shipX && v2ProyectilY == shipY)|| v2ProyectilX == 1 || v2ProyectilY2 == height -2 )
        {
            v2ProyectilX = v2ShipX;
            v2ProyectilY = v2ShipY+1;
            
        }else
        {
            
            v2ProyectilY ++;
            v2ProyectilX --;
        }
    }
    public void DispararSeundoProyectil(int shipX, int shipY)
    {
        Console.SetCursorPosition(v2ProyectilX2, v2ProyectilY2);
        System.Console.Write("O");
        if(v2ProyectilY2 ==  height -2 || (v2ProyectilY2 > (v2ShipY +40))||(v2ProyectilX2 == shipX && v2ProyectilY2 == shipY)|| v2ProyectilX2 == width -1 || v2ProyectilY2 == height -2)
        {
            v2ProyectilX2 = v2ShipX;
            v2ProyectilY2 = v2ShipY+1;
            
        }else
        {
            
            v2ProyectilY2 ++;
            v2ProyectilX2 ++;
        }
    }
}
class NaveEnemiga
{
    public int enemyShipX = 0;
    public int enemyShipY = 0;
    public int enemyProyectilX = 0;
    public int enemyProyectilY = 0;
    int  width = Console.WindowHeight;
    int height = Console.WindowHeight;

    bool isCheck = false;
    public NaveEnemiga()
    {
        enemyShipX = width/4;
        enemyShipY = 10;
    }
    public void Draw(int widthNaveEnemy)
    {
        
        Console.SetCursorPosition(enemyShipX, enemyShipY);
        System.Console.Write("╩▄╩");
        if(enemyShipX == widthNaveEnemy-2)
        {
            isCheck =! isCheck;
        }
        else if(enemyShipX == 2)
        { 
            isCheck =! isCheck;
        }

        if(isCheck)
        {
            enemyShipX--;
        }
        else
        {
            enemyShipX++;
        }
           
        

    }
    public void disparar(int shipX, int shipY)
    {
        Console.SetCursorPosition(enemyProyectilX, enemyProyectilY);
        System.Console.Write("O");
        if(enemyProyectilY ==  height -2 || (enemyProyectilY > (enemyShipY +40))||(enemyProyectilX == shipX && enemyProyectilY == shipY))
        {
            enemyProyectilX = enemyShipX;
            enemyProyectilY = enemyShipY+1;
            
        }else
        {
            enemyProyectilY ++;
        }
    }
}
class Ship 
{
    int width = 0;
    int height = 0;
    public int shipX = 0;
    public int shipY = 0;

    public int proyectilX = 0;
    public int proyectilY = 0;
    
    int scoreX = 0;
    int scoreY = 0;

    public bool isShot = false;

    public bool AdestruidoObjeto = false;
    public bool AdestruidoCometa = false;
    public bool AdestruidoAsteroide = false;
    public bool AdestruidoMeteoro = false;
    public bool AdestruidoShotBooster = false;
    public bool ATomadoVida = false;

    public int maxHearths = 10;
    public Ship()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        this.shipX = width / 2;
        this.shipY = height - 2;
        proyectilX = shipX;
        proyectilY = shipY;
    }
    public void Draw()
    {
        
        Console.SetCursorPosition(shipX, shipY);
        Console.Write("╩▄╩");
        
    }
    public void Shot(int width, int height, int obstacleX, int obstacleY, int MeteoroX, int MeteoroY,int cometaX, int cometaY, int asteroideX, int asteroideY)
    {
        Console.SetCursorPosition(proyectilX, proyectilY);
        Console.Write("✩");
        if(proyectilY ==  1 || (proyectilY < (shipY -20)))
        {
            proyectilX = shipX;
            proyectilY = shipY-1;
            
        }else
        {
            proyectilY --;
        }
        if ((((proyectilX >= obstacleX && proyectilX <= (obstacleX +8)))&& proyectilY == obstacleY))
        {
            Console.Beep(600, 300);
            AdestruidoObjeto = true;
            proyectilY = 1;
        }
        else
        {
            AdestruidoObjeto = false;
        }
        
        if ((((proyectilX >= MeteoroX&& proyectilX <= (MeteoroX +8)))&& proyectilY == MeteoroY))
        {
            Console.Beep(600, 300);
            AdestruidoMeteoro = true;
            proyectilY = 1;
        }
        else
        {
            AdestruidoMeteoro = false;
        }
        if ((((proyectilX >= cometaX&& proyectilX <= (cometaX +8)))&& proyectilY == cometaY))
        {
            Console.Beep(600, 300);
            AdestruidoCometa = true;
            proyectilY = 1;
        }
        else
        {
            AdestruidoCometa = false;
        }
        if ((((proyectilX >= asteroideX&& proyectilX <= (asteroideX +8)))&& proyectilY == asteroideY))
        {
            Console.Beep(600, 300);
            AdestruidoAsteroide = true;
            proyectilY = 1;
        }
        else
        {
            AdestruidoAsteroide = false;
        }

    }
    public void Move(int width, int height)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.A && shipX > 2) 
            {
                shipX= shipX -3;
            }
            else if (key.Key == ConsoleKey.D && shipX < width-3)
            {
                shipX= shipX +3;
            }

            if (key.Key == ConsoleKey.W && shipY > 2) 
            {
                shipY--;
            }
            else if (key.Key == ConsoleKey.S && shipY < height - 2)
            {
                shipY++;
            }
        }    
    }
    public void Die(int obstacleX, int obstacleY, bool enJuego, bool gameOver, int MeteoroX, int MeteoroY, int cometaX,  int cometaY, int asteroideX, int asteroideY, int proX, int proY, int prox2, int proy2, int proyNodrizaX, int proyNodrizaY,int proyNodrizaX2, int proyNodrizaY2,int proyNodrizaX3, int proyNodrizaY3)
    {
        //System.Console.WriteLine("Esto es shipX: "+shipX + "Esto es obstacleX: "+obstacleX);
        if(((shipX >= obstacleX && shipX <= (obstacleX +8))&& shipY == obstacleY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((shipX >= MeteoroX && shipX <= (MeteoroX +8))&& shipY == MeteoroY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((shipX >= cometaX && shipX <= (cometaX +8))&& shipY == cometaY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((shipX >= asteroideX && shipX <= (asteroideX +8))&& shipY == asteroideY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((proX >= shipX && proX <= (shipX+2))&& proY == shipY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((prox2 >= shipX && prox2 <= (shipX+2))&& proy2 == shipY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
        if(((proyNodrizaX >= shipX && proyNodrizaX <= (shipX+2))&& proyNodrizaY == shipY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
          if(((proyNodrizaX2 >= shipX && proyNodrizaX2 <= (shipX+2))&& proyNodrizaY2 == shipY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
          if(((proyNodrizaX3 >= shipX && proyNodrizaX3 <= (shipX+2))&& proyNodrizaY3 == shipY))
        {
            Console.Beep(600, 200);
            maxHearths --;
        }
        else
        {
            enJuego = true;
            gameOver = false;
        }
            
    }
    public void TakeBooster(int hearthBoosterX, int hearthBoosterY, int shotBoosterX, int shotBoosterY)
    {
        //shipX == hearthBoosterX || shipX+1 == hearthBoosterX|| shipX +2 == hearthBoosterX)&& shipY == hearthBoosterY
        if((hearthBoosterX >= shipX && hearthBoosterX <= (shipX +2))&& hearthBoosterY == shipY)
        {
            ATomadoVida = true;
            Console.Beep(900, 200);
            maxHearths++;
        }
        if((shotBoosterX >= shipX && shotBoosterX <= (shipX +2))&& shotBoosterY == shipY)
        {
            AdestruidoShotBooster = true;
            Console.Beep(900, 200);
            isShot = true;
        }

    }
    public void Score(int width, int height, int maxHearths, int scoreDelJugador)
    {
        int scoreX = width-25;
        int scoreY = 1;
        Console.SetCursorPosition(scoreX, scoreY);
        System.Console.Write("Score: "+scoreDelJugador +"pts    ");
        Console.Write(maxHearths+"♥"); 
    }
}
class Asteroide
{
    int width = 0;
    int height = 0;
    public int  asteroideX = 0;
    public int asteroideY = 0;
    public Asteroide()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        int asteroideX = width - 4;
        int asteroideY = height - 5;
    }
    public void Draw(bool AdestruidoAsteroide)
    {
        Console.SetCursorPosition(asteroideX, asteroideY);
        Console.Write("王王王王王");

        if (asteroideY == height - 1)
        {
            asteroideX = new Random().Next(width);

            asteroideY = 0;
        }
        else
        {
            if(AdestruidoAsteroide)
            {
                asteroideX = new Random().Next(width);
                asteroideY= 0;
            }
            else
            {
                asteroideY++; 
            }
            
        }

    }
}
class Obstacle
{
    int width = 0;
    int height = 0;
    public int obstacleX = 0;
    public int obstacleY = 0;
    public Obstacle()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        int obstacleX = width - 4;
        int obstacleY = height - 5;
    }
    public void Draw(bool AdestruidoObjeto)
    {
        Console.SetCursorPosition(obstacleX, obstacleY);
        Console.Write("⌬⌬⌬⌬⌬⌬⌬⌬");

        if (obstacleY == height - 1)
        {
            obstacleX = new Random().Next(width);

            obstacleY = 0;
        }
        else
        {
            if(AdestruidoObjeto)
            {
                obstacleX = new Random().Next(width);
                obstacleY = 0;
            }
            else
            {
                obstacleY++; 
            }
            
        }

    }
}
class Meteoro
{
    int width = 0;
    int height = 0;
    public int  MeteoroX = 0;
    public int MeteoroY = 0;
    public Meteoro()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        int obstacleX = width - 4;
        int obstacleY = height - 5;
    }
    public void Draw(bool AdestruidoMeteoro)
    {
        Console.SetCursorPosition(MeteoroX, MeteoroY);
        System.Console.WriteLine("多多多多多");
        if (MeteoroY == height - 1)
        {
            MeteoroX = new Random().Next(width);

            MeteoroY = 0;
        }
        else
        {
            if(AdestruidoMeteoro)
            {
                MeteoroX = new Random().Next(width);
                MeteoroY = 0;
            }
            else
            {
                MeteoroY++; 
            } 
        }

    }
}
class Cometa
{
    int width = 0;
    int height = 0;
    public int  cometaX = 0;
    public int cometaY = 0;
    public Cometa()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        int obstacleX = width - 4;
        int obstacleY = height - 5;
    }
    public void Draw(bool AdestruidoCometa)
    {
        Console.SetCursorPosition(cometaX , cometaY);
        Console.Write("╩▄╩▄╩▄╩▄");

        if (cometaY == height - 1)
        {
            cometaX  = new Random().Next(width);

            cometaY = 0;
        }
        else
        {
            if(AdestruidoCometa)
            {
                cometaX  = new Random().Next(width);
                cometaY= 0;
            }
            else
            {
                cometaY++; 
            }
            
        }

    }
}
class NaveNodriza
{
    public int enemyProyectilX = 0;
    public int enemyProyectilY = 0;
    public int enemyProyectilX2 = 0;
    public int enemyProyectilY2 = 0;
    public int enemyProyectilX3 = 0;
    public int enemyProyectilY3 = 0;
    int width = 0;
    int height = 0;
    public int vida = 100; // Establecer la cantidad de vida inicial
    public int naveNodrizaX = 0;
    public int naveNodrizaY = 0;
    public NaveNodriza()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        naveNodrizaX = width /2;
        naveNodrizaY = 3;
    }
    public void LeftBar(int proyectilX, int proyectilY)
    {
        // Dibujar la barra de vida inicial
        Console.SetCursorPosition(2,1);
        Console.Write("Alpha Ship:[");
        for (int i = 0; i < 100; i++)
        {
            if (vida > i)
            {
                Console.Write("█");
            }
            else
            {
                Console.Write("-");
            }
        }
        Console.Write("]");

        // Restar una vida y actualizar la barra de vida
        if(((proyectilX >= 80 && proyectilX<= 115)&& proyectilY < 14))
        {
            vida--;
            Console.SetCursorPosition(7, Console.CursorTop); // Mover el cursor a la posición de la barra de vida
            Console.Write("[");
            for (int i = 0; i < 10; i++)
            {
                if (vida > i)
                {
                    Console.Write("█");
                }
                else
                {
                    Console.Write("-");
                }
            }
            Console.Write("]");
        }
        

    }
    public void Draw()
    {
        
        
        
        List<string> messages = new List<string> {
        "▀  ▀",
        "▀ ████████▄▄▀",
        "▄▄       ▀███████████▄▀",
        " █ ▀▀▀▀    ██████████          ▄",
        "█          ███████       ▀▀▀▀ ▀",
        "▀▀▀██▀ ███████████████▀██    █",
        "▀▄  ▀▄ █▄█  ████  █▄█ ▀█ ▀▀▀▀",
        " ▄▀▀▀ ▄▀█  ██    ██  ██  ▀  ▄▀",
        " ▄▀ █▀█████████████ ▀▄▀▀▀▀▄",
        " █ █   █████████▀  █ █",
        " █   █  █   █  █   █",
        " █ █ █  █     █  █ █ █",
        "██ █   █ ██",
        }; // Lista de mensajes a centrar
        int windowWidth = Console.WindowWidth;
        int totalHeight = messages.Count;
        int top = (Console.WindowHeight /4) - (totalHeight / 2);
        foreach (string message in messages) 
        {
            int left = (windowWidth / 2) - (message.Length / 2);
            Console.SetCursorPosition(left, top++);
            Console.Write(message);
            //Thread.Sleep(1);
        }
        
        
       
      
    }
    public void Disparar(int shipX, int shipY)
    {
        Console.SetCursorPosition(enemyProyectilX3,enemyProyectilY3);
        System.Console.Write("۝");
        Console.SetCursorPosition(enemyProyectilX, enemyProyectilY);
        System.Console.Write("۝");
        Console.SetCursorPosition(enemyProyectilX2,enemyProyectilY2);
        System.Console.Write("۝");
        
        if(enemyProyectilY ==  height -2 || (enemyProyectilY > (naveNodrizaY +40))||(enemyProyectilX == shipX && enemyProyectilY == shipY))
        {
            enemyProyectilX = naveNodrizaX;
            enemyProyectilY = naveNodrizaY+1;
            
        }else
        {
            enemyProyectilY ++;
        }
        if((enemyProyectilY2 ==  height -2 || (enemyProyectilY2 > (naveNodrizaY +40))||(enemyProyectilX2 == shipX && enemyProyectilY2 == shipY))||enemyProyectilX2>width-2)
        {
            enemyProyectilX2 = naveNodrizaX;
            enemyProyectilY2 = naveNodrizaY+1;
            
        }else
        {
            enemyProyectilY2 ++;
            enemyProyectilX2++;
        }
        if((enemyProyectilY3 ==  height -2 || (enemyProyectilY3 > (naveNodrizaY +40))||(enemyProyectilX3 == shipX && enemyProyectilY3 == shipY))||enemyProyectilX3 < 1)
        {
            enemyProyectilX3 = naveNodrizaX;
            enemyProyectilY3 = naveNodrizaY+1;
            
        }else
        {
            enemyProyectilY3 ++;
            enemyProyectilX3--;
        }
    }
}
//Boosters :)
class BoosterVida
{
    int width = 0;
    int height = 0;
    public int hearthBoosterX = 0;
    public int hearthBoosterY = 0;

    bool IsVelocityHearth = false;
    public BoosterVida()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        hearthBoosterX = width /3;
        hearthBoosterY = height /2;
    }
    public void Draw(int shipX, int shipY)
    {
        
        Console.SetCursorPosition(hearthBoosterX, hearthBoosterY);
        Console.Write("♥");
        if ((hearthBoosterX >= shipX && hearthBoosterX<= (shipX +2))&& hearthBoosterY== shipY)
        {
            hearthBoosterX = new Random().Next(4,width -4);
            hearthBoosterY = new Random().Next(4,height-4);
        }
        else
        {
            IsVelocityHearth =! IsVelocityHearth;
            if(IsVelocityHearth)
            {
                hearthBoosterY ++;
            }
            else
            {

            }
        }
        if(hearthBoosterY > height -2)
        {
            hearthBoosterY = 2;
        }
    }
}
class BoosterDisparo
{
    int width = 0;
    int height = 0;
    public int shotBoosterX = 0;
    public int shotBoosterY = 0;

    bool choquesDeCosola = false;
    bool choqueAbajo = false;
    bool choqueArriba = false;
    bool choqueDerecha = false;
    bool choqueIzquierda = false;

    bool isVelocityShot;

    public BoosterDisparo()
    {
        width = Console.WindowWidth;
        height = Console.WindowHeight;
        shotBoosterX = width /2;
        shotBoosterY = height /2; 
    }
    public void Draw(int shipX, int shipY, bool AdestruidoShotBooster)
    {
        Console.SetCursorPosition(shotBoosterX, shotBoosterY);
        Console.Write("▄");
        if ((shotBoosterX >= shipX && shotBoosterX <= (shipX +2))&& shotBoosterY == shipY)
        {
            shotBoosterY = new Random().Next(height -3);
        }
        else
        {
            isVelocityShot =! isVelocityShot;
            if(isVelocityShot)
            {
                shotBoosterY++;
                shotBoosterX++;
            }
            else
            {

            }
            
        }
        if (shotBoosterY > height - 2 || shotBoosterX > width -1)
        {
            shotBoosterX  = new Random().Next(width-3);

            shotBoosterY = 1;
        }
    }
}
//Funciones de videojuego
class VideoGame
{
    public VideoGame()
    {
        usuarios = new List<Usuario>(); // Inicializa la lista de jugadores.
        if (File.Exists("jugadores.json")) // Verifica si existe el archivo "jugadores.json".
        {
            string jugadoresJson = File.ReadAllText("jugadores.json"); // Lee el archivo "jugadores.json" y lo almacena en una variable de tipo string.
            usuarios = JsonConvert.DeserializeObject<List<Usuario>>(jugadoresJson)!; // Deserializa la información del archivo en una lista de jugadores y la asigna a la variable "jugadores".
        }
    }

    public bool isLevel1 = false;
    public bool isLevel2 = false;
    public bool isLevel3 = false;
    public bool isLevel4 = false;
    
    int width = Console.WindowWidth;
    int height = Console.WindowHeight;
    DrawTitles drawTitles = new DrawTitles();
    Ship ship = new Ship();
    Asteroide asteroide = new Asteroide();
    Cometa cometa = new Cometa();
    Meteoro meteoro = new Meteoro();
    NaveEnemiga naveEnemiga = new NaveEnemiga();
    NaveV2 naveV2 = new NaveV2();
    Obstacle obstacle = new Obstacle();
    Obstacle obstacle2 = new Obstacle();
    Obstacle obstacle3 = new Obstacle();
    Obstacle obstacle4 = new Obstacle();
    Obstacle obstacle5 = new Obstacle();
    NaveNodriza naveNodriza = new NaveNodriza();
    BoosterVida boosterVida = new BoosterVida();
    BoosterDisparo boosterDisparo = new BoosterDisparo();

    public List<Usuario>usuarios;
    List<RankingDePuntuacion>usuariosDestacados = new List<RankingDePuntuacion>();
    List<equipo> equipos = new List<equipo>();
    //JSON
    int codigoUsuario = 0;
    public bool GameFinish = false;
    public int iniciarPuntuacion = 1;
    public int scoreDelJugador = 0;
    public bool Jugador2Activo = false;
    public bool isMenuInterfaces = true;
    public bool esPerfil = false;
    public bool gameOver = false;
    public bool AdestruidoObjeto;
    int c_jugador;
    //public bool aPerdidoFrenarMusica = false;
    public bool enJuego = false;
    public bool primerFrame = true;
    public bool isDerrotado = false;
    public int indexTag1;
    public int indexTag2;
    public int indexTagAnterior = -1;
    Usuario jugadorExistente;
    
    public void Registrar(string TagDelJugadorActual)
    {
        jugadorExistente = usuarios.Find(j => j.tag == TagDelJugadorActual); // Busca en la lista de jugadores si ya existe un jugador con el ID ingresado. Si existe, almacena ese jugador en la variable "jugadorExistente".
        if (jugadorExistente != null) // Verifica si la variable "jugadorExistente" no es nula.
        {
            esPerfil = true;
            Console.WriteLine($"Hola de nuevo {jugadorExistente.tag} Tu puntuacion maxima: {jugadorExistente.scoreIndividual}"); // Si el jugador ya existe en la lista, muestra sus datos en la consola.
        }
        else // Si el jugador no existe en la lista.
        {
            esPerfil = true;
            var user = new Usuario // Crea un nuevo objeto Jugador con los datos ingresados.
            {
                tag = TagDelJugadorActual,
            };

            usuarios.Add(user); // Agrega el nuevo jugador a la lista de jugadores.

            string jugadoresJson = JsonConvert.SerializeObject(usuarios); // Convierte la lista de jugadores a un formato JSON y la almacena en una variable de tipo string llamada "jugadoresJson".
            File.WriteAllText("jugadores.json", jugadoresJson); // Escribe el contenido de la variable "jugadoresJson" en el archivo "jugadores.json".

            Console.WriteLine($"Bienvenido {user.tag} te has registrado ¡exitosamente!"); // Muestra en la consola que se ha registrado el nuevo jugador.
        }
    }
    public void VerUsuario()
    {   
       
        Console.Clear();
        int posicionDeClasificacion = 0;
        int scoreAnterior = 0;
        string message = "-----TOP 10 MEJORES PUNTUACIONES-----"; // Texto a centrar
        // Calcular la posición para centrar el texto
        int windowWidth = Console.WindowWidth;
        int messageWidth = message.Length;
        int left = (windowWidth / 2) - (messageWidth / 2);
        int top = Console.CursorTop;
        Console.SetCursorPosition(left, top);
        Console.WriteLine(message);
        System.Console.WriteLine("");
        //Ver usuarios en JSON ordenados del mas alto al mas bajo de los 10 mejores puntuaciones
        List<Usuario> clasificacion = usuarios.OrderByDescending(o => o.scoreIndividual).ToList();
    
        foreach(var j in usuarios)
        {  
            if(posicionDeClasificacion <=9)
            {
                posicionDeClasificacion ++;
                message = posicionDeClasificacion+"-Perfil: "+j.tag+ "  Puntuacion:"+ j.scoreIndividual; // Texto a centrar
                // Calcular la posición para centrar el texto
                windowWidth = Console.WindowWidth;
                messageWidth = message.Length;
                left = (windowWidth / 2) - (messageWidth / 2);
                top = Console.CursorTop;
                // Imprimir el texto centrado en la consola
                Console.SetCursorPosition(left, top);
                Console.WriteLine(message);
                scoreAnterior = j.scoreIndividual;  
            }                                                   
        }
    }
    public void PerfilJugador(string Tag)
    {
        int indexTag1 = usuarios.FindIndex(u => u.tag == Tag);
        if(indexTag1 != -1)
        {
            Console.Clear();
            esPerfil = true;
            enJuego = true;
            if(usuarios[indexTag1].jugadorAJugado)
            {
                if(indexTag1 == indexTagAnterior)
                {
                    if(scoreDelJugador > usuarios[indexTag1].scoreIndividual)
                    {
                        Console.Clear();
                        usuarios[indexTag1].scoreIndividual = scoreDelJugador;
                        System.Console.WriteLine("                                                          Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts");
                    }
                    else
                    {
                        System.Console.WriteLine("                                                          Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts");
                    }
                }
                else
                {
                    System.Console.WriteLine("                                                          Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts");
                }
            }
            else
            {
                usuarios[indexTag1].scoreIndividual = 0;
                string message ="Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts"; // Texto a centrar
                // Calcular la posición para centrar el texto
                int windowWidth = Console.WindowWidth;
                int messageWidth = message.Length;
                int left = (windowWidth / 2) - (messageWidth / 2);
                int top = Console.CursorTop;
                // Imprimir el texto centrado en la consola
                Console.SetCursorPosition(left, top +10);
                Console.WriteLine(message);
            }
        }
        else
        {
            esPerfil = false;
            enJuego = false;
            System.Console.WriteLine("                                                                          ¡No se encontro tu tag!");
        }
    }
    public void PerfilDeSalida(string Tag)
    {
        
        indexTag1 = usuarios.FindIndex(u => u.tag == Tag);

        if(indexTag1 != -1)
        {
            Console.Clear();
            esPerfil = true;
            enJuego = true;

            if(usuarios[indexTag1].jugadorAJugado)
            {
                if(indexTag1 == indexTagAnterior)
                {
                    if(scoreDelJugador > usuarios[indexTag1].scoreIndividual)
                    {
                        usuarios[indexTag1].scoreIndividual = scoreDelJugador;
                        //System.Console.WriteLine("                                                                  𝐇𝐎𝐋𝐀! ^"+usuarios[indexTag1].tag+"^"+ " TU PUNTUACION MAXIMA: "+usuarios[indexTag1].scoreIndividual+"PTS");
                    }
                    else
                    {
                        
                        System.Console.WriteLine("_____________________________________________________________________Hola! ^"+usuarios[indexTag1].tag+"^"+ " TU PUNTUACION MAXIMA: "+usuarios[indexTag1].scoreIndividual+"PTS___________________________________________________________________");
                    }                             
                }
                else
                {
                    usuarios[indexTag1].scoreIndividual = 0;
                    
                    System.Console.WriteLine("_____________________________________________________________________Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts___________________________________________________________________");
                }
               
            }
            else
            {
                usuarios[indexTag1].scoreIndividual = 0;
                System.Console.WriteLine("_____________________________________________________________________Hola! ^"+usuarios[indexTag1].tag+"^"+ "   Tu puntuacion maxima: "+usuarios[indexTag1].scoreIndividual+"pts___________________________________________________________________");
            }
        }
    }
    public void DosJugadores(string tagJugador2, string teamName)
    {
        int indexTeam = equipos.FindIndex(t => t.nombreEquipo == teamName);
        indexTag2 = usuarios.FindIndex(u => u.tag == tagJugador2);
        if(indexTag1 != -1 && indexTag2 != -1)
        {
            Console.Clear();
            esPerfil = true;
            System.Console.WriteLine(" Jugador 1: "+usuarios[indexTag1].tag+" Your ship:[♗]                                                                                      Jugador 2: "+usuarios[indexTag2].tag+" Your ship: [♝]");
            Jugador2Activo = true;
            //equipos[indexTeam].score_duo = scoreDelJugador;
        }
        else
        {
            esPerfil = false;
            //enJuego = false;
            System.Console.WriteLine("                                                        ¡Lo sentimos el los tags son inecxistentes!");
            Jugador2Activo = false;
            
        }
    }
    public void VerScoreDuo()
    {
        foreach(equipo e in equipos)
        {
            System.Console.WriteLine($" Score duo {e.score_duo}");
        }
    }
    public void EjecutarJuego()
    {
        
        //Empezar juego con 2 vidas :)        
        boosterVida.hearthBoosterX = width /3;
        boosterVida.hearthBoosterY = height /2;
        boosterDisparo.shotBoosterX =width/2;
        boosterDisparo.shotBoosterY = height/3;
        ship.shipX = width/2;
        ship.shipY = height -2;
        ship.maxHearths = 5;
        
        bool isWin = true;
        int Difficult = 20;
        int score = 0;
        int countShots = 0;
        scoreDelJugador = 0;

        bool isFirstText1 = true;
        bool isFirstText2 = true;
        bool isFirstText3 = true;
        

        
        while (isWin)
        {
            if(naveNodriza.vida <1)
            {
                isDerrotado = true;
            }
            Console.Clear();
            
            if(isLevel4)
            {

                naveEnemiga.Draw(width);
                naveEnemiga.disparar(ship.shipX,ship.shipY);
         
                naveNodriza.LeftBar(ship.proyectilX, ship.proyectilY);
                naveNodriza.Draw();   
                naveNodriza.Disparar(ship.shipX,ship.shipY);
                //ship.Shot(width,height,obstacle.obstacleX,obstacle.obstacleY, meteoro.MeteoroX, meteoro.MeteoroY, cometa.cometaX, cometa.cometaY, asteroide.asteroideX, asteroide.asteroideY);
                
                Console.ForegroundColor =  ConsoleColor.Red;
                Difficult = 1;    
            }
            else
            {
                if(isLevel3)
                {
                    
                    naveEnemiga.Draw(width);
                    naveEnemiga.disparar(ship.shipX,ship.shipY);
                 
                    //ship.Shot(width,height,obstacle.obstacleX,obstacle.obstacleY, meteoro.MeteoroX, meteoro.MeteoroY, cometa.cometaX, cometa.cometaY, asteroide.asteroideX, asteroide.asteroideY);
                    Console.ForegroundColor = ConsoleColor.Green;
                    if(isFirstText3)
                    {
                        drawTitles.message = "Level 3. Naves extrañas, ¿El primer decubrimiento de vida no amigable?";
                        drawTitles.DramTextLevels();
                        //screen game
                        drawTitles.DrawScreenStartGame(primerFrame);
                        isFirstText3 = false;
                    }
                    
                    Difficult = 10;
                }
                else
                {
                    if(isLevel2)
                    {
                       
                        //ship.Shot(width,height,obstacle.obstacleX,obstacle.obstacleY, meteoro.MeteoroX, meteoro.MeteoroY, cometa.cometaX, cometa.cometaY, asteroide.asteroideX, asteroide.asteroideY);
                        
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        if(isFirstText2)
                        {
                            drawTitles.message = "Level 2. Mas alla del infinito";
                            drawTitles.DramTextLevels();
                            //screen game
                            drawTitles.DrawScreenStartGame(primerFrame);
                            isFirstText2 = false;
                        }
                        Difficult = 10;   
                    }
                    else
                    {
                        if(isLevel1)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            if(isFirstText1)
                            {
                                drawTitles.message = "Level 1. Explorando el universo";
                                drawTitles.DramTextLevels();
                                //screen game
                                drawTitles.DrawScreenStartGame(primerFrame);
                                isFirstText1 = false;
                            }
                            Difficult =20;
                        }
                    }
                }
            }
            ship.Draw();
            asteroide.Draw(ship.AdestruidoAsteroide);
            cometa.Draw(ship.AdestruidoCometa);
            meteoro.Draw(ship.AdestruidoMeteoro);
        
            obstacle.Draw(ship.AdestruidoObjeto);
            boosterVida.Draw(ship.shipX, ship.shipY);
            boosterDisparo.Draw(ship.shipX, ship.shipY, ship.AdestruidoShotBooster);
            if(ship.maxHearths == 0 || naveNodriza.vida == 0)
            {
                GameFinish = true;
                Console.Clear();
                string message = "";
                enJuego = false;
                if(naveNodriza.vida == 0)
                {
                    scoreDelJugador = scoreDelJugador +10000;
                    message = jugadorExistente.tag+" Derroto a Alpha Ship"+ " El score fue de: "+ scoreDelJugador+"pts  "+" 10000pts extra"; // Texto a centrar
                    gameOver = false;
                    Console.Beep(500, 400);
                    Console.Beep(700, 500);
                    Console.Beep(800, 600);
                    Console.Beep(900, 800);
                }
                else
                {
                    message = jugadorExistente.tag+" ESTA MUERTO "+ "El score fue de: "+ scoreDelJugador+"pts"; // Texto a centrar
                    gameOver = true;
                    Console.Beep(700, 400);
                    Console.Beep(600, 500);
                    Console.Beep(500, 600);
                    Console.Beep(400, 800);
                }
                
                isMenuInterfaces = false;
                jugadorExistente.jugadorAJugado = true;
                if(jugadorExistente.scoreIndividual < scoreDelJugador)
                {
                    jugadorExistente.scoreIndividual = scoreDelJugador;
                    string jugadoresJson = JsonConvert.SerializeObject(usuarios);
                    File.WriteAllText("jugadores.json", jugadoresJson);
                }
                
                
                // Calcular la posición para centrar el texto
                int windowWidth = Console.WindowWidth;
                int messageWidth = message.Length;
                int left = (windowWidth / 2) - (messageWidth / 2);
                int top = Console.CursorTop;
                Console.SetCursorPosition(left, top);
                Console.WriteLine(message);
                Console.WriteLine();
               
                primerFrame = true;
                isWin = false;
                ship.maxHearths = 3;
                

                break;
            } 
            
            //Desaparecer objeto cuando impacte
            ship.Move(width, height);
            //Posibilidad de muerte
            ship.Die(obstacle.obstacleX, obstacle.obstacleY, enJuego, gameOver, meteoro.MeteoroX, meteoro.MeteoroY, cometa.cometaX, cometa.cometaY, asteroide.asteroideX, asteroide.asteroideY, naveEnemiga.enemyProyectilX, naveEnemiga.enemyProyectilY, naveV2.v2ProyectilX, naveV2.v2ProyectilY, naveNodriza.enemyProyectilX, naveNodriza.enemyProyectilY,naveNodriza.enemyProyectilX2, naveNodriza.enemyProyectilY2,naveNodriza.enemyProyectilX3, naveNodriza.enemyProyectilY3);
            //Disparar
            if(ship.isShot && countShots <500)
            {
                countShots++;
                ship.Shot(width,height,obstacle.obstacleX,obstacle.obstacleY, meteoro.MeteoroX, meteoro.MeteoroY, cometa.cometaX, cometa.cometaY, asteroide.asteroideX, asteroide.asteroideY);
            }
            else
            {
                countShots = 0;
                ship.isShot = false;
            }
            //Puntuacion
            ship.Score(width, height,ship.maxHearths, scoreDelJugador);
            //Tomar boosters
            ship.TakeBooster(boosterVida.hearthBoosterX, boosterVida.hearthBoosterY, boosterDisparo.shotBoosterX, boosterDisparo.shotBoosterY);
            //Score aculativo del jugador
            scoreDelJugador++;
            //Contador para el cambio de color
            score++;
            System.Threading.Thread.Sleep(Difficult);
        }
    }
}



  



