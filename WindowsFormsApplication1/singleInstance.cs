using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
   
    class singleInstance<T>
    {
        private singleInstance() { }
        
        ~singleInstance(){}
        static public singleInstance<T> getInstance() {
            if (p == null)
            {
                p = new singleInstance<T>();
            }
            return p; }
        static private singleInstance<T>p;// = new singleInstance<T>() {  };
    }
}
