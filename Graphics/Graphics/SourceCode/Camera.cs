using Matrices;
using System;
using Vectors;

namespace Cameras
{
	public class Camera
	{
		private Vector3 position = Vector3.Zero();
		private Vector3 rotation = Vector3.Zero();
		private Vector3 forward = Vector3.Zero();
		private Vector3 up = Vector3.Zero();

		public Matrix LookAtM = new Matrix(4, 4);


		public Camera()
		{
			CalcUpFor();
			LookAtMatrix();
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

				LookAtMatrix();
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

				CalcUpFor();
				LookAtMatrix();
			}
		}

		public Vector3 Forward
		{
			get { return forward; }
			set { }
		}

		public Vector3 Up
		{
			get { return up; }
			set { }
		}

		public void Translate(Vector3 V)
		{
			Position = position + V;
		}

		public void Rotate(Vector3 V)
		{
			Rotation = Rotation + V;
		}

		private void CalcUpFor()
		{
			forward = new Vector3((float)Math.Sin(rotation.y * Math.PI / 180) * (float)Math.Cos(rotation.x * Math.PI / 180), -(float)Math.Sin(rotation.x * Math.PI / 180), (float)Math.Cos(rotation.y * Math.PI / 180) * (float)Math.Cos(rotation.x * Math.PI / 180));
			up = Vector3.Cross(forward, Vector3.Cross(forward, new Vector3(0, 1, 0)));
		}

		private void LookAtMatrix()
		{
			Vector3 Zaxis = Vector3.Normal(forward - position);
			Vector3 Xaxis = Vector3.Normal(Vector3.Cross(up, Zaxis));
			Vector3 Yaxis = Vector3.Cross(Zaxis, Xaxis);

			LookAtM.matrix = new float[][]{
				new float[]{Xaxis.x, Yaxis.x, Zaxis.x, 0},
				new float[]{Xaxis.y, Yaxis.y, Zaxis.y, 0},
				new float[]{Xaxis.z, Yaxis.z, Zaxis.z, 0},
				new float[]{-Vector3.Dot(Xaxis, position), -Vector3.Dot(Yaxis, position), -Vector3.Dot(Zaxis, position), 1},
			};
		}

	}
}