using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PokerComTDD.Testes
{
    public class AnalisadorDeVencedorTeste
    {
        [Theory]
        [InlineData("2O,4C,3P,6C,VP", "3O,5C,2E,9C,AP", "Segundo Jogador")]
        [InlineData("2O,4C,3P,6C,VP", "3O,5C,2E,9C,4P", "Primeiro Jogador")]
        [InlineData("2O,4C,3P,6C,VP", "3O,5C,2E,9C,VO", "Segundo Jogador")]
        public void DeveAnalisarVencedorQuandoOMesmoTerMaiorCarta(
            string cartasDoPrimeiroJogadorEmString, 
            string cartasDoSegundoJogadorEmString,
            string jogadorVencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorEmString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorEmString.Split(',').ToList();
            var analisador = new AnalisadorDeVencedor();

            var jogadorVencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);
            
            Assert.Equal(jogadorVencedorEsperado, jogadorVencedor);
        }

        [Theory]
        [InlineData("2O,4C,2P,6C,VP", "3O,5C,2E,9C,AP", "Primeiro Jogador")]
        [InlineData("3O,5C,2E,9C,AP", "2O,4C,2P,6C,VP", "Segundo Jogador")]
        public void DeveAnalisarVencedorQuandoOMesmoTerDuasCartasDoMesmoNumero(
            string cartasDoPrimeiroJogadorEmString, 
            string cartasDoSegundoJogadorEmString,
            string jogadorVencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorEmString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorEmString.Split(',').ToList();
            var analisador = new AnalisadorDeVencedor();

            var jogadorVencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            Assert.Equal(jogadorVencedorEsperado, jogadorVencedor);
        }

        [Theory]
        [InlineData("3O,5C,3E,9C,AP", "4O,4C,4P,6C,VP", "Segundo Jogador")]
        public void DeveAnalisarVencedorDeDoisJogadoresComDuasCartasDeMesmoValorAnalisandoOJogadorComCartaMaisAlta(
            string cartasDoPrimeiroJogadorEmString, 
            string cartasDoSegundoJogadorEmString,
            string jogadorVencedorEsperado)
        {
            var cartasDoPrimeiroJogador = cartasDoPrimeiroJogadorEmString.Split(',').ToList();
            var cartasDoSegundoJogador = cartasDoSegundoJogadorEmString.Split(',').ToList();
            var analisador = new AnalisadorDeVencedor();

            var jogadorVencedor = analisador.Analisar(cartasDoPrimeiroJogador, cartasDoSegundoJogador);

            Assert.Equal(jogadorVencedorEsperado, jogadorVencedor);
        }
    }

    internal class AnalisadorDeVencedor
    {
        public AnalisadorDeVencedor()
        {
        }

        internal string Analisar(List<string> cartasDoPrimeiroJogador, List<string> cartasDoSegundoJogador)
        {
            var cartasDuplicadasDoPrimeiroJogador =
                cartasDoPrimeiroJogador
                .Select(carta => ConversorDeCarta.Converter(carta))
                .GroupBy(carta => carta.Valor)
                .Where(grupo => grupo.Count() > 1);            

            var cartasDuplicadasDoSegundoJogador =
                cartasDoSegundoJogador
                .Select(carta => ConversorDeCarta.Converter(carta))
                .GroupBy(carta => carta.Valor)
                .Where(grupo => grupo.Count() > 1);

            if(cartasDuplicadasDoPrimeiroJogador.Any() && cartasDuplicadasDoSegundoJogador.Any())
            {
                var maiorCartaDoPrimeiroJogador = cartasDuplicadasDoPrimeiroJogador.Select(carta => carta.Key).Max();
                var maiorCartaDoSegundoJogador = cartasDuplicadasDoSegundoJogador.Select(carta => carta.Key).Max();
                
                return maiorCartaDoPrimeiroJogador > maiorCartaDoSegundoJogador ?
                    "Primeiro Jogador" : "Segundo Jogador";
            }
            
            else if(cartasDuplicadasDoPrimeiroJogador != null)
                return "Primeiro Jogador";

            else if(cartasDuplicadasDoSegundoJogador != null)
                return "Segundo Jogador";
            
            var cartaComMaiorNumeroDoPrimeiroJogador = 
                cartasDoPrimeiroJogador
                .Select(carta => ConversorDeCarta.Converter(carta))
                .OrderBy(pesoDaCarta => pesoDaCarta).Max();

            var cartaComMaiorNumeroDoSegundoJogador = 
                cartasDoSegundoJogador
                .Select(carta => ConversorDeCarta.Converter(carta))
                .OrderBy(pesoDaCarta => pesoDaCarta).Max();

            return cartaComMaiorNumeroDoPrimeiroJogador.Valor > cartaComMaiorNumeroDoSegundoJogador.Valor ? 
                "Primeiro Jogador" : "Segundo Jogador";
        }
    }
}
