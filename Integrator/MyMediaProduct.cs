using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrator
{
    public class MyMediaProduct
    {
        //Declare and initiate variables 
        private Type _type;
        private int _itemNumber;
        private string _name;
        private double _price;
        private int _quantity;
        private string _author;
        private string _genre;
        private string _format;
        private string _language;
        private string _platform;
        private int _playtime;

        public MyMediaProduct(string type, int itemNumber, string name, double price, int quantity, string author, string genre, string format, string language, string platform, string playtime)
        {
            //Set values
            Type = Helper.GetType(type);
            ItemNumber = itemNumber;
            Name = name;
            Price = price;
            Quantity = quantity;
            Author = author;
            Genre = genre;
            Format = format;
            Language = language;
            Platform = platform;
            Playtime = Helper.ConvertInteger(playtime);

        }

        public MyMediaProduct(string type, int itemNumber, string name, string price, string quantity, string author, string genre, string format, string language, string platform, string playtime)
        {
            //Set values
            Type = Helper.GetType(type);
            ItemNumber = itemNumber;
            Name = name;
            Price = Helper.ConvertDouble(price);
            Quantity = Helper.ConvertInteger(quantity);
            Author = author;
            Genre = genre;
            Format = format;
            Language = language;
            Platform = platform;
            Playtime = Helper.ConvertInteger(playtime);

        }

        public Type Type
        {
            get => _type;
            set
            {
                _type = value;
            }
        }

        public int ItemNumber
        {
            get => _itemNumber;
            set
            {
                _itemNumber = value;
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                //Validate price
                if (0 <= value)
                {
                    if (value <= double.MaxValue)
                        _price = value;
                }
                else
                    _price = 0;
            }
        }
        public int Quantity
        {
            get => _quantity;
            set
            {
                //Validate quantity
                if (0 <= value)
                {
                    if (value <= double.MaxValue)
                        _quantity = value;
                }
                else
                    _quantity = 0;
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                _genre = value;
            }
        }

        public string Format
        {
            get => _format;
            set
            {
                _format = value;
            }
        }

        public string Language
        {
            get => _language;
            set
            {
                _language = value;
            }
        }

        public string Platform
        {
            get => _platform;
            set
            {
                _platform = value;

            }
        }
        public int Playtime
        {
            get => _playtime;
            set
            {
                //Validate plytime
                if (0 <= value)
                {
                    if (value <= double.MaxValue)
                        _playtime = value;
                }
                else
                    _playtime = 0;
            }
        }
    }
}
