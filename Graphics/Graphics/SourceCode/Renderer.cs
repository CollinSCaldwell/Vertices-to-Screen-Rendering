using Cameras;
using Matrices;
using ObjectComponents;
using System;
using Vectors;

namespace Render
{
	public class Renderer
	{
		public int[,] BackBuffer;
		public int Width;
		public int Height;


		public float fieldOfViewY;
		public float aspectRatio;
		public float znearPlane;
		public float zfarPlane;

		public Camera Cam;

		public GameObject[] ActiveObjects;

		private Matrix projectionM;

		private Matrix ModelWorld = new Matrix(4, 4);


		public Renderer(int length, int height)
		{
			BackBuffer = new int[height, length];
			Height = height;
			Width = length;

			fieldOfViewY = 90f;
			znearPlane = .1f;
			zfarPlane = 10;
			aspectRatio = (float)Height / Width;
			float tanRelation = 1f / (float)Math.Tan(fieldOfViewY / 2 / 180 * (float)Math.PI);


			projectionM = new Matrix(4, 4);
			projectionM.matrix = new float[][]{
				new float[]{aspectRatio * tanRelation, 0, 0, 0},
				new float[]{0, -tanRelation, 0, 0},
				new float[]{0, 0, zfarPlane/(zfarPlane - znearPlane), 1},
				new float[]{0, 0, -(znearPlane*zfarPlane)/(zfarPlane - znearPlane), 0},
			};

		}



		private void ClearBackBuffer()
		{
			for (int i = 0; i < Height; i++)
				for (int j = 0; j < Width; j++)
					BackBuffer[i, j] = 0;
		}


		private void DrawTriangle(Vector3 V1, Vector3 V2, Vector3 V3)
		{
			Vector3 top, middle, bottom;
			V1.x = (int)V1.x;
			V1.y = (int)V1.y;

			V2.x = (int)V2.x;
			V2.y = (int)V2.y;

			V3.x = (int)V3.x;
			V3.y = (int)V3.y;


			if (V1.y > V2.y)
			{

				middle = V2;
				V2 = V1;
				V1 = middle;
			}

			if (V2.y > V3.y)
			{
				middle = V2;
				V2 = V3;
				V3 = middle;
			}

			if (V1.y > V2.y)
			{
				middle = V2;
				V2 = V1;
				V1 = middle;
			}

			top = V1;
			middle = V2;
			bottom = V3;

			float dV1V2, dV1V3;

			if (middle.y - top.y > 0)
			{
				dV1V2 = (middle.x - top.x) / (middle.y - top.y);
			}
			else
			{
				dV1V2 = 0;
			}
			if (bottom.y - top.y > 0)
			{
				dV1V3 = (bottom.x - top.x) / (bottom.y - top.y);
			}
			else
			{
				dV1V3 = 0;
			}


			if (dV1V2 > dV1V3)
			{
				for (int y = (int)top.y; y <= (int)bottom.y; y++)
				{
					if (y < middle.y)
					{
						ScanLine(y, top, bottom, top, middle);
					}
					else
					{
						ScanLine(y, top, bottom, middle, bottom);
					}
				}
			}
			else
			{
				for (int y = (int)top.y; y <= (int)bottom.y; y++)
				{
					if (y < middle.y)
					{
						ScanLine(y, top, middle, top, bottom);
					}
					else
					{
						ScanLine(y, middle, bottom, top, bottom);
					}
				}
			}
		}

		private void ScanLine(int y, Vector3 pa, Vector3 pb, Vector3 pc, Vector3 pd)
		{
			var gradient1 = pa.y != pb.y ? (y - pa.y) / (pb.y - pa.y) : 1;
			var gradient2 = pc.y != pd.y ? (y - pc.y) / (pd.y - pc.y) : 1;

			int sx = (int)SingleInterpolate(pa.x, pb.x, gradient1);
			int ex = (int)SingleInterpolate(pc.x, pd.x, gradient2);

			DrawLine(sx, y, ex, y);
		}
		private void DrawLine(float x1, float y1, float x2, float y2)
		{
			for (int i = 0; i < 50; i++)
			{
				Vector3 V = Interpolate(x1, y1, x2, y2, i / 50f);
				DrawPoint(V.x, V.y, new Color(10, 10, 10, 10));
			}
		}
		private void DrawPoint(float x, float y, Color C)
		{
			if (x >= 0 && x < Width && y >= 0 && y < Height)
			{
				BackBuffer[(int)y, (int)x] = C.R;
			}
		}

		public ref int[,] RenderScene()
		{
			ClearBackBuffer();


			foreach (GameObject O in ActiveObjects)
			{

				Vector3[] Projected = new Vector3[O.ObjMesh.points.Length];


				for (int i = 0; i < O.ObjMesh.points.Length; i++)
				{
					Matrix Coordinates = Matrix.Vector3ToMatrix(O.ObjMesh.points[i]);

					Matrix Resultant = new Matrix(4, 1);
					Resultant = projectionM * Cam.LookAtM * O.ObjMesh.ModelToWorld * Coordinates;

					Resultant = Resultant / Resultant[3][0];
					Projected[i] = new Vector3(((Resultant[0][0] + 1) * Width / 2), ((Resultant[1][0] + 1) * Height / 2), Resultant[2][0]);
				}

				for (int i = 0; i < O.ObjMesh.indices.Length; i += 3)
				{
					DrawTriangle(Projected[O.ObjMesh.indices[i]], Projected[O.ObjMesh.indices[i + 1]], Projected[O.ObjMesh.indices[i + 2]]);
				}

			}
			return ref BackBuffer;
		}







		private static Vector3 Interpolate(float x1, float y1, float x2, float y2, float percent)
		{
			Vector3 V = Vector3.Zero();

			V.x = ((1 - percent) * x1) + (percent * x2);
			V.y = ((1 - percent) * y1) + (percent * y2);

			return V;
		}

		private static float SingleInterpolate(float x1, float x2, float percent)
		{
			return ((1 - percent) * x1) + (percent * x2);
		}


	}
}