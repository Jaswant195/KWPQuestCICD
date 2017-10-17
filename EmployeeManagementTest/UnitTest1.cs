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
        public void GetEmployee_Get_ShouldReturnDataset()
        {
            // Arrange
            int expected = 2;
            // Act
            DataSet ds = BLLEmployee.GetEmployee(1);

            // Assert
            Assert.AreEqual(expected, ds.Tables.Count);
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
