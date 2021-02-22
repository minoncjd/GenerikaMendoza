using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerikaMendoza
{
    public static class Globals
    {
        private static int _accountType = 0;
        private static int _accountId = 0;

        public static int AccountType
        {
            get { return _accountType; }
            set { _accountType = value; }
        }

        public static int AccountID
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
    }
}
