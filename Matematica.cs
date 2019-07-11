/*
  Autor: Dalton Solano dos Reis
*/

using System;

namespace cgunit4
{
  /// <summary>
  /// Classe com funções matemáticas.
  /// </summary>
  public class Matematica {
    /// <summary>
    /// Função para calcular um ponto sobre o perímetro de um círculo informando um ângulo e raio.
    /// </summary>
    /// <param name="angulo"></param>
    /// <param name="raio"></param>
    /// <returns></returns>
    public Ponto4D ptoCirculo(double angulo, double raio) {
      Ponto4D pto = new Ponto4D();
      pto.X = (raio * Math.Cos(Math.PI * angulo / 180.0));
      pto.Y = (raio * Math.Sin(Math.PI * angulo / 180.0));
      pto.Z = 0;
      return(pto);
    }

    public double ptoCirculoSimétrico(double raio) {
      return (raio * Math.Cos(Math.PI * 45 / 180.0));
    }

  }
}