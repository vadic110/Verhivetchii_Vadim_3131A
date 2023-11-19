using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Verhivetchii_Vadim_3131A;

// Această clasă reprezintă fereastra de afișare a graficii 3D
class Window3D : GameWindow
{
    // Declarațiile de variabile și obiecte necesare pentru scenă, interacțiuni și desenare.
    private KeyboardState previousKeyboard;  // Starea tastaturii în frame-ul anterior pentru detectarea apăsărilor de taste
    private MouseState previousMouse;  // Starea mouse-ului în frame-ul anterior pentru detectarea acțiunilor cu mouse-ul
    private readonly Randomizer rando;  // Obiect pentru generarea de valori aleatoare
    private readonly Axes ax;  // Obiect pentru desenarea axelor în scena 3D
    private readonly Grid grid;  // Obiect pentru desenarea unei grile în scena 3D
    private readonly Camera3DIsometric cam;  // Obiect pentru gestionarea camerei 3D
    private bool displayMarker;  // Indicator pentru a afișa sau ascunde marcaje pentru analiza performanței
    private ulong updatesCounter;  // Contor pentru numărul de actualizări ale scenei
    private ulong framesCounter;  // Contor pentru numărul de cadre desenate
    private MassiveObject objy;  // Obiect masiv pentru scena 3D (potențial un obiect voluminos sau obiect de masă)

    private List<SomeObject> objectsList;  // Lista de obiecte create de utilizator
    private List<Vector3> vertices;  // Lista de vârfuri (vertices) citită dintr-un fișier
    private List<MassiveObject> massiveObjectsList;  // Lista de obiecte masive

    private readonly Color DEFAULT_BKG_COLOR = Color.FromArgb(49, 50, 51);  // Culoarea de fundal implicită

    // Constructorul clasei care inițializează obiectele și setările inițiale ale scenei
    public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
    {
        // Inițializarea parametrilor și a obiectelor
        VSync = VSyncMode.On;  // Sincronizare verticală pentru a sincroniza rata de cadru cu cea a monitorului

        rando = new Randomizer();  // Inițializare obiect pentru generarea de valori aleatoare
        ax = new Axes();  // Inițializare obiect pentru desenarea axelor în scena 3D
        grid = new Grid();  // Inițializare obiect pentru desenarea grilei în scena 3D
        cam = new Camera3DIsometric();  // Inițializare obiect pentru gestionarea camerei 3D
        objy = new MassiveObject(Color.Yellow);  // Inițializare obiect masiv pentru scena 3D cu culoare galbenă

        // Citirea vârfurilor (vertices) dintr-un fișier și stocarea lor într-o listă
        vertices = readVerticesFromFile(@"./../../coordonate.txt");

        // Inițializarea listelor de obiecte pentru a fi utilizate în scenă
        objectsList = new List<SomeObject>();
        massiveObjectsList = new List<MassiveObject>();

        // Afișarea instrucțiunilor în consolă pentru utilizator
        DisplayHelp();

        // Inițializarea variabilelor pentru analiza performanței
        displayMarker = false;
        updatesCounter = 0;
        framesCounter = 0;
    }

    // Metoda pentru încărcarea de setări și inițializări la începutul aplicației
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        GL.Enable(EnableCap.DepthTest);  // Activarea testării adâncimii pentru a gestiona obiectele 3D
        GL.DepthFunc(DepthFunction.Less);  // Setarea funcției de adâncime pentru a determina modul în care sunt desenate obiectele

        GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);  // Setarea sugestiei pentru desenarea liniilor netede în modul cel mai fin
    }

    // Metoda pentru redimensionarea ferestrei și setarea perspectivei
    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        // Setarea culorii de fundal și a viewport-ului în funcție de dimensiunile ferestrei
        GL.ClearColor(DEFAULT_BKG_COLOR);
        GL.Viewport(0, 0, this.Width, this.Height);

        // Setarea perspectivei și a matricei de proiecție pentru desenare 3D
        Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref perspectiva);

        // Setarea poziției camerei pentru a privi scena
        cam.SetCamera();
    }

    // Metoda pentru actualizarea datelor și a interacțiunilor în fiecare frame
    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        updatesCounter++;

        // Verificarea și afișarea marcajelor pentru analiza performanței
        if (displayMarker)
        {
            TimeStampIt("update", updatesCounter.ToString());
        }

        // Capturarea stării curente a tastaturii și a mouse-ului
        KeyboardState currentKeyboard = Keyboard.GetState();
        MouseState currentMouse = Mouse.GetState();

        // Verificarea tastelor apăsate și gestionarea interacțiunilor
        if (currentKeyboard[Key.Escape])
        {
            Exit();
        }

        if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
        {
            DisplayHelp();
        }

        if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
        {
            GL.ClearColor(DEFAULT_BKG_COLOR);
            ax.Show();
            grid.Show();
        }

        if (currentKeyboard[Key.K] && !previousKeyboard[Key.K])
        {
            ax.ToggleVisibility();
        }

        if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
        {
            GL.ClearColor(rando.RandomColor());
        }

        if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
        {
            grid.ToggleVisibility();
        }

        if (currentKeyboard[Key.O] && !previousKeyboard[Key.O])
        {
            objy.ToggleVisibility();
        }

        if (currentKeyboard[Key.W])
        {
            cam.MoveForward();
        }
        if (currentKeyboard[Key.S])
        {
            cam.MoveBackward();
        }
        if (currentKeyboard[Key.A])
        {
            cam.MoveLeft();
        }
        if (currentKeyboard[Key.D])
        {
            cam.MoveRight();
        }
        if (currentKeyboard[Key.Q])
        {
            cam.MoveUp();
        }
        if (currentKeyboard[Key.E])
        {
            cam.MoveDown();
        }
        if (currentKeyboard[Key.KeypadMinus])
        {
            cam.ZoomOut();
        }

        if (currentKeyboard[Key.KeypadPlus])
        {
            cam.ZoomIn();
        }

        if (currentKeyboard[Key.M])
        {
            cam.Far();
        }

        if (currentKeyboard[Key.N])
        {
            cam.Near();
        }
        if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
        {
            objectsList.Add(new SomeObject(true, vertices));
        }

        // Actualizarea poziției și stării obiectelor din scenă
        foreach (SomeObject obj in objectsList)
        {
            obj.UpdatePosition(true);
        }

        foreach (MassiveObject obj in massiveObjectsList)
        {
            obj.UpdatePosition(true);
        }

        // Gestionarea acțiunii de adăugare a unui obiect la clicul stâng al mouse-ului și eliminarea obiectelor la clicul drept
        if (currentMouse[MouseButton.Left] && !previousMouse[MouseButton.Left])
        {
            objectsList.Add(new SomeObject(true, vertices));
        }

        if (currentMouse[MouseButton.Right] && !previousMouse[MouseButton.Right])
        {
            objectsList.Clear();
        }

        // Schimbarea indicatorului pentru afișarea marcajelor de performanță la apăsarea tastei "L"
        if (currentKeyboard[Key.L] && !previousKeyboard[Key.L])
        {
            displayMarker = !displayMarker;
        }

        // Actualizarea stării tastaturii și a mouse-ului pentru frame-ul următor
        previousKeyboard = currentKeyboard;
        previousMouse = currentMouse;
    }

    // Metoda pentru desenarea elementelor în fiecare frame
    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        framesCounter++;

        // Verificarea și afișarea marcajelor pentru analiza performanței
        if (displayMarker)
        {
            TimeStampIt("render", framesCounter.ToString());
        }

        // Ștergerea bufferelor de culoare și adâncime pentru desenarea noilor cadre
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.Clear(ClearBufferMask.DepthBufferBit);

        // Desenarea elementelor în scenă (axele, grila, obiectele create, etc.)

        grid.Draw();
        ax.Draw();
        objy.Draw();

        foreach (SomeObject obj in objectsList)
        {
            obj.Draw();
        }

        foreach (MassiveObject obj in massiveObjectsList)
        {
            obj.Draw();
        }

        // Schimbarea bufferelor pentru a afișa noile cadre desenate
        SwapBuffers();
    }

    // Metoda pentru afișarea instrucțiunilor în consolă pentru utilizator
    private void DisplayHelp()
    {
        Console.WriteLine("\n      MENIU");
        Console.WriteLine(" (H) - meniul");
        Console.WriteLine(" (ESC) - parasire aplicatie");
        Console.WriteLine(" (K) - schimbare vizibilitate sistem de axe");
        Console.WriteLine(" (R) - resteaza scena la valori implicite");
        Console.WriteLine(" (B) - schimbare culoare de fundal");
        Console.WriteLine(" (V) - schimbare vizibilitate linii");
        Console.WriteLine(" (W,A,S,D) - deplasare camera (izometric)");
        Console.WriteLine(" (M,N) - setare camera la locatii predefinite (aproape si departe)");
        Console.WriteLine(" (+, -) - tastatura numerica - manipulare zoom camera");
    }

    // Metoda pentru afișarea timestamp-ului pentru analiza performanței
    private void TimeStampIt(String source, String counter)
    {
        String dt = DateTime.Now.ToString("hh:mm:ss.ffff");
        Console.WriteLine("     TSTAMP from <" + source + "> on iteration <" + counter + ">: " + dt);
    }

    // Metoda pentru citirea vârfurilor (vertices) dintr-un fișier
    public List<Vector3> readVerticesFromFile(string numeFisier)
    {
        List<Vector3> vertexuriDinFisier = new List<Vector3>();

        using (StreamReader sr = new StreamReader(numeFisier))
        {
            string linie;
            while ((linie = sr.ReadLine()) != null)
            {
                var numere = linie.Split(',');
                int i = 0;
                float[] coordonate = new float[3];
                foreach (var nr in numere)
                {
                    coordonate[i++] = float.Parse(nr);

                    if (coordonate[i - 1] < 0 || coordonate[i - 1] > 250)
                    {
                        throw new ArithmeticException("Invalid vertex !");
                    }
                }
                vertexuriDinFisier.Add(new Vector3(coordonate[0], coordonate[1], coordonate[2]));
            }
        }

        return vertexuriDinFisier;
    }
}
