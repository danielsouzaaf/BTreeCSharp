using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BTree.UnitTest
{
    public static class ValidacaoArvore
    {
        public static void ValidarArvore(No<int, int> arvore, int T, params int[] chavesEsperadas)
        {
            var chavesEncontradas = new Dictionary<int, List<Entrada<int, int>>>();
            ValidarSubArvore(arvore, arvore, T, int.MinValue, int.MaxValue, chavesEncontradas);

            Assert.AreEqual(0, chavesEsperadas.Except(chavesEncontradas.Keys).Count());
            foreach (var keyValuePair in chavesEncontradas)
            {
                Assert.AreEqual(1, keyValuePair.Value.Count);
            }
        }

        private static void AtualizarChavesEncontradas(Dictionary<int, List<Entrada<int, int>>> chavesEncontradas, Entrada<int, int> entrada)
        {
            List<Entrada<int, int>> entradasEncontradas;
            if (!chavesEncontradas.TryGetValue(entrada.Chave, out entradasEncontradas))
            {
                entradasEncontradas = new List<Entrada<int, int>>();
                chavesEncontradas.Add(entrada.Chave, entradasEncontradas);
            }

            entradasEncontradas.Add(entrada);
        }

        private static void ValidarSubArvore(No<int, int> raiz, No<int, int> no, int T, int noMin, int noMax, Dictionary<int, List<Entrada<int, int>>> chavesEncontradas)
        {
            if (raiz != no)
            {
                Assert.IsTrue(no.Entradas.Count >= T - 1);
                Assert.IsTrue(no.Entradas.Count <= (2 * T) - 1);
            }

            for (int i = 0; i <= no.Entradas.Count; i++)
            {
                int subarvoreMin = noMin;
                int subarvoreMax = noMax;

                if (i < no.Entradas.Count)
                {
                    var entrada = no.Entradas[i];
                    AtualizarChavesEncontradas(chavesEncontradas, entrada);
                    Assert.IsTrue(entrada.Chave >= noMin && entrada.Chave <= noMax);

                    subarvoreMax = entrada.Chave;
                }

                if (i > 0)
                {
                    subarvoreMin = no.Entradas[i - 1].Chave;
                }

                if (!no.ehFolha)
                {
                    Assert.IsTrue(no.Filhos.Count >= T);
                    ValidarSubArvore(raiz, no.Filhos[i], T, subarvoreMin, subarvoreMax, chavesEncontradas);
                }
            }
        }
    }
}
