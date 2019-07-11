/*
  Autor: Dalton Solano dos Reis
 */
using OpenTK.Graphics.OpenGL;

namespace cgunit4
{
  internal class Maca : Objeto
  {
    public int linha, coluna, textureId;
    private Ponto4D a, b, c, d, e, f, g, h;
    public Maca(int linha, int coluna, int textureId)
    {
      this.linha = linha;
      this.coluna = coluna;
      this.textureId = textureId;
      a = new Ponto4D(linha  , 0.3f, coluna+1);
      b = new Ponto4D(linha+1, 0.3f, coluna+1);
      c = new Ponto4D(linha+1, 1.3f, coluna+1);
      d = new Ponto4D(linha  , 1.3f, coluna+1);
      e = new Ponto4D(linha  , 0.3f, coluna);
      f = new Ponto4D(linha+1, 0.3f, coluna);
      g = new Ponto4D(linha+1, 1.3f, coluna);
      h = new Ponto4D(linha  , 1.3f, coluna);
    }

    new public void Desenha()
    {
      GL.BindTexture(TextureTarget.Texture2D, this.textureId);
      GL.Begin(PrimitiveType.Quads);

        // Face da frente
        GL.Color3(1, 0, 0);
        GL.Normal3(0, 0, 1);
        GL.TexCoord2(c.X, c.Z); GL.Vertex3(c.X, c.Y, c.Z);
        GL.TexCoord2(d.X, d.Z); GL.Vertex3(d.X, d.Y, d.Z);
        GL.TexCoord2(a.X, a.Z); GL.Vertex3(a.X, a.Y, a.Z);
        GL.TexCoord2(b.X, b.Z); GL.Vertex3(b.X, b.Y, b.Z);
        
        // Face de tras
        GL.Color3(1, 0, 0);
        GL.Normal3(0, 0, -1);
        GL.TexCoord2(f.X, f.Z); GL.Vertex3(f.X, f.Y, f.Z);
        GL.TexCoord2(e.X, e.Z); GL.Vertex3(e.X, e.Y, e.Z);
        GL.TexCoord2(h.X, h.Z); GL.Vertex3(h.X, h.Y, h.Z);
        GL.TexCoord2(g.X, g.Z); GL.Vertex3(g.X, g.Y, g.Z);
        
        // Face da esquerda
        GL.Color3(0.37, 0.53, 0.2);
        GL.Normal3(-1, 0, 0);
        GL.TexCoord2(a.X, a.Z); GL.Vertex3(a.X, a.Y, a.Z);
        GL.TexCoord2(d.X, d.Z); GL.Vertex3(d.X, d.Y, d.Z);
        GL.TexCoord2(h.X, h.Z); GL.Vertex3(h.X, h.Y, h.Z);
        GL.TexCoord2(e.X, e.Z); GL.Vertex3(e.X, e.Y, e.Z);

        // Face da direita
        GL.Color3(1, 0, 0);
        GL.Normal3(1, 0, 0);
        GL.TexCoord2(g.X, g.Z); GL.Vertex3(g.X, g.Y, g.Z);
        GL.TexCoord2(c.X, c.Z); GL.Vertex3(c.X, c.Y, c.Z);
        GL.TexCoord2(b.X, b.Z); GL.Vertex3(b.X, b.Y, b.Z);
        GL.TexCoord2(f.X, f.Z); GL.Vertex3(f.X, f.Y, f.Z);

        // Face de cima
        GL.Color3(1, 0, 0);
        GL.Normal3(0, 1, 0);
        GL.TexCoord2(d.X, d.Z); GL.Vertex3(d.X, d.Y, d.Z);
        GL.TexCoord2(c.X, c.Z); GL.Vertex3(c.X, c.Y, c.Z);
        GL.TexCoord2(g.X, g.Z); GL.Vertex3(g.X, g.Y, g.Z);
        GL.TexCoord2(h.X, h.Z); GL.Vertex3(h.X, h.Y, h.Z);

        // Face de baixo
        GL.Color3(1, 0, 0);
        GL.Normal3(0, -1, 0);
        GL.TexCoord2(a.X, a.Z); GL.Vertex3(a.X, a.Y, a.Z);
        GL.TexCoord2(b.X, b.Z); GL.Vertex3(b.X, b.Y, b.Z);
        GL.TexCoord2(f.X, f.Z); GL.Vertex3(f.X, f.Y, f.Z);
        GL.TexCoord2(e.X, e.Z); GL.Vertex3(e.X, e.Y, e.Z);

      GL.End();
    }
  }
}