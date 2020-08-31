using Matrices;
using System;
using Vectors;

namespace ObjectComponents
{
    public class Color
    {
        public int R = 10;
        public int G = 11;
        public int B = 12;
        public int A = 13;

        public Color(int R, int G, int B, int A)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
    }

    public class Mesh
    {
        public Vector3[] points = new Vector3[0];
        public int[] indices = new int[0];
        public Vector3[] normals = new Vector3[0];
        public Color[] color = new Color[0];
        public Matrix ModelToWorld;

    }


    public class GameObject
    {
        public Vector3 position = Vector3.Zero();
        public Vector3 rotation = Vector3.Zero();
        public Vector3 scale = new Vector3(1, 1, 1);
        public Mesh ObjMesh = new Mesh();

        public Matrix Tran = new Matrix(4, 4);
        public Matrix RotZ = new Matrix(4, 4);
        public Matrix RotY = new Matrix(4, 4);
        public Matrix RotX = new Matrix(4, 4);
        public Matrix Scal = new Matrix(4, 4);

        public GameObject()
        {
            CalcTran();
            CalcRot();
            CalcScal();
        }

        private void CalcTran()
        {
            Tran.matrix = new float[][]{
                new float[]{1, 0, 0, position.x},
                new float[]{0, 1, 0, position.y},
                new float[]{0, 0, 1, position.z},
                new float[]{0, 0, 0, 1},
            };
        }

        private void CalcRot()
        {
            RotX.matrix = new float[][]{
                new float[]{0, 0, 1, 0},
                new float[]{(float)Math.Cos(rotation.x / 180 * (float)Math.PI), (float)Math.Sin(rotation.x / 180 * (float)Math.PI), 0, 0},
                new float[]{-(float)Math.Sin(rotation.x / 180 * (float)Math.PI), (float)Math.Cos(rotation.x / 180 * (float)Math.PI), 0, 0},
                new float[]{0, 0, 0, 1},
            };

            RotY.matrix = new float[][]{
                new float[]{-(float)Math.Sin(rotation.y / 180 * (float)Math.PI), (float)Math.Cos(rotation.y / 180 * (float)Math.PI), 0, 0},
                new float[]{0, 0, 1, 0},
                new float[]{(float)Math.Cos(rotation.y / 180 * (float)Math.PI), (float)Math.Sin(rotation.y / 180 * (float)Math.PI), 0, 0},
                new float[]{0, 0, 0, 1},
            };

            RotZ.matrix = new float[][]{
                new float[]{(float)Math.Cos(rotation.z / 180 * (float)Math.PI), (float)Math.Sin(rotation.z / 180 * (float)Math.PI), 0, 0},
                new float[]{-(float)Math.Sin(rotation.z / 180 * (float)Math.PI), (float)Math.Cos(rotation.z / 180 * (float)Math.PI), 0, 0},
                new float[]{0, 0, 1, 0},
                new float[]{0, 0, 0, 1},
            };
        }

        private void CalcScal()
        {
            Scal.matrix = new float[][]{
                new float[]{scale.x, 0, 0, 0},
                new float[]{0, scale.y, 0, 0},
                new float[]{0, 0, scale.z, 0},
                new float[]{0, 0, 0, 1},
            };
        }

        public Vector3 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                CalcTran();
                FindLocalToWorld();
            }
        }

        public Vector3 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                CalcScal();
                FindLocalToWorld();
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;

                if (rotation.x > 360)
                    rotation.x = rotation.x - 360;
                if (rotation.x < -360)
                    rotation.x = rotation.x + 360;

                if (rotation.y > 360)
                    rotation.y = rotation.y - 360;
                if (rotation.y < -360)
                    rotation.y = rotation.y + 360;

                if (rotation.z > 360)
                    rotation.z = rotation.z - 360;
                if (rotation.z < -360)
                    rotation.z = rotation.z + 360;
                CalcRot();
                FindLocalToWorld();
            }
        }


        public void Translate(Vector3 V)
        {
            Position = position + V;
        }

        public void Rotate(Vector3 V)
        {
            Rotation = rotation + V;
        }

        public void Rescale(Vector3 V)
        {
            scale = scale + V;
            CalcScal();
            FindLocalToWorld();
        }
        public void Rescale(float V)
        {
            scale = scale * V;
            CalcScal();
            FindLocalToWorld();
        }

        public void FindLocalToWorld()
        {
            ObjMesh.ModelToWorld = Tran * RotZ * RotY * RotX * Scal;
        }
    }
}





