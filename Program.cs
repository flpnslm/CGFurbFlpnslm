/*
  Autor: Dalton Solano dos Reis
*/

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;
using System.Drawing.Imaging;

namespace cgunit4
{
  class Render : GameWindow
  {
    Mundo mundo = Mundo.getInstance();
    Camera camera = new Camera();
    Vector3 eye = Vector3.Zero, target = Vector3.Zero, up = Vector3.UnitY;
    private bool ligaLuz = true;

    public Render(int width, int height) : base(width, height) { }

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GL.ClearColor(Color.Green);                        // Aqui é melhor
      GL.Enable(EnableCap.DepthTest);                   // NOVO
      GL.Enable(EnableCap.CullFace);                    // NOVO
      GL.Enable(EnableCap.Texture2D);
      GL.Enable(EnableCap.Lighting);
      GL.Enable(EnableCap.Light0);
      getDirtTexture();
      getSnakeTexture();
      getAppleTexture();
    }
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      GL.Viewport(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);

      Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 50.0f);
      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadMatrix(ref projection);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
    }
    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // GL.Clear(ClearBufferMask.ColorBufferBit);

      eye.X = 10; eye.Y = 40; eye.Z = 11;
      target.X = 10; target.Y = -20; target.Z = 9;
      Matrix4 modelview = Matrix4.LookAt(eye, target, up);
      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadMatrix(ref modelview);

      mundo.Desenha();
      this.god();

      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      /*if (e.Key == Key.R)
      {
        GL.Disable(EnableCap.Lighting);
        GL.Disable(EnableCap.Light0);
        mundo.Destroy();
        mundo = Mundo.getInstance();
        GL.Enable(EnableCap.Lighting);
        GL.Enable(EnableCap.Light0);
      }
      */
      mundo.OnKeyDown(e);
    }

    private void getDirtTexture()
    {
      int textureId = 0;
      Bitmap bitmap = new Bitmap("img/dirt.bmp");
      GL.GenTextures(1, out textureId);
      GL.BindTexture(TextureTarget.Texture2D, textureId);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
          ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      bitmap.UnlockBits(data);

      this.mundo.dirtTextureId = textureId;
    }

    private void getSnakeTexture()
    {
      int textureId;
      Bitmap bitmap = new Bitmap("img/snake.bmp");
      GL.GenTextures(1, out textureId);
      GL.BindTexture(TextureTarget.Texture2D, textureId);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
          ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      bitmap.UnlockBits(data);

      this.mundo.snakeTextureId = textureId;
    }

    private void getAppleTexture()
    {
      int textureId;
      Bitmap bitmap = new Bitmap("img/apple.bmp");
      GL.GenTextures(1, out textureId);
      GL.BindTexture(TextureTarget.Texture2D, textureId);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
      GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

      BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
          ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

      GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
          OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

      bitmap.UnlockBits(data);

      this.mundo.appleTextureId = textureId;
    }

    private void god()
    {
      // Enable Light 0 and set its parameters.
      GL.Light(LightName.Light0, LightParameter.Position, new float[] { 10.0f, 5.0f, -1.5f });
      GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 20.3f, 20.3f, 20.3f, 2.0f });
      GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 10000.0f, 10000.0f, 10000.0f, 255.0f });
      GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 10000.0f, 10000.0f, 10000.0f, 10000 });
      GL.Light(LightName.Light0, LightParameter.SpotExponent, new float[] { 255.0f, 255.0f, 255.0f, 255 });
      GL.LightModel(LightModelParameter.LightModelAmbient, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
      GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
      GL.LightModel(LightModelParameter.LightModelLocalViewer, 1);
    }

  }

  class Program
  {
    static void Main(string[] args)
    {
      Render window = new Render(600, 600);
      window.Run();
      window.Run(1.0 / 60.0);
    }
  }

}
