using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace AdoTask2
{
   
    public partial class MainWindow : Window
    {
        SqlConnection? cstr = null;
        SqlCommand? command = null;
        SqlDataAdapter? adapter = null;
        DataTable? dataTable = null;

        public MainWindow()
        {
            InitializeComponent();
            cstr = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");
            dataTable = new DataTable();
            adapter?.Fill(dataTable);
            dataGridView.ItemsSource = dataTable.DefaultView;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            if (dataTable is not null)
            {
                adapter?.Update(dataTable);
            }
        }
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            SqlParameter parameter = new SqlParameter()
            {
                ParameterName = "@item",
                SqlDbType = SqlDbType.NVarChar,
                Value = txtBox.Text
            };
            command = new SqlCommand("SELECT * FROM Authors AS A WHERE A.FirstName=@item", cstr);
            command.Parameters.Add(parameter);
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView.ItemsSource = dataTable.DefaultView;
        }
    }
}
