using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hostal.Utilidades
{
    public class ExcepcionPersonal:Exception
    {
        public ExcepcionPersonal(String msn) : base(msn)
        {

        }
    }
}