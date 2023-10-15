using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace SimpleList1.Pages.Properties
{
    public class IndexModel : PageModel
    {
        public List<PropertyList> ListProperty = new List<PropertyList>();

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CRM;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Properties";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PropertyList propertyList = new PropertyList();
                                propertyList.id = "" + reader.GetInt32(0);
                                propertyList.HouseAddress = reader.GetString(1);
                                propertyList.Street = reader.GetString(2);
                                propertyList.City = reader.GetString(3);
                                propertyList.Zipcodes = reader.GetString(4);
                                propertyList.Block = reader.GetString(5);
                                propertyList.Lots = reader.GetString(6);
                                propertyList.Owner = reader.GetString(7);
                                propertyList.Age = reader.GetString(8);
                                propertyList.PhoneNumber = reader.GetString(9);

                                ListProperty.Add(propertyList);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class PropertyList
    {
        public String id;
        public String HouseAddress;
        public String Street;
        public String City;
        public String Zipcodes;
        public String Block;
        public String Lots;
        public String Owner;
        public String Age;
        public String PhoneNumber;




    }

}
