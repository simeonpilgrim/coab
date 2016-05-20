using System;
using System.Collections.Generic;
using GoldBox.Classes;
using GoldBox.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GoldBox.Engine
{
    [TestClass]
    public class ovr017Tests
    {
        [TestMethod]
        public void TestGettingMenuItems()
        {
            var exceptions = new List<Exception>();

            Config.Setup();
            gbl.import_from = ImportSource.Curse;

            List<MenuItem> displayNames;
            List<MenuItem> fileNames;
            ovr017.BuildLoadablePlayersLists(out fileNames, out displayNames);

            if (fileNames.Count == 0)
                exceptions.Add(new Exception("File Names has no items"));
            if (displayNames.Count == 0)
                exceptions.Add(new Exception("Display Names has no items"));

            if (exceptions.Count > 0)
            {
                var aggregateException = new AggregateException(exceptions);
                Console.Error.WriteLine(aggregateException);
                throw aggregateException;
            }
        }
    }
}
