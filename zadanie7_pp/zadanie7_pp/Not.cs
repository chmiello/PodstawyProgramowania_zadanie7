using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zadanie7_pp
{
    public class Not
    {
        private  Dictionary<string, Dr> domain;
        public List<Cond> conds;

        public Not()
        {
            domain = new Dictionary<string, Dr>();
            conds = new List<Cond>();
        }

        /* public void addDomain(string name, Dr value) 
        {
            this.domain.Add(name, value);
        } */

        public void addCond(Cond value)
        {
            this.conds.Add(value);
        }

        /* public Dictionary<string, Dr> getDomain()
        {
            return this.domain;
        } */

        public List<Cond> getConds()
        {
            return this.conds;
        }

    }
}
