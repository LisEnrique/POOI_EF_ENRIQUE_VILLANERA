using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POOI_EF_ENRIQUE_VILLANERA.Models
{
    public class EmpleadoPedido
    {

        public int EmployeeID { get; set; }

        public string Empleado { get; set; }

        public int CantidadPedidos { get; set; }

        public EmpleadoPedido() { }

        public EmpleadoPedido(int employeeID, string empleado,int cantidadPedidos)
        {
            EmployeeID = employeeID;
            Empleado = empleado;
            CantidadPedidos = cantidadPedidos;
        }
    }
}