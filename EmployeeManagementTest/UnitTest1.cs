using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL;
using System.Data;

namespace EmployeeManagementTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SaveEmployee_Add_ShouldReturnInteger()
        {
            // Arrange
            int expected = 1;
            // Act
            int actual = BLLEmployee.InsertEmployee("Jash", 27, 35000);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MethodName_ActionName_ReturnValue()
        {
            // Arrange
            
            // Act
            

            // Assert
        }
    }
}
