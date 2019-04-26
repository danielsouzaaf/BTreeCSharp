using System;
using System.Collections.Generic;
using System.Text;

namespace BTree
{
    class No<TK, TC>
    {
        private int t;

        public List<No<TK, TC>> Filhos { get; set; }

        public List<Entrada<TK, TC>> Entradas { get; set; }

        public No(int t)
        {
            this.t = t;
            this.Filhos = new List<No<TK, TC>>(t);
            this.Entradas = new List<Entrada<TK, TC>>(t);
        }

        public Boolean ehFolha
        {
            get
            {
                return this.Filhos.Count == 0;
            }
        }

        public bool atingiuMaximoEntradas
        {
            get
            {
                return this.Entradas.Count == (2 * this.t) - 1;
            }
        }

        public bool atingiuMinimoEntradas
        {
            get
            {
                return this.Entradas.Count == this.t - 1;
            }
        }
    }
}
