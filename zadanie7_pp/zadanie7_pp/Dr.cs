using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zadanie7_pp
{
    class Dr
    {
        public string label;
        public string name;
        public string tokenId;


        public Dr(string name, string label) 
        {
            this.label = label;
            this.name = name;

            this.tokenId = name.Replace("[", "");
            this.tokenId = this.tokenId.Replace("]", "");
            if (tokenId.Length > 0)
            {
                this.tokenId = "i" + this.tokenId;
            }
            else 
            {
                this.tokenId = "notExists";
            }

        }

        public string getLabel() 
        {
            return this.label;
        }

        public string getName() 
        {
            return this.name;
        }
    }
}
