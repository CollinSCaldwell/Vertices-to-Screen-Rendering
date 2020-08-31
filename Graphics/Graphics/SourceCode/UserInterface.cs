using ObjectComponents;
using System;
using Vectors;

namespace Engine
{
	class GameEngine
	{
		public GameObject[] ObjectArray;


		private float rotation = 0;

		private GameObject B = new GameObject();
		private GameObject GO = new GameObject();

		public GameEngine()
		{
			ObjectArray = new GameObject[] { GO, B };


			GO.Position = new Vector3(-2, 0, 30);
			GO.ObjMesh.points = new Vector3[]{
				new Vector3(-1, 1, 1),
				new Vector3(1, 1, 1),
				new Vector3(-1, -1, 1),
				new Vector3(-1, -1, -1),
				new Vector3(-1, 1, -1),
				new Vector3(1, 1, -1),
				new Vector3(1, -1, 1),
				new Vector3(1, -1, -1),
			};

			GO.ObjMesh.indices = new int[]{
				0, 1, 2,
				1, 6, 2,
				5, 6, 1,
				7, 6, 5,
				3, 7, 5,
				3, 5, 4,
				3, 4, 2,
				4, 0, 2,
				5, 1, 4,
				1, 0, 4,
				7, 6, 3,
				6, 2, 3
			};



			B.Position = new Vector3(2, 0, 15);
			B.ObjMesh.points = new Vector3[]{
				new Vector3(0, 1, 0),
				new Vector3(1, 0, -1),
				new Vector3(1, 0, 1),
				new Vector3(-1, 0, 1),
				new Vector3(-1, 0, -1),
				new Vector3(0, -1, 0)
			};

			B.ObjMesh.indices = new int[]{
				1, 0, 2,
				2, 0, 3,
				3, 4, 0,
				4, 0, 1,
				1, 5, 2,
				2, 5, 3,
				3, 4, 5,
				4, 5, 1,
			};

		}




		public void GameUpdate()
		{
			B.Position = new Vector3(5 * (float)Math.Sin(rotation / 180 * Math.PI), (float)Math.Sin(rotation / 45 * Math.PI)*2, 35 + 20 * (float)Math.Cos(rotation / 180 * Math.PI));
			GO.Rotate(new Vector3(10, 0, 0));
			rotation += 5;


		}

		public void FrameUpdate()
		{


		}

	}
}
