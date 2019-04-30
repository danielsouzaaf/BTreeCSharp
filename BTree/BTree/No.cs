using System;
using System.Collections.Generic;
using System.Text;

namespace BTree
{
    public class No<TC, TV>
    {
        private int t;

        public List<No<TC, TV>> Filhos { get; set; }

        public List<Entrada<TC, TV>> Entradas { get; set; }

        public No(int t)
        {
            this.t = t;
            this.Filhos = new List<No<TC, TV>>(t);
            this.Entradas = new List<Entrada<TC, TV>>(t);
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
