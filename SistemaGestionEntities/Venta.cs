using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion
{
    public class Venta
    {   //declaro atributos
        private int _id;
        private string _comentarios;
        private int _idUsuario;


        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public string comentarios
        {
            get
            {
                return _comentarios;
            }
            set
            {
                _comentarios = value;
            }
        }

        public int idUsuario
        {
            get
            {
                return _idUsuario;
            }
            set
            {
                _idUsuario = value;
            }
        }


        //Inicializo
        public Venta()
        {
            _id = 0;
            _comentarios = string.Empty;
            _idUsuario = 0;
        }

        //contructor
        public Venta(int id, string comentarios, int idUsuario)
        {
            this._id = id;
            this._comentarios = comentarios;
            this._idUsuario = idUsuario;
        }


    }
}

