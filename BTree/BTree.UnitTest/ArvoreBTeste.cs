using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BTree.UnitTest
{
    [TestClass]
    public class ArvoreBTeste
    {
        private const int T = 2;

        private readonly int[] testChaveData = new int[] { 10, 20, 30, 50 };
        private readonly int[] testValorData = new int[] { 50, 60, 40, 20 };

        [TestMethod]
        public void CriarArvoreB()
        {
            var btree = new Arvore<int, int>(T);

            No<int, int> root = btree.Root;
            Assert.IsNotNull(root);
            Assert.IsNotNull(root.Entradas);
            Assert.IsNotNull(root.Filhos);
            Assert.AreEqual(0, root.Entradas.Count);
            Assert.AreEqual(0, root.Filhos.Count);
        }

        [TestMethod]
        public void InserirUmNo()
        {
            var btree = new Arvore<int, int>(T);
            this.InserirDadosDeTesteEValidarAArvore(btree, 0);
            Assert.AreEqual(1, btree.Altura);
        }

        [TestMethod]
        public void InserirMultiplosNosParaDividir()
        {
            var btree = new Arvore<int, int>(T);

            for (int i = 0; i < this.testChaveData.Length; i++)
            {
                this.InserirDadosDeTesteEValidarAArvore(btree, i);
            }

            Assert.AreEqual(2, btree.Altura);
        }

        private void InserirDadosDeTeste(Arvore<int, int> btree, int testDataIndice)
        {
            btree.Inserir(this.testChaveData[testDataIndice], this.testValorData[testDataIndice]);
        }

        private void InserirDadosDeTesteEValidarAArvore(Arvore<int, int> btree, int testDataIndice)
        {
            btree.Inserir(this.testChaveData[testDataIndice], this.testValorData[testDataIndice]);
            ValidacaoArvore.ValidarArvore(btree.Root, T, this.testChaveData.Take(testDataIndice + 1).ToArray());
        }

    }
}
