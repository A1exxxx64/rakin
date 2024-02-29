using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ракин_Курсовая
{
    public partial class Arends
    {
        public override string ToString()
        {
            return Date_start.ToShortDateString();
        }

    }
    public partial class Clients
    {
        public override string ToString()
        {
            return Client_Name + " " +Client_Surname;
        }
    }
    public partial class Devises
    {
        public override string ToString()
        {
            return Devise_Name;
        }
    }
    public partial class Pays
    {
        public override string ToString()
        {
            return Summ.ToString()+"руб.";
        }
    }
    public partial class Users
    {
        public override string ToString()
        {
            return login + " "+role;
        }
    }

}
