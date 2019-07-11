/*
  Autor: Dalton Solano dos Reis
 */
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace cgunit4
{
  internal class Piso : Objeto
  {
    private int textureId;
    private Ponto4D a, b, c, d;
  
    public Piso(int linha, int y, int coluna, int textureId)
    {
      a = new Ponto4D(linha+1, 0, coluna+1);
      b = new Ponto4D(linha  , 0, coluna+1);
      c = new Ponto4D(linha+1, 0, coluna);
      d = new Ponto4D(linha  , 0, coluna);

      this.textureId = textureId;
    }

    //TODO: entender o uso da keyword new ... e replicar para os outros projetos
    new public void Desenha()
    {
      GL.BindTexture(TextureTarget.Texture2D, this.textureId);
      GL.Begin(PrimitiveType.Quads);
        // Face de cima
        GL.Color3(0.93, 0.76, 0.62);
        GL.Normal3(0, 1, 0);
        GL.TexCoord2(b.X, b.Z); GL.Vertex3(b.X, b.Y, b.Z);
        GL.TexCoord2(a.X, a.Z); GL.Vertex3(a.X, a.Y, a.Z);
        GL.TexCoord2(c.X, c.Z); GL.Vertex3(c.X, c.Y, c.Z);
        GL.TexCoord2(d.X, d.Z); GL.Vertex3(d.X, d.Y, d.Z);
      GL.End();
    }
  }
}