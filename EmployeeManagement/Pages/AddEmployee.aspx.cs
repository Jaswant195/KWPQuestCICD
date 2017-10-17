using System;
using System.Collections.Generic;
using System.Linq;
using BLL;

namespace EmployeeManagement.Pages
{
    public partial class AddEmployee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SaveEmployee(object sender, EventArgs e)
        {
            string Name = txtName.Text.Trim();
            int Age     = Convert.ToInt32(txtAge.Text);
            int Salary  = Convert.ToInt32(txtSalary.Text);

            int result = BLLEmployee.InsertEmployee(Name, Age, Salary);

            if (result > 0)
            {
                lblMessage.Text = "Employee details Inserted Successfully";
            }
            else if(result == -1)
            {
                lblMessage.Text = "Employee Age should not be less then 20 !";
            }
            else
            {
                lblMessage.Text = "Employee details not Inserted";
            }
        } 
    } 
}