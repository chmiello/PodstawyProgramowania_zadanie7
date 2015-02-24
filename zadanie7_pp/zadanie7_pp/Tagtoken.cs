using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zadanie7_pp
{
    class Tagtoken
    {
        private string xmlId;
        private Dictionary<string, string> tags;

        public Tagtoken() {
            this.xmlId = "";
            this.tags = new Dictionary<string, string>();
        }

        public void addTag(string name, string innerText) {
            this.tags.Add(name, innerText);
        }

        public string getTag(string name) { 
            return this.tags[name];
        }

        public void setXmlId(string xmlId) {
            this.xmlId = xmlId;
        }

        public string getXmlId(){
            return this.xmlId;
        }
    }
}
