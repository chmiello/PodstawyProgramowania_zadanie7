using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zadanie7_pp
{
    public class Cond
    {
        public Dictionary<string, string> args;

        public string nodeName;

        public Not not;

        public List<Index> indexlist;

        public Cond() 
        {
            this.args = new Dictionary<string, string>();
            this.indexlist = new List<Index>();
            this.not = new Not();
        }

        public void addArg(string name, string value)
        {
            this.args.Add(name, value);
        }

        public void setNodeName(string name) 
        {
            this.nodeName = name;
        }

        public string getArg(string name) {
            if (this.args.ContainsKey(name))
            {
                return this.args[name];
            }
            else 
            {
                return "No define";
            }
        }

        public String getNodeName()
        {
            return this.nodeName;
        }

        public void addNot(Not not)
        {
            this.not = not;
        }

        public Not getNot()
        {
            return this.not;
        }

        public void addIndexElement(Index element)
        {
            this.indexlist.Add(element);
        }

        public string getAllArgs()
        {
            string all = "";
            foreach (KeyValuePair<string, string> x in this.args)
                all += x.Key + " : " + x.Value + ", ";
            return all;
        }


    }
}
