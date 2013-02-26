using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class HotelsControllerTests
    {
        [TestMethod]
        public void Get()
        {
            var hotelObj1 = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelObj2 = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj2);
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj1);
            var all = TestHelpers.HotelsApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Address == hotelObj1.Address && x.Title == hotelObj1.Title));
            Assert.IsTrue(all.Any(x => x.Address == hotelObj2.Address && x.Title == hotelObj2.Title));
        }
    }
}
