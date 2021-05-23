using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Integrator
{
    public static class Repository
    {
        public static List<SimpleMediaProdct> LoadSimpleMediaProducts(string path)
        {
            //Declare a new intance of XmlDocument
            XmlDocument xmlDocument = new XmlDocument();
            //Set path for xml
            xmlDocument.Load(path);

            //Declare a new intance of List<SimpleMediaProdct>
            List<SimpleMediaProdct> productList = new List<SimpleMediaProdct>();

            //Return empty product list if file is not avilabel
            if (!Helper.dataFileCheck(path))
                return productList;

            foreach (XmlElement childNode1 in xmlDocument.ChildNodes[0].ChildNodes)
            {
                //Declare nullabel varibel, three int and two string
                int id = 0;
                int price = 0;
                int count = 0;
                string description = "";
                string name = "";

                    foreach (XmlElement childNode2 in childNode1.ChildNodes)
                    {
                    //Check values with method in helper
                        if (childNode2.Name == "id")
                            id = Helper.ConvertInteger(childNode2.InnerText);
                        else if (childNode2.Name == "price")
                            price = Helper.ConvertInteger(childNode2.InnerText);
                        else if (childNode2.Name == "count")
                            count = Helper.ConvertInteger(childNode2.InnerText);
                        else if (childNode2.Name == "name")
                            name = childNode2.InnerText;
                        else if (childNode2.Name == "description")
                            description = childNode2.InnerText;
                    }

                //Add to product list
                productList.Add(new SimpleMediaProdct(id, price, count, name, description));
            }

            return productList;
        }

        public static void WriteSimpleMediaProducts(string path, List<SimpleMediaProdct> productList)
        {
            //Declare a new intance of XmlDocument
            XmlDocument doc1 = new XmlDocument();

            //Create container in doc
            XmlElement element1 = doc1.CreateElement("products");
            
            foreach (SimpleMediaProdct product in productList)
            {
                //Set product values and attach to product container
                XmlElement element2 = doc1.CreateElement("product");
                XmlElement xmlElement1 = element2;
                XmlDocument doc2 = doc1;
                XmlNode newChild1 = Helper.MakeElement(doc2, "id", product.Id.ToString());
                xmlElement1.AppendChild(newChild1);
                XmlElement xmlElement2 = element2;
                XmlDocument doc3 = doc1;
                XmlNode newChild2 = Helper.MakeElement(doc3, "price", product.Price.ToString());
                xmlElement2.AppendChild(newChild2);
                XmlElement xmlElement3 = element2;
                XmlDocument doc4 = doc1;
                XmlNode newChild3 = Helper.MakeElement(doc4, "count", product.Count.ToString());
                xmlElement3.AppendChild(newChild3);
                element2.AppendChild(Helper.MakeElement(doc1, "name", product.Name.ToString()));
                element2.AppendChild(Helper.MakeElement(doc1, "description", product.Description.ToString()));
                element1.AppendChild((XmlNode)element2);
            }

            //Attach container to document
            doc1.AppendChild((XmlNode)element1);

            //Save document to database
            doc1.Save(path);
        }

        public static List<MyMediaProduct> LoadMyMediaProducts(string path)
        {
            //Declare a new intance of List<MyMediaProduct>
            List<MyMediaProduct> productList = new List<MyMediaProduct>();

            //Return empty product list if file is not avilabel
            if (!Helper.dataFileCheck(path))
                return productList;

            //Start upp reding from CVS file
            using (var reader = new StreamReader(path))
            {
                //Get rows and split into data from CVS file
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //Create new prodokts and add produkts to inventoryList
                    if (values.Length == 11)
                    {
                        MyMediaProduct product = new MyMediaProduct(values[0], Convert.ToInt32(values[1]), values[2], values[3], values[4], values[5], values[6], values[7], values[8], values[9], values[10]);

                        productList.Add(product);
                    }
                }
            }
            return productList;
        }

        public static void WriteMyMediaProducts(string path, List<MyMediaProduct> productList)
        {
            //Check file
            Helper.dataFileCheck(path);

            //Open file
            using (var writer = new StreamWriter(path))
            {
                //Write all items from inventory list
                foreach (var product in productList)
                {
                    writer.WriteLine(
                        $"{product.Type}," +
                        $"{product.ItemNumber}," +
                        $"{product.Name}," +
                        $"{product.Price}," +
                        $"{product.Quantity}," +
                        $"{product.Author}," +
                        $"{product.Genre}," +
                        $"{product.Format}," +
                        $"{product.Language}," +
                        $"{product.Platform}," +
                        $"{product.Playtime}");
                }
            }
        }

    }
}
