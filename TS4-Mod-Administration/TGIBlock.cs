using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS4_Mod_Administration
{
    class TGIBlock
    {
        private string resourceType;
        private string resourceGroup;
        private string resourceInstance;

        public string ResourceType
        {
            get
            {
                return this.resourceType;
            }
            set
            {
                this.resourceType = value;
            }
        }
        public string ResourceGroup
        {
            get
            {
                return this.resourceGroup;
            }
            set
            {
                this.resourceGroup = value;
            }
        }
        public string ResourceInstance
        {
            get
            {
                return this.resourceInstance;
            }
            set
            {
                this.resourceInstance = value;
            }
        }

        public TGIBlock(string resType, string resGroup, string resInstance)
        {
            this.resourceType = resType;
            this.resourceGroup = resGroup;
            this.resourceInstance = resInstance;
        }
    }
}
