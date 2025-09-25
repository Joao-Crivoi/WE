namespace Bl_Container.Models
{
    
        public class Container
        {
            public int ID { get; set; }
            public string Numero { get; set; } = string.Empty;
            public string Tipo { get; set; } = string.Empty;
            public int Tamanho { get; set; }

            // FK obrigatório
            public int IDBl { get; set; }

            public Container() { }

            public Container(int id, string numero, string tipo, int tamaho, int idBl)
            {
                ID = id;
                Numero = numero;
                Tipo = tipo;
                Tamanho = tamaho;
                IDBl = idBl;
            }
        }
    }
