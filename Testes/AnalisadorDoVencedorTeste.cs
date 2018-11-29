using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokerComTDD.Testes
{
    public class AnalisadorDoVencedorTeste
    {
        [Theory]
        [InlineData("2O,3C,4E,6P", "2P,3O,4P,5P", "Primeiro Jogador")]
        [InlineData("2O,3C,4E,6P", "2P,3O,4P,7P", "Segundo Jogador")]
        [InlineData("2O,3C,4E,10P", "2P,3O,4P,7P", "Primeiro Jogador")]
        [InlineData("2O,3C,4E,RP", "2P,3O,4P,AP", "Segundo Jogador")]
        public void DeveAnalisarVencedorQuandoEsteTerMaiorCarta(string cartasDoPrimeiroJogadorEmString, string cartasDoSegundoJogadorEmString, string jogadorVencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorEmString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorEmString.Split(',').ToList();
            var analisador = new AnalisadorDoVencedor();

            var jogadorVencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            Assert.Equal(jogadorVencedorEsperado, jogadorVencedor);
        }
    }

    internal class AnalisadorDoVencedor
    {
        public AnalisadorDoVencedor()
        {
        }

        internal object Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var maiorCartaDoPrimeiroJogador = cartasDoPrimeiroJogador.Select(carta => ConverterParaValorDaCarta(carta)).OrderBy(valor => valor).Last();
            var maiorCartaDoSegundoJogador = cartasDoSegundoJogador.Select(carta => ConverterParaValorDaCarta(carta)).OrderBy(valor => valor).Last();

            return maiorCartaDoPrimeiroJogador > maiorCartaDoSegundoJogador ?
                "Primeiro Jogador" : "Segundo Jogador";
        }

        private int ConverterParaValorDaCarta(string carta)
        {
            if(!int.TryParse(carta.Substring(0, carta.Length - 1), out var valor))
            {
                switch (carta)
                {
                    case "V":
                        valor = 11;
                        break;
                    case "D":
                        valor = 12;
                        break;
                    case "R":
                        valor = 13;
                        break;
                    case "A":
                        valor = 14;
                        break;
                }
            }

            return valor;
        }
    }
}