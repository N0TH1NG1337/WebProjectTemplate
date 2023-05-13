using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

public static class Helper
{
    public const string DBName = "Database.mdf";   //Name of the MSSQL Database.
    public const string tblName = "tblUser";      // Name of the user Table in the Database
    public const string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\"
                                    + DBName + ";Integrated Security=True";   // The Data Base is in the App_Data = |DataDirectory|

    //public static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gilad\source\repos\DBWeb\DBWeb\App_Data\Database.mdf;Integrated Security=True";
    //public static string conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";


    public static DataSet RetrieveTable(string SQLStr)
    // Gets A table from the data base acording to the SELECT Command in SQLStr;
    // Returns DataSet with the Table.
    {
        // connect to DataBase
        SqlConnection con = new SqlConnection(conString);

        // Build SQL Query
        SqlCommand cmd = new SqlCommand(SQLStr, con);

        // Build DataAdapter
        SqlDataAdapter ad = new SqlDataAdapter(cmd);

        // Build DataSet to store the data
        DataSet ds = new DataSet();

        // Get Data form DataBase into the DataSet
        ad.Fill(ds, tblName);

        return ds;
    }

    public static object GetScalar(string SQL)
    {
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        SqlCommand cmd = new SqlCommand(SQL, con);

        // ביצוע השאילתא
        con.Open();
        object scalar = cmd.ExecuteScalar();
        con.Close();

        return scalar;
    }

    public static int ExecuteNonQuery(string SQL)
    {
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        SqlCommand cmd = new SqlCommand(SQL, con);

        // ביצוע השאילתא
        con.Open();
        int n = cmd.ExecuteNonQuery();
        con.Close();

        // return the number of rows affected
        return n;
    }

    public static void Delete(int id)
    // The Array "userIdToDelete" contain the id of the users to delete. 
    // Delets all the users in the array "userIdToDelete".
    {
        int[] userIdToDelete = { id };
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // טעינת הנתונים
        string SQL = "SELECT * FROM " + tblName;
        SqlCommand cmd = new SqlCommand(SQL, con);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adapter.Fill(ds, tblName);

        // מחיקת שורות שנבחרו מתוך הדאטה סט
        for (int i = 0; i < userIdToDelete.Length; i++)
        {
            {
                DataRow[] dr = ds.Tables[tblName].Select($"userId = {userIdToDelete[i]}");
                dr[0].Delete();
            }
        }

        // עדכון הדאטה סט בבסיס הנתונים
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        adapter.UpdateCommand = builder.GetDeleteCommand();
        adapter.Update(ds, tblName);
    }

    public static void Update(User user)
    // The Method recieve a user objects. Find the user in the DataBase acording to his userId and update all the other properties in DB.
    {
        // HttpRequest Request
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        string SQLStr = "SELECT * FROM " + Helper.tblName + $" Where userid={user.userId}";
        SqlCommand cmd = new SqlCommand(SQLStr, con);

        //  טעינת הנתונים לתוך DataSet
        DataSet ds = new DataSet();
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds, tblName);

        // בניית השורה להוספה
        DataRow dr = ds.Tables[tblName].Rows[0];
        dr["firstName"] = user.firstName;
        dr["lastName"] = user.lastName;
        dr["userName"] = user.userName;
        dr["password"] = user.password;
        dr["birthday"] = user.birthday;
        dr["city"] = user.city;
        dr["admin"] = user.Admin;

