using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Verhivetchii_Vadim_3131A
{
    // Definirea clasei Camera3DIsometric
    class Camera3DIsometric
    {
        // Declarații de variabile pentru poziția camerei, punctul de vizare și vectorul de orientare
        private Vector3 eye;         // Poziția camerei (punctul de vedere)
        private Vector3 target;      // Punctul de vizare către care este orientată camera
        private Vector3 up_vector;   // Vectorul de orientare "sus"

        // Unități de mișcare și de zoom pentru cameră
        private const int MOVEMENT_UNIT = 1;
        private const int ZOOM_UNIT = 4;

        // Constructor implicit ce definește o poziție și orientare inițială a camerei
        public Camera3DIsometric()
        {
            eye = new Vector3(200, 175, 25);    // Poziția camerei (x, y, z)
            target = new Vector3(0, 25, 0);    // Punctul de vizare (x, y, z)
            up_vector = new Vector3(0, 1, 0);  // Vectorul de orientare "sus" (x, y, z)
        }

        // Constructor care primește coordonatele pentru poziția camerei, punctul de vizare și vectorul de orientare
        public Camera3DIsometric(int _eyeX, int _eyeY, int _eyeZ, int _targetX, int _targetY, int _targetZ, int _upX, int _upY, int _upZ)
        {
            eye = new Vector3(_eyeX, _eyeY, _eyeZ);
            target = new Vector3(_targetX, _targetY, _targetZ);
            up_vector = new Vector3(_upX, _upY, _upZ);
        }

        // Constructor care primește vectori pentru poziția camerei, punctul de vizare și vectorul de orientare
        public Camera3DIsometric(Vector3 _eye, Vector3 _target, Vector3 _up)
        {
            eye = _eye;
            target = _target;
            up_vector = _up;
        }

        // Metodă pentru a seta camera folosind matricea LookAt și OpenGL
        public void SetCamera()
        {
            Matrix4 camera = Matrix4.LookAt(eye, target, up_vector); // Creează matricea de vizualizare
            GL.MatrixMode(MatrixMode.Modelview); // Setează modul matricei la modelview
            GL.LoadMatrix(ref camera); // Încarcă matricea de vizualizare în OpenGL
        }

        // Metode pentru a mișca camera în diferite direcții
        public void MoveRight()
        {
            eye = new Vector3(eye.X, eye.Y, eye.Z - MOVEMENT_UNIT);
            target = new Vector3(target.X, target.Y, target.Z - MOVEMENT_UNIT);
            SetCamera();
        }

        public void MoveLeft()
        {
            eye = new Vector3(eye.X, eye.Y, eye.Z + MOVEMENT_UNIT);
            target = new Vector3(target.X, target.Y, target.Z + MOVEMENT_UNIT);
            SetCamera();
        }

        public void MoveForward()
        {
            // Mișcare înainte pe axa X și Z
            eye = new Vector3(eye.X - MOVEMENT_UNIT, eye.Y, eye.Z);
            target = new Vector3(target.X - MOVEMENT_UNIT, target.Y, target.Z);
            SetCamera();
        }

        public void MoveBackward()
        {
            // Mișcare înapoi pe axa X și Z
            eye = new Vector3(eye.X + MOVEMENT_UNIT, eye.Y, eye.Z);
            target = new Vector3(target.X + MOVEMENT_UNIT, target.Y, target.Z);
            SetCamera();
        }

        public void MoveUp()
        {
            // Mișcare în sus pe axa Y
            eye = new Vector3(eye.X, eye.Y + MOVEMENT_UNIT, eye.Z);
            target = new Vector3(target.X, target.Y + MOVEMENT_UNIT, target.Z);
            SetCamera();
        }

        public void MoveDown()
        {
            // Mișcare în jos pe axa Y
            eye = new Vector3(eye.X, eye.Y - MOVEMENT_UNIT, eye.Z);
            target = new Vector3(target.X, target.Y - MOVEMENT_UNIT, target.Z);
            SetCamera();
        }

        // Metode pentru a face zoom in/out
        public void ZoomOut()
        {
            eye = new Vector3(eye.X + ZOOM_UNIT, eye.Y + ZOOM_UNIT, eye.Z);
            target = new Vector3(target.X + ZOOM_UNIT, target.Y + ZOOM_UNIT, target.Z);
            SetCamera();
        }

        public void ZoomIn()
        {
            eye = new Vector3(eye.X - ZOOM_UNIT, eye.Y - ZOOM_UNIT, eye.Z);
            target = new Vector3(target.X - ZOOM_UNIT, target.Y - ZOOM_UNIT, target.Z);
            SetCamera();
        }

        // Metode pentru a seta poziții de apropiere și depărtare
        public void Near()
        {
            eye = new Vector3(125, 100, 25);
            target = new Vector3(0, 25, 0);
            SetCamera();
        }

        public void Far()
        {
            eye = new Vector3(200, 175, 25);
            target = new Vector3(0, 25, 0);
            SetCamera();
        }
    }
}
