using System;

namespace Vectors
{
	public class Vector3
	{
		public float x;
		public float y;
		public float z;

		public Vector3(float X, float Y, float Z)
		{
			x = X;
			y = Y;
			z = Z;
		}

		public static Vector3 operator +(Vector3 V1, Vector3 V2)
		{

			return new Vector3(V1.x + V2.x, V1.y + V2.y, V1.z + V2.z);
		}

		public static Vector3 operator -(Vector3 V1, Vector3 V2)
		{

			return new Vector3(V1.x - V2.x, V1.y - V2.y, V1.z - V2.z);
		}

		public static Vector3 operator *(Vector3 V1, float V2)
		{

			return new Vector3(V1.x * V2, V1.y * V2, V1.z * V2);
		}


		public static Vector3 operator *(float V2, Vector3 V1)
		{

			return new Vector3(V1.x * V2, V1.y * V2, V1.z * V2);
		}

		public static Vector3 operator /(Vector3 V1, float V2)
		{

			return new Vector3(V1.x / V2, V1.y / V2, V1.z / V2);
		}

		public static Vector3 Normal(Vector3 V)
		{
			float dividend = (float)Math.Pow(Math.Pow(V.x, 2) + Math.Pow(V.y, 2) + Math.Pow(V.z, 2), .5f);

			if (dividend == 0)
				return Vector3.Zero();
			return V / dividend;
		}

		public static Vector3 Cross(Vector3 V1, Vector3 V2)
		{
			Vector3 V = Vector3.Zero();
			V.x = V1.y * V2.z - V1.z * V2.y;
			V.y = V1.z * V2.x - V1.x * V2.z;
			V.z = V1.x * V2.y - V1.y * V2.x;
			return V;
		}

		public static float Dot(Vector3 V1, Vector3 V2)
		{
			return V1.x * V2.x + V1.y * V2.y + V1.z * V2.z;
		}

		public static Vector3 Zero()
		{
			return new Vector3(0, 0, 0);
		}

		public static void PrintVector(Vector3 V)
		{
			Console.WriteLine(V.x + ", " + V.y + ", " + V.z);
		}
	}
}