using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace SolitaireBCL.Tests
{
    [TestFixture]
    public class LightStackTests
    {
        public LightStack<string> Init()
        {
            LightStack<string> stack = new LightStack<string>();
            stack.Push("Pasha");
            stack.Push("Valera");
            stack.Push("Tolik");

            return stack;
        }

        [TestCase()]
        public void PeekTest()
        {
            //Arrange
            var stack = Init();
            string expected = "Tolik";

            //Act
            string result = stack.Peek();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase()]
        public void PopTest()
        {
            //Arrange
            var stack = Init();
            string expected = "Tolik";

            //Act
            string result = stack.Pop();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase()]
        public void PopPeekTest()
        {
            //Arrange
            var stack = Init();
            string popExpected = "Tolik";
            string peekExpected = "Valera";
            List<string> expected = new List<string>();
            expected.Add(popExpected);
            expected.Add(peekExpected);

            //Act
            string popResult = stack.Pop();
            string peekResult = stack.Peek();
            List<string> result = new List<string>();
            result.Add(popResult);
            result.Add(peekResult);

            //Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestCase()]
        public void PushPeekTest()
        {
            //Arrange
            var stack = Init();
            string peekExpected = "Natasha";

            //Act
            stack.Push("Natasha");
            string peekResult = stack.Peek();

            //Assert
            Assert.AreEqual(peekExpected, peekResult);
        }

        [TestCase()]
        public void PopExceptionTest()
        {
            //Arrange
            var stack = new LightStack<string>();

            //Act
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }
        
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(4)]
        public void CountTest(int expected)
        {
            //Arrange
            int result = 0;
            LightStack<string> stack;
            
            //Act
            if (expected == 0)
            {
                stack = new LightStack<string>();
            }
            else
            {
                stack = Init();
                if (expected == 4)
                {
                    stack.Push("Natasha");
                }
            }
            result = stack.Count;

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(true, "Pasha")]
        [TestCase(false, "sha")]
        [TestCase(false, "pasha")]
        public void ContainsTest(bool expected, string value)
        {
            //Arrange
            var stack = Init();

            //Act
            bool result = stack.Contains(value);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
