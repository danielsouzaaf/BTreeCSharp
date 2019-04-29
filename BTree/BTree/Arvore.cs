using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTree
{
    class Arvore<TC, TV> where TC : IComparable<TC>
    {
        public Arvore(int T)
        {
            if (T < 2)
                throw new ArgumentException("T da árvore tem que ser ao menos 2", "T");

            this.Root = new No<TC, TV>(T);
            this.T = T;
            this.Altura = 1;
        }

        public No<TC, TV> Root { get; private set; }
        public int T { get; private set; }
        public int Altura { get; private set; }

        public No<TC, TV> Buscar(TC chave)
        {
            return this.buscarInternal(this.Root, chave);
        }

        public void Inserir(TC chave, TV valor)
        {
            // posso inserir na raíz
            if (!this.Root.atingiuMaximoEntradas)
            {
                this.inserirInternal(this.Root, chave, valor);
            }

            // criar um novo nó e dividir a raíz antiga
            No<TC, TV> raizAntiga = this.Root;
            this.Root = new No<TC, TV>(this.T);
            this.Root.Filhos.Add(raizAntiga);
            this.dividirFilho(this.Root, 0, raizAntiga);
            this.inserirInternal(this.Root, chave, valor);

            this.Altura++;
        }

        private void dividirFilho(No<TC, TV> noPai, int indiceNoASerDividido, No<TC, TV> noASerDividido)
        {
            var novoNo = new No<TC, TV>(this.T);
            //no a ser promovido é o nó anterior ao nó de maior chave antes do split
            noPai.Entradas.Insert(indiceNoASerDividido, noASerDividido.Entradas[this.T - 1]);
            //adicionando o no resultante da divisao como filho no pai
            noPai.Filhos.Insert(indiceNoASerDividido + 1, novoNo);
            //adicionando os T - 1 nós que estão à direita do T para o novoNo
            novoNo.Entradas.AddRange(noASerDividido.Entradas.GetRange(this.T, this.T - 1));
            //removendo do nó a ser dividido os nós que foram adicionados no outro nó
            noASerDividido.Entradas.RemoveRange(this.T - 1, this.T);

            //é necessário ajeitar subárvore do nó
            if (!noASerDividido.ehFolha)
            {
                novoNo.Filhos.AddRange(noASerDividido.Filhos.GetRange(this.T, this.T));
                noASerDividido.Filhos.RemoveRange(this.T, this.T);
            }
        }

        private void inserirInternal(No<TC, TV> no, TC chave, TV valor)
        {
            int posicaoAInserir = no.Entradas.TakeWhile(entrada => chave.CompareTo(entrada.Chave) >= 0).Count();

            if (no.ehFolha)
            {
                no.Entradas.Insert(posicaoAInserir, new Entrada<TC, TV>() { Chave = chave, Valor = valor });
                return;
            }

            No<TC, TV> filho = no.Filhos[posicaoAInserir];

            if(filho.atingiuMaximoEntradas)
            {
                this.dividirFilho(no, posicaoAInserir, filho);
                if(chave.CompareTo(no.Entradas[posicaoAInserir].Chave) > 0)
                {
                    posicaoAInserir++;
                }
            }

            this.inserirInternal(no.Filhos[posicaoAInserir], chave, valor);
        }

        private No<TC, TV> buscarInternal(No<TC, TV> root, TC chave)
        {
            throw new NotImplementedException();
        }
    }
}
