using System;
using System.Collections.Generic;
using Xunit;

namespace PokerComTDD.Testes
{
    public class ConversorDeCartaTeste
    {
        [Theory]
        [InlineData("2O", 2)]
        [InlineData("7C", 7)]
        [InlineData("10E", 10)]
        [InlineData("VP", 11)]
        [InlineData("AO", 14)]
        public void DeveConverterUmaCartaParaSeuValor(string cartaEmString, double valorEsperado)
        {
            var carta = ConversorDeCarta.Converter(cartaEmString);
            
            Assert.Equal(valorEsperado, carta.Valor);
        }
    }

    public static class ConversorDeCarta
    {
        public static Carta Converter(string carta)
        {
            var naipeDaCarta = carta.Substring(carta.Length - 1, 1);
            var valorDaCartaEmDescricao = carta.Replace(naipeDaCarta, string.Empty);
            var valorDaCarta = ObterValorDaCarta(valorDaCartaEmDescricao);
            
            return new Carta(valorDaCarta, naipeDaCarta);
        }      

        private static int ObterValorDaCarta(string valorDaCartaEmDescricao)
        {
            if (!int.TryParse(valorDaCartaEmDescricao, out var valorDaCarta))
            {
                switch (valorDaCartaEmDescricao)
                {
                    case "V":
                        valorDaCarta = 11;
                        break;
                    case "D":
                        valorDaCarta = 12;
                        break;
                    case "R":
                        valorDaCarta = 13;
                        break;
                    case "A":
                        valorDaCarta = 14;
                        break;
                }
            }

            return valorDaCarta;
        }
    }

    public class Carta
    {
        public Carta(int valor, string naipe)
        {
            Valor = valor;
            Naipe = naipe;
        }

        public int Valor { get; private set; }
        public string Naipe { get; private set; }
    }
}