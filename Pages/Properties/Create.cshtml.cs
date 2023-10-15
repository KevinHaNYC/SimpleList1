using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.Emit;


namespace SimpleList1.Pages.Properties
{
    public class CreateModel : PageModel
    {
        public PropertyList propertyList = new PropertyList();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            propertyList.HouseAddress = Request.Form["House Address"];
            propertyList.Street = Request.Form["Street"];
            propertyList.City = Request.Form["City"];
            propertyList.Zipcodes = Request.Form["Zip codes"];
            propertyList.Block = Request.Form["Block"];
            propertyList.Lots = Request.Form["Lot"];
            propertyList.Owner = Request.Form["Owner"];
            propertyList.Age = Request.Form["Age"];
            propertyList.PhoneNumber = Request.Form["Phone Number"];


            if (propertyList.HouseAddress.Length == 0 ||
                propertyList.Street.Length == 0 ||
                propertyList.City.Length == 0 ||
                propertyList.Zipcodes.Length == 0 ||
                propertyList.Block.Length == 0 ||
                propertyList.Lots.Length == 0 ||
                propertyList.Owner.Length == 0 ||
                propertyList.Age.Length == 0 ||
                propertyList.PhoneNumber.Length == 0)
            {
                errorMessage = "All Fields are Required";
                return;
            }
            //save the new client into the database
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRM;Integrated Security=True";
                using (SqlConnection connection= new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO properties" +
                        "(HouseAddress, Street, City, Zipcode, Block, Lot, Owner, Age, PhoneNumber) VALUES" +
                        "(@HouseAddress, @Street, @City, @Zipcodes, @Block, @Lot, @Owner, @Age, @PhoneNumber)";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@HouseAddress", propertyList.HouseAddress);
                        command.Parameters.AddWithValue("@Street", propertyList.Street);
                        command.Parameters.AddWithValue("@City", propertyList.City);
                        command.Parameters.AddWithValue("@ZipCodes", propertyList.Zipcodes);
                        command.Parameters.AddWithValue("@Block", propertyList.Block);
                        command.Parameters.AddWithValue("@Lot", propertyList.Lots);
                        command.Parameters.AddWithValue("@Owner", propertyList.Owner);
                        command.Parameters.AddWithValue("@Age", propertyList.Age);
                        command.Parameters.AddWithValue("@PhoneNumber", propertyList.PhoneNumber);

                        command.ExecuteNonQuery(); 
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
           

            propertyList.HouseAddress = "";
            propertyList.Street = "";
            propertyList.City = "";
            propertyList.Zipcodes = "";
            propertyList.Block = "";
            propertyList.Lots =
            propertyList.Owner = "";
            propertyList.Age = "";
            propertyList.PhoneNumber = "";

            successMessage = "New Property Added Correctly";

            Response.Redirect("/Properties/Index");
        }
    }

}
