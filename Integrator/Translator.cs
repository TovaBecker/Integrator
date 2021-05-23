using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator
{
    public static class Translator
    {
        public static MyMediaProduct ConvertToMyMediaProduct(SimpleMediaProdct product)
        {
            //Check price value with helper method
            double price = Convert.ToDouble(product.Price);

            //Get and split descrition
            var description = product.Description.Split(',');
            
            //Check description length
            if(description.Length == 7)
                //Put decription splitetd values to corresponding attribute and return a MyMedia product
                return new MyMediaProduct(CleanValue(description[0]), product.Id, product.Name, price, product.Count, CleanValue(description[1]), CleanValue(description[2]), CleanValue(description[3]), CleanValue(description[4]), CleanValue(description[5]), CleanValue(description[6]));
            return null;
        }

        public static List<MyMediaProduct> ConvertToMyMediaProductList(List<SimpleMediaProdct> productList)
        {
            //Declare an intance of List<MyMediaProduct>
            List<MyMediaProduct> myMediaProductList = new List<MyMediaProduct>();

            //Go thought the list of procucts
            foreach (SimpleMediaProdct product in productList)
            {
                //Convert to myMedia product
                var myMediaProduct = ConvertToMyMediaProduct(product);

                //Add product to list if not null
                if (myMediaProduct != null)
                    myMediaProductList.Add(myMediaProduct);
            }

            return myMediaProductList;
        }

        public static SimpleMediaProdct ConvertToSimpleMediaProdct(MyMediaProduct product)
        {
            //Create SimpleMedia descriotion from MyMedia attributes
            string description = $"Type: {product.Type}, Author: {product.Author}, Genre: {product.Genre}, Format: {product.Format}, Language: {product.Language}, Platform: {product.Platform}, Playtime: {product.Playtime} ";
            
            //Check price value with helper method
            int price = Convert.ToInt32(product.Price);

            //Return a SimpleMedia product
            return new SimpleMediaProdct(product.ItemNumber, price, product.Quantity, product.Name, description);
        }

        public static List<SimpleMediaProdct> ConvertToSimpleMediaProdctList(List<MyMediaProduct> productList)
        {
            //Declare an intance of List<SimpleMediaProdct>
            List<SimpleMediaProdct> simpleMediaProdctList = new List<SimpleMediaProdct>();

            //Go thought the list of procucts
            foreach ( MyMediaProduct product in productList)
            {
                //Convert to SimpleMedia product
                var SimpleMediaProduct = ConvertToSimpleMediaProdct(product);

                //Add product to list if not null
                if (SimpleMediaProduct != null)
                    simpleMediaProdctList.Add(SimpleMediaProduct);
            }

            return simpleMediaProdctList;
        }

        public static List<MyMediaProduct> FindChanges(List<MyMediaProduct> productList, List<MyMediaProduct> compareList)
        {
            //Declare an changesList of List<MyMediaProduct>
            var changesList = new List<MyMediaProduct>();

            //Go thought products
            foreach (MyMediaProduct product in productList)
            {
                //Find the product in the list
                var compareProdct = compareList.FirstOrDefault(c => c.ItemNumber == product.ItemNumber);

                //Check, null or if somthing has changed add product
                if (compareProdct == null)
                {
                    //Add product to list
                    changesList.Add(product);
                }
                else if(compareProdct.Author != product.Author
                        || compareProdct.Format != product.Format
                        || compareProdct.Genre != product.Genre
                        || compareProdct.Language != product.Language
                        || compareProdct.Name != product.Name
                        || compareProdct.Platform != product.Platform
                        || compareProdct.Playtime != product.Playtime
                        || compareProdct.Price != product.Price
                        || compareProdct.Quantity != product.Quantity
                        || compareProdct.Type != product.Type)
                {
                    //Add product to list
                    changesList.Add(product);
                }
            }
            return changesList;
        }

        public static List<MyMediaProduct> FindChanges(List<SimpleMediaProdct> productList, List<MyMediaProduct> compareList)
        {
            //Declare an changesList of List<MyMediaProduct>
            var changesList = new List<MyMediaProduct>();

            //Go thought products
            foreach (SimpleMediaProdct product in productList)
            {
                //Find the product in the list
                var compareProdct = compareList.FirstOrDefault(c => c.ItemNumber == product.Id);

                //Check, null or if somthing has changed add product
                if (compareProdct == null)
                {
                    //Add product to list
                    changesList.Add(ConvertToMyMediaProduct(product));
                }
                else
                {
                    var checkProduct = ConvertToMyMediaProduct(product);
                    if (compareProdct.Author != checkProduct.Author
                        || compareProdct.Format != checkProduct.Format
                        || compareProdct.Genre != checkProduct.Genre
                        || compareProdct.Language != checkProduct.Language
                        || compareProdct.Name != checkProduct.Name
                        || compareProdct.Platform != checkProduct.Platform
                        || compareProdct.Playtime != checkProduct.Playtime
                        || compareProdct.Price != checkProduct.Price
                        || compareProdct.Quantity != checkProduct.Quantity
                        || compareProdct.Type != checkProduct.Type)
                    {
                        //Add product to list
                        changesList.Add(ConvertToMyMediaProduct(product));
                    }

                }
            }

            return changesList;
        }

        public static List<SimpleMediaProdct> UpdateSimpleMediaProductList(List<SimpleMediaProdct> productList, List<MyMediaProduct> changesList)
        {
            //Go thought all changed products in changesList
            foreach (MyMediaProduct changedProduct in changesList)
            {
                //Find product in SimpleMedia list
                var oldProduct = productList.FirstOrDefault(c => c.Id == changedProduct.ItemNumber);

                //Check, null add product or if somthing has changed uppdate product in productList
                if (oldProduct == null)
                    productList.Add(ConvertToSimpleMediaProdct(changedProduct));
                else
                {
                    var index = productList.IndexOf(oldProduct);
                    productList[index] = ConvertToSimpleMediaProdct(changedProduct);
                }
            }

            return productList;
        }

        public static List<MyMediaProduct> UpdateMyMediaProductList(List<MyMediaProduct> productList, List<MyMediaProduct> changesList)
        {
            //Go thought all changed products in changesList
            foreach (MyMediaProduct changedProduct in changesList)
            {
                //Find product in MyMedia list
                var oldProduct = productList.FirstOrDefault(c => c.ItemNumber == changedProduct.ItemNumber);

                //Check, null add product or if somthing has changed uppdate product in productList
                if (oldProduct == null)
                    productList.Add(changedProduct);
                else
                {
                    var index = productList.IndexOf(oldProduct);
                    productList[index] = changedProduct;
                }
            }

            return productList;
        }

        public static string CleanValue(string value)
        {
            //Split value
            var result = value.Split(':');
            //Check lenght
            if (result.Length == 2)
                //Trim value to use
                return result[1].Trim();
            //Return value 
            return value;

        }


    }
}
