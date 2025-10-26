namespace Test.Core;

public class RangeBandTests
{
    [Test]
    [TestCase(0, RangeBand.None)]
    [TestCase(1, RangeBand.Adjacent)]
    [TestCase(2, RangeBand.Close)]
    [TestCase(3, RangeBand.Short)]
    [TestCase(4, RangeBand.Short)]
    [TestCase(5, RangeBand.Medium)]
    [TestCase(6, RangeBand.Medium)]
    [TestCase(7, RangeBand.Medium)]
    [TestCase(8, RangeBand.Medium)]
    [TestCase(9, RangeBand.Medium)]
    [TestCase(10, RangeBand.Long)]
    [TestCase(19, RangeBand.Long)]
    [TestCase(20, RangeBand.VeryLong)]
    [TestCase(44, RangeBand.VeryLong)]
    [TestCase(45, RangeBand.Distant)]
    [TestCase(90, RangeBand.Distant)]
    public void GetRightBandForDistances(int distance, RangeBand band)
    {
        Assert.That(distance.GetBand(), Is.EqualTo(band));
    }
}
