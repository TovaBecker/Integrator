using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Integrator
{
    public class Watcher
    {
        FileSystemWatcher fia;
        FileSystemWatcher simpleMedia;
        string _myMediaPath;
        string _simpleMediaPath;
        List<DateTime> _timeForMyMediaChange = new List<DateTime>();
        List<DateTime> _timeForSimpleMediaChange = new List<DateTime>();
        List<MyMediaProduct> compareList = new List<MyMediaProduct>();

        public Watcher()
        {
            ReadConfigFile();
            LoadCompareList();
            SetUpWatcherForMyMedia();
            SetUpWatcherForSimpleMedia();
        }

        private void LoadCompareList()
        {
            //Get copare list fom repository
            compareList = Repository.LoadMyMediaProducts(_myMediaPath);

            //Convert to simple media product list
            var simpleMediaProductList = Translator.ConvertToSimpleMediaProdctList(compareList);

            //Save change to SimpleMedia database, so simple media and my media has the same databas from start
            Repository.WriteSimpleMediaProducts(_simpleMediaPath, simpleMediaProductList);
        }

        private void SetUpWatcherForMyMedia()
        {
            //Get folder name index
            var indexOfLastFolder = _myMediaPath.LastIndexOf('\\');
            //Get folder name
            var databaseFoler = _myMediaPath.Substring(0, indexOfLastFolder);
            //Get file name
            var databaseFile = _myMediaPath.Substring(indexOfLastFolder + 1, _myMediaPath.Length - indexOfLastFolder - 1);
            //Get fia database file
            fia = new FileSystemWatcher(databaseFoler, databaseFile);
            //Check if fia is changed
            fia.Changed += Fia_Changed;
            //Create event if somthing changes
            fia.EnableRaisingEvents = true;
        }

        private void SetUpWatcherForSimpleMedia()
        {
            //Get folder name index
            var indexOfLastFolder = _simpleMediaPath.LastIndexOf('\\');
            //Get folder name
            var databaseFoler = _simpleMediaPath.Substring(0, indexOfLastFolder);
            //Get file name
            var databaseFile = _simpleMediaPath.Substring(indexOfLastFolder + 1, _simpleMediaPath.Length - indexOfLastFolder - 1);
            //Get fia database file
            simpleMedia = new FileSystemWatcher(databaseFoler, databaseFile);
            //Check if simpleMedia is changed
            simpleMedia.Changed += SimpleMedia_Changed;
            //Create event if somthing changes
            simpleMedia.EnableRaisingEvents = true;
        }

        private void ReadConfigFile()
        {
            //Declare a new intance of XmlDocument
            XmlDocument doc = new XmlDocument();
            //Set path for doc
            doc.Load("config.xml");

            //Go thought element in doc
            foreach (XmlElement elem in doc.FirstChild.ChildNodes)
            {
                if (elem.Name == "database")
                {
                    var application = elem.GetAttribute("application");

                    //Get mymedia path
                    if (application == "mymedia")
                        _myMediaPath = elem.InnerXml;

                    //Get simplemedia path
                    if (application == "simplemedia")
                        _simpleMediaPath = elem.InnerXml;
                }

            }
        }

        private void Fia_Changed(object sender, FileSystemEventArgs e)
        {
            //Set time for change
            _timeForMyMediaChange.Add(DateTime.Now);

            //Check what change is the oldest
            while (_timeForSimpleMediaChange.Count > 0 && _timeForSimpleMediaChange[0] < _timeForMyMediaChange[0])
            {
                //Set system to wait
                System.Threading.Thread.Sleep(1000);
            }
            //Set system to wait
            System.Threading.Thread.Sleep(1000);

            //Do not create event if somthing changes
            simpleMedia.EnableRaisingEvents = false;

            //Get products from database and set to list
            var myMediaProductList = Repository.LoadMyMediaProducts(_myMediaPath);
            //Get the list of changed products
            var changesList = Translator.FindChanges(myMediaProductList, compareList);
            //Get products from database and set to list
            var simpleMediaProductList = Repository.LoadSimpleMediaProducts(_simpleMediaPath);
            //Update the simpleMedia list
            simpleMediaProductList = Translator.UpdateSimpleMediaProductList(simpleMediaProductList, changesList);
            //Save change to SimpleMedia database
            Repository.WriteSimpleMediaProducts(_simpleMediaPath, simpleMediaProductList);
            //Update the compare list
            compareList = Translator.UpdateMyMediaProductList(compareList, changesList);

            //Create event if somthing changes
            simpleMedia.EnableRaisingEvents = true;

            //Remove time of change
            _timeForMyMediaChange.RemoveAt(0);

        }

        private void SimpleMedia_Changed(object sender, FileSystemEventArgs e)
        {
            //Set time for change
            _timeForSimpleMediaChange.Add(DateTime.Now);

            //Check what change is the oldest
            while (_timeForMyMediaChange.Count > 0 && _timeForSimpleMediaChange[0] > _timeForMyMediaChange[0])
            {
                //Set system to wait
                System.Threading.Thread.Sleep(1000);
            }
            //Set system to wait
            System.Threading.Thread.Sleep(1000);

            //Do not create event if somthing changes
            fia.EnableRaisingEvents = false;

            //Get products from database and set to list
            var simpleMediaProductList = Repository.LoadSimpleMediaProducts(_simpleMediaPath);
            //Get the list of changed products
            var changesList = Translator.FindChanges(simpleMediaProductList, compareList);
            //Get products from database and set to list
            var myMediaProductList = Repository.LoadMyMediaProducts(_myMediaPath);
            //Update the myMedia list
            myMediaProductList = Translator.UpdateMyMediaProductList(myMediaProductList, changesList);
            //Save change to myMedia database
            Repository.WriteMyMediaProducts(_myMediaPath, myMediaProductList);
            //Update the compare list
            compareList = Translator.UpdateMyMediaProductList(compareList, changesList); ;

            //Create event if somthing changes
            fia.EnableRaisingEvents = true;

            //Remove time of change
            _timeForSimpleMediaChange.RemoveAt(0);

        }
    }
}
