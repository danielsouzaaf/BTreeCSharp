using System;

namespace BTree
{

    public class Entrada<TC, TV> : IEquatable<Entrada<TC, TV>>
    {
        public TC Chave { get; set; }

        public TV Valor { get; set; }

        public bool Equals(Entrada<TC, TV> outro)
        {
            return this.Chave.Equals(outro.Chave) && this.Valor.Equals(outro.Valor);
        }


    }
}