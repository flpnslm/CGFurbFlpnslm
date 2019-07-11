/*
  Autor: Dalton Solano dos Reis
*/

using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Collections.Generic;
using OpenTK.Input;
using System.Timers;
using System.Drawing.Imaging;

namespace cgunit4
{
  /// <summary>
  /// Classe que define o mundo virtual
  /// Padrão Singleton
  /// </summary>
  /// 

  class Mundo
  {
    public static Mundo instance = null;
    private List<Cubo> snake = new List<Cubo>();
    private int[] snakeHead;
    private int snakeValue;
    private string currentMovement { get; set; }
    private int[,] matriz;
    private Maca apple;
    private Random random = new Random();
    private bool moveu = false;
    private Timer aTimer;
    public int dirtTextureId { get; set; }
    public int snakeTextureId { get; set; }
    public int appleTextureId { get; set; }

    private Mundo()
    {
      this.startMatrix();
      this.startTimer();
    }

    public static Mundo getInstance()
    {
      if (instance == null)
        instance = new Mundo();
      return instance;
    }

    public void Destroy()
    {
      instance = null;
    }

    public void Desenha()
    {
      SRU3D();
      this.drawFloor();
      this.drawSnake();
      this.drawApple();
    }

    private void startMatrix()
    {
      this.snakeValue = 3;
      this.matriz = new int[20, 20]
        {
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
          {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };
        snakeHead = new int[2] {19, 9};
        this.currentMovement = "L";
    }

    private void startTimer()
    {
      setTimer(200);
    }

    private void setTimer(int interval)
    {
      aTimer = new System.Timers.Timer(interval);
      aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
      aTimer.Interval = interval;
      aTimer.Enabled = true;
    }

    private void drawFloor()
    {
      for (int linha = 0; linha < this.matriz.GetLength(0); linha++)
      {
          for (int coluna = 0; coluna < this.matriz.GetLength(1); coluna++)
          {
            Piso piso = new Piso(linha, 1, coluna, this.dirtTextureId);
            piso.Desenha();
          }
      }
    }

    public void OnTimedEvent(object source, ElapsedEventArgs e)
    {
      try
      {
        switch (this.currentMovement)
        {
          case "U":
            this.snakeHead[1]--;
            break;
          case "R":
            this.snakeHead[0]++;
            break;
          case "D":
            this.snakeHead[1]++;
            break;
          case "L":
            this.snakeHead[0]--;
            break;
        }
        this.WithdrawSnakeValue();
        if (this.matriz[this.snakeHead[0], this.snakeHead[1]] > 0)
        {
          this.aTimer.Stop();
          Console.WriteLine("Você perdeu!");
        }
        else if (this.matriz[this.snakeHead[0], this.snakeHead[1]] < 0)
        {
          this.matriz[this.snakeHead[0], this.snakeHead[1]] = this.snakeValue;
          this.drawApple();
          this.snakeValue++;
        }
        else
        {
          this.matriz[this.snakeHead[0], this.snakeHead[1]] = this.snakeValue;
        }
      }
      catch (System.Exception)
      {
        Console.WriteLine("Você perdeu");
        this.aTimer.Stop();
      }
      this.moveu = false;
    }
    private void drawSnake()
    {
      this.snake.Clear();
      for (int coluna = 0; coluna < this.matriz.GetLength(1); coluna++)
      {
          for (int linha = 0; linha < this.matriz.GetLength(0); linha++)
          {
            if (this.matriz[linha, coluna] > 0)
            {
              Cubo body = new Cubo(linha, coluna, this.snakeTextureId);
              this.snake.Add(body);
            }
          }
      }
      foreach (Cubo body in this.snake)
      {
          body.Desenha();
      }
    }

    private void drawApple()
    {
      int rLinha = this.random.Next(0, 19);
      int rColuna = this.random.Next(0, 19);

      while (this.matriz[rLinha, rColuna] != 0)
      {
        rLinha = this.random.Next(0, 19);
        rColuna = this.random.Next(0, 19);
      }

      if (this.apple == null)
      {
        this.matriz[rLinha, rColuna] = -1;
      }
      else if (this.matriz[this.apple.linha, this.apple.coluna] >= 0)
      {
        this.matriz[rLinha, rColuna] = -1;
      }
      for (int coluna = 0; coluna < this.matriz.GetLength(1); coluna++)
      {
          for (int linha = 0; linha < this.matriz.GetLength(0); linha++)
          {
            if (this.matriz[linha, coluna] < 0)
            {
              this.apple = new Maca(linha, coluna, this.appleTextureId);
            }
          }
      }
      this.apple.Desenha();
    }

    public void Reset()
    {
      this.startMatrix();
    }

    public void WithdrawSnakeValue()
    {
      for (int linha = 0; linha < this.matriz.GetLength(0); linha++)
      {
          for (int coluna = 0; coluna < this.matriz.GetLength(1); coluna++)
          {
            if (this.matriz[linha, coluna] > 0)
            {
              this.matriz[linha,coluna]--;
            }         
          }
      }
    }

    private void move(string movement)
    {
      this.currentMovement = movement;
    }
    public void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (!this.moveu)
      {
        switch (e.Key)
        {
            case Key.Up:
              if (this.currentMovement != "D" & !this.moveu)
              {
                this.move("U");
                this.moveu = true;
              }
              break;
            case Key.Right:
              if (this.currentMovement != "L" & !this.moveu)
              {
                this.move("R");
                this.moveu = true;
              }
              break;
            case Key.Down:
              if (this.currentMovement != "U" & !this.moveu)
              {
              this.move("D");
                this.moveu = true;
              }
              break;
            case Key.Left:
              if (this.currentMovement != "R" & !this.moveu)
              {
                this.move("L");
                this.moveu = true;
              }
              break;
            default:
              Console.WriteLine("NoOp");
              break;
        }
      }
    }

    private void SRU3D()
    {
      /*
      GL.LineWidth(1);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(Color.Red);
      GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
      GL.Color3(Color.Green);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
      GL.Color3(Color.Blue);
      GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
      GL.End();
      */
    }
  }

}