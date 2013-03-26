using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace RestTraining.Web.Tests.WatinTests
{
    [TestClass]
    public class HotelTests
    {
        [TestMethod]
        public void AddBoundedHotelTest()
        {
            using (var browser = new IE())
            {
                browser.GoTo("http://localhost:51599/");
                var link = browser.Link(Find.ByText("Hotels"));
                link.Click();
            }
        }
    }
}
