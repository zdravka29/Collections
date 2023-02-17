using NUnit.Framework.Interfaces;
using System;

namespace Collections.UnitTests
{
    public class CollectionTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            // Arrange 
            var nums = new Collection<int>();

            // Act
            Assert.That(nums.Count == 0, "Count property");
            //Assert.That(nums.Capacity == 0, "Capacity property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[]");

        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            // Arrange 
            var nums = new Collection<int>(5);

            // Act
            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5]");

        }

        [Test]
        public void Test_Collection_ConstructorMultipleItem()
        {
            // Arrange 
            var nums = new Collection<int>(5, 6);

            // Act
            Assert.That(nums.Count == 2, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[5, 6]");

        }

        [Test]
        public void Test_Collection_Add()
        {
            // Arrange 
            var nums = new Collection<int>();

            // Act
            nums.Add(7);

            // Assert
            Assert.That(nums.Count == 1, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[7]");

        }


        [Test]
        public void Test_Collection_AddRange()
        {
            // Arrange 
            var items = new int[] { 6, 7, 8 };
            var nums = new Collection<int>();

            // Act
            nums.AddRange(items);

            // Assert
            Assert.That(nums.Count == 3, "Count property");
            Assert.AreEqual(nums.Capacity, 16, "Capacity property");
            Assert.That(nums.ToString() == "[6, 7, 8]");

        }

        [Test]

        public void Test_Collection_AddRangeWithGrow() {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        [Timeout(5000)]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000;
            var nums = new Collection<int>();

            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [TestCase("Peter, Maria, George", 0, "Peter")]
        [TestCase("Peter, Maria, George", 1, "Maria")]
        [TestCase("Peter, Maria, George", 2, "George")]
        public void Test_Collection_GetByValidIndex(
            string data, int index, string ExpectedValue)
        {
            // Arrange
            var nums = new Collection<string>(data.Split(", "));

            // Act
            var actual = nums[index];

            // Assert
            Assert.AreEqual(ExpectedValue, actual);
        }

        [TestCase("", 0)]
        [TestCase("Peter", -1)]
        [TestCase("Peter", 1)]
        [TestCase("Peter, Maria, George", -1)]
        [TestCase("Peter, Maria, George", 3)]
        [TestCase("Peter, Maria, George", 150)]
        public void Test_Collection_GetByInvalidIndex(
            string data, int index)
        {
            var items = new Collection<string>(data.Split(",", 
                StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => items[index], Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtStart() {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            var removed = names.RemoveAt(0);
            Assert.AreEqual(removed, "Sam");
            Assert.That(names.ToString(), Is.EqualTo("[Kate, Peter, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            var removed = names.RemoveAt(3);
            Assert.AreEqual(removed, "Mia");
            Assert.That(names.ToString(), Is.EqualTo("[Sam, Kate, Peter]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            var removed = names.RemoveAt(1);
            Assert.AreEqual(removed, "Kate");
            Assert.That(names.ToString(), Is.EqualTo("[Sam, Peter, Mia]"));
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.Clear();
            Assert.That(names.ToString, Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.InsertAt(0, "Zaffy");
            Assert.That(names.ToString, Is.EqualTo("[Zaffy, Sam, Kate, Peter, Mia]"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.InsertAt(2, "Zaffy");
            Assert.That(names.ToString, Is.EqualTo("[Sam, Kate, Zaffy, Peter, Mia]"));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.InsertAt(4, "Zaffy");
            Assert.That(names.ToString, Is.EqualTo("[Sam, Kate, Peter, Mia, Zaffy]"));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            var names = new Collection<string>("Sam", "Kate");
            Assert.That(() => { names.InsertAt(-1, "Zaffy"); }, Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names.InsertAt(5, "Zaffy"); }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names[1] = "Betty";
            Assert.That(names.ToString, Is.EqualTo("[Sam, Betty, Peter, Mia]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var names = new Collection<string>("Sam", "Kate");
            Assert.That(() => { names[-1] = "Betty"; }, Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => { names[3] = "Betty"; }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.Exchange(0, 3);
            Assert.That(names.ToString, Is.EqualTo("[Mia, Kate, Peter, Sam]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndex()
        {
            var names = new Collection<string>("Sam", "Kate", "Mia");
            Assert.That(() => { names.Exchange(-1, 4); }, Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var names = new Collection<string>("Sam", "Kate", "Peter", "Mia");
            names.Exchange(1, 2);
            Assert.That(names.ToString, Is.EqualTo("[Sam, Peter, Kate, Mia]"));
        }
    }
}