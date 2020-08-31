using Cameras;
using Engine;
using Graphics;
using Render;
using System.Drawing;
using System.Windows.Forms;
using Vectors;

public class Main
{
	public int Height;
	public int Width;

	public int[,] bmp;

	private Bitmap bitmap;
	private GameEngine RunningEngine;
	private Camera cam;
	private Renderer Rend;
	public Main(int height, int width)
	{
		Height = height;
		Width = width;

		bitmap = new Bitmap(Width, Height);
		RunningEngine = new GameEngine();

		cam = new Camera();
		Rend = new Renderer(Width, Height)
		{
			Cam = cam,
			ActiveObjects = RunningEngine.ObjectArray
		};
	}


	public void CallToRun(ref PictureBox displayPicture, Form1 F1)
	{

		RunningEngine.FrameUpdate();
		RunningEngine.GameUpdate();
		cam.Rotate(new Vector3(0, 0, 1));
		bmp = Rend.RenderScene();

		for (int j = 0; j < Height; j++)
		{
			for (int i = 0; i < Width; i++)
			{
				if (bmp[j, i] == 0)
					bitmap.SetPixel(i, j, System.Drawing.Color.Black);
				else
					bitmap.SetPixel(i, j, System.Drawing.Color.White);
			}
		}

		F1.Refresh();
		F1.Update();
		displayPicture.Image = bitmap;
		F1.Update();

	}

}



