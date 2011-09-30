using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProductStudio;

namespace SEActivities.DataAccess
{
    public class PSDataAccess
    {
        private string connectDomain;
        private string productName;
        private ProductStudio.Directory psDirectory;
        private Product psProduct;
        private Datastore psDataStore;

        public PSDataAccess(string connectDomain, string productName, string onBehalfOf = null)
        {
            this.connectDomain = connectDomain;
            this.productName = productName;
            try
            {
                this.psDirectory = new ProductStudio.Directory();
                this.psDirectory.Connect(connectDomain, "", "");
                this.psProduct = this.psDirectory.GetProductByName(productName);
                this.psDataStore = this.psProduct.Connect("", "", onBehalfOf);
            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Failed to initialize Product Studio connection for domain: {0} and product: {1}",
                    connectDomain, productName), e);
            }
        }

    }
}
