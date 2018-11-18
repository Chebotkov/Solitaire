using System;
using NUnit.Framework;

namespace SolitaireBCL.Tests
{
    [TestFixture]
    public class LightListTests
    {
        public LightList<string> Init()
        {
            LightList<string> list = new LightList<string>();
            list.Add("Pasha");
            list.Add("Valera");
            list.Add("Tolik");

            return list;
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(4)]
        public void AddTest(int expected)
        {
            //Arrange
            int result = 0;
            LightList<string> list;

            //Act
            if (expected == 0)
            {
                list = new LightList<string>();
            }
            else
            {
                list = Init();
                if (expected == 4)
                {
                    list.Add("Natasha");
                }
            }
            result = list.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(true, "Pasha")]
        [TestCase(false, "sha")]
        [TestCase(false, "pasha")]
        public void ContainsTest(bool expected, string value)
        {
            //Arrange
            var list = Init();

            //Act
            bool result = list.Contains(value);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [TestCase(0, null)]
        [TestCase(1, "Nastya")]
        [TestCase(3, "Pasha")]
        [TestCase(4, "Pasha")]
        public void FirstTest(int listCount, string expected)
        {
            //Arrange
            string result;
            LightList<string> list;

            //Act
            if (listCount == 0)
            {
                list = new LightList<string>();
            }
            else
            {
                if(listCount == 1)
                {
                    list = new LightList<string>();
                    list.Add("Nastya");
                }

                else
                {
                    list = Init();
                    if (listCount == 4)
                    {
                        list.Add("Natasha");
                    }
                }
            }
            result = list.First;

            //Assert
            if (expected is null)
            {
                Assert.IsNull(result);
            }
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, null)]
        [TestCase(1, "Nastya")]
        [TestCase(3, "Tolik")]
        [TestCase(4, "Natasha")]
        public void LastTest(int listCount, string expected)
        {
            //Arrange
            string result;
            LightList<string> list;

            //Act
            if (listCount == 0)
            {
                list = new LightList<string>();
            }
            else
            {
                if (listCount == 1)
                {
                    list = new LightList<string>();
                    list.Add("Nastya");
                }

                else
                {
                    list = Init();
                    if (listCount == 4)
                    {
                        list.Add("Natasha");
                    }
                }
            }
            result = list.Last;

            //Assert
            if (expected is null)
            {
                Assert.IsNull(result);
            }
            Assert.AreEqual(expected, result);
        }

        [TestCase(0, null, null)]
        [TestCase(1, "Nastya", null)]
        [TestCase(3, "Tolik", "Valera")]
        [TestCase(3, "Pasha", "Tolik")]
        [TestCase(4, "Natasha", "Tolik")]
        [TestCase(4, "Valera", "Natasha")]
        public void RemoveAndLastTest(int listCount, string deletedString, string expected)
        {
            //Arrange
            string result;
            LightList<string> list;

            //Act
            if (listCount == 0)
            {
                list = new LightList<string>();
            }
            else
            {
                if (listCount == 1)
                {
                    list = new LightList<string>();
                    list.Add("Nastya");
                    list.Remove(deletedString);
                }

                else
                {
                    list = Init();
                    if (listCount == 4)
                    {
                        list.Add("Natasha");
                        list.Remove(deletedString);
                    }
                    else
                    {
                        list.Remove(deletedString);
                    }
                }
            }
            result = list.Last;

            //Assert
            if (expected is null)
            {
                Assert.IsNull(result);
            }
            Assert.AreEqual(expected, result);
        }
    }
}