        // עדכון הדאטה סט בבסיס הנתונים
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        adapter.UpdateCommand = builder.GetUpdateCommand();
        adapter.Update(ds, tblName);

    }

    public static void Insert(User user)
    // The Method recieve a user objects and insert it to the Database as new row. 
    // The Method does't check if the user is already taken.
    {
        //HttpRequest Request
        // התחברות למסד הנתונים
        //string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gilad\source\repos\DBWeb\DBWeb\App_Data\Database.mdf;Integrated Security=True";
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        string SQLStr = $"SELECT * FROM " + tblName + " WHERE 0=1";
        SqlCommand cmd = new SqlCommand(SQLStr, con);

        // בניית DataSet
        DataSet ds = new DataSet();

        // טעינת סכימת הנתונים
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        adapter.Fill(ds, tblName);

        // בניית השורה להוספה
        DataRow dr = ds.Tables[tblName].NewRow();
        dr["firstName"] = user.firstName;
        dr["lastName"] = user.lastName;
        dr["userName"] = user.userName;
        dr["password"] = user.password;
        if (!user.birthday.Equals(DateTime.Parse("01-01-1900")))
            dr["birthday"] = user.birthday;
        dr["city"] = user.city;
        ds.Tables[tblName].Rows.Add(dr);

        // עדכון הדאטה סט בבסיס הנתונים
        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
        adapter.UpdateCommand = builder.GetInsertCommand();
        adapter.Update(ds, tblName);
    }

    public static User GetUserData(int id)
    // The Method check if there is a user with userName and Password. 
    // If true the Method return a user with the first Name and Admin property.
    // If not the Method return a user wuth first name "Visitor" and Admin = false

    {
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        string SQL = $"SELECT userId, userName, [password], firstName, lastName, birthday, city, [admin] FROM " + tblName +
                $" WHERE userid='{id}'";
        SqlCommand cmd = new SqlCommand(SQL, con);

        // ביצוע השאילתא
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // שימוש בנתונים שהתקבלו
        User user = new User();
        if (reader.HasRows)
        {
            reader.Read();
            user.userId = reader.GetInt32(0);
            user.userName = reader.GetString(1);
            user.password = reader.GetString(2);
            if (reader.GetValue(3).GetType() != typeof(DBNull) && reader.GetValue(3).GetType() != null)
                user.firstName = reader.GetString(3);
            if (reader.GetValue(4).GetType() != typeof(DBNull) && reader.GetValue(4).GetType() != null)
                user.lastName = reader.GetString(4);
            if (reader.GetValue(5).GetType() != typeof(DBNull) && reader.GetValue(5).GetType() != null)
                user.birthday = reader.GetDateTime(5);
            if (reader.GetValue(6).GetType() != typeof(DBNull) && reader.GetValue(6).GetType() != null)
                user.city = reader.GetString(6);
            user.Admin = reader.GetBoolean(7);
        }
        else
        {
            user.userName = "Visitor";
        }
        reader.Close();
        con.Close();
        return user;
    }
    public static User GetRow(string userName, string password)
    // The Method check if there is a user with userName and Password. 
    // If true the Method return a user with the first Name and Admin property.
    // If not the Method return a user wuth first name "Visitor" and Admin = false

    {
        // התחברות למסד הנתונים
        SqlConnection con = new SqlConnection(conString);

        // בניית פקודת SQL
        string SQL = $"SELECT userId, userName, [password], firstName, lastName, birthday, city, [admin] FROM " + tblName +
                $" WHERE userName='{userName}' AND password = '{password}'";
        SqlCommand cmd = new SqlCommand(SQL, con);

        // ביצוע השאילתא
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();

        // שימוש בנתונים שהתקבלו
        User user = new User();
        if (reader.HasRows)
        {
            reader.Read();
            user.userId = reader.GetInt32(0);
            user.userName = reader.GetString(1);
            user.password = reader.GetString(2);
            if (reader.GetValue(3).GetType() != typeof(DBNull) && reader.GetValue(3).GetType() != null)
                user.firstName = reader.GetString(3);
            if (reader.GetValue(4).GetType() != typeof(DBNull) && reader.GetValue(4).GetType() != null)
                user.lastName = reader.GetString(4);
            if (reader.GetValue(5).GetType() != typeof(DBNull) && reader.GetValue(5).GetType() != null)
                user.birthday = reader.GetDateTime(5);
            if (reader.GetValue(6).GetType() != typeof(DBNull) && reader.GetValue(6).GetType() != null)
                user.city = reader.GetString(6);
            user.Admin = reader.GetBoolean(7);
        }
        else
        {
            user.userName = "Visitor";
        }
        reader.Close();
        con.Close();
        return user;
    }

    public static string BuildSimpleUsersTable(DataTable dt)
    // the Method Build HTML user Table using the users in the DataTable dt.
    {
        string str = "<table class='usersTable' align='center'>";
        str += "<tr>";
        foreach (DataColumn column in dt.Columns)
        {
            str += "<td>" + column.ColumnName + "</td>";
        }

        foreach (DataRow row in dt.Rows)
        {
            str += "<tr>";
            foreach (DataColumn column in dt.Columns)
            {
                str += "<td>" + row[column] + "</td>";
            }
            str += "</tr>";
        }
        str += "</tr>";
        str += "</Table>";
        return str;
    }

    public static string[] OldNames = { "userId", "userName", "password", "firstName", "lastName", "birthday", "city", "Admin" };
    public static string[] NewNames = { "Id", "Username", "Password", "FirstName", "LastName", "Birthday", "City", "Admin" };

    public static string BuildUsersTable(DataTable dt)
    // the Method Build HTML user Table with checkBoxes using the users in the DataTable dt.
    {

        string str = "<table align='center' style='padding: 20px'>";
        str += "<tr>";
        // set everything left // str += "<td> </td>";
        foreach (DataColumn column in dt.Columns)
        {
            int IndexOfName = Array.IndexOf(OldNames, column.ColumnName);
            str += "<td style='padding: 10px'>" + NewNames[IndexOfName] + "</td>";
        }


        foreach (DataRow row in dt.Rows)
        {
            str += "<tr>";
            foreach (DataColumn column in dt.Columns)
            {
                if (column.ColumnName == "birthday")
                {
                    if (row[column].ToString() != "" && row[column].ToString() != null)
                    {
                        DateTime date = DateTime.Parse(row[column].ToString());
                        string formatdate = $"{date.Day}/{date.Month}/{date.Year}";
                        str += "<td>" + formatdate + "</td>";
                    }
                    else
                        str += "<td>אין</td>";
                }
                else if (column.ColumnName == "Admin")
                {
                    str += "<td>" + Createchkbox(bool.Parse(row["Admin"].ToString())) + "</td>";

                }
                else
                {
                    str += "<td>" + row[column] + "</td>";
                }
            }
            str += "<td>" + CreateEditLink(row["userId"].ToString()) + "</td>";
            str += "<td>" + CreateDeleteLink(row["userId"].ToString()) + "</td>";
            str += "</tr>";
        }
        str += "</tr>";
        str += "</Table>";
        return str;
    }

    public static string CreateEditLink(string id)
    {
        return $"<a href='/Pages/Edit?id={id}' style='marging: 10px' runat='server' >edit</a>";
    }
    public static string CreateDeleteLink(string id)
    {
        return $"<a href='/Pages/Delete?id={id}' style='marging: 10px' runat='server' >delete</a>";
    }
    public static string Createchkbox(bool isadmin)
    {
        if (isadmin)
            return $"<input type='checkbox' disabled checked/>";
        else
            return $"<input type='checkbox' disabled />";
    }

    public static string CleanString(string StringToUse)
    {
        return StringToUse.Replace(" ", String.Empty);
    }

    public static DataTable SortTable(DataTable dt, string column, string dir)
    {
        dt.DefaultView.Sort = column + " " + dir;
        return dt.DefaultView.ToTable();
    }

    public static DataTable FilterTable(DataTable dt, string column, string criteria)
    {
        dt.DefaultView.RowFilter = column + "=" + criteria;
        return dt.DefaultView.ToTable();
    }
}