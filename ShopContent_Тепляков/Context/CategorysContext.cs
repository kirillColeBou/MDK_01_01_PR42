using ShopContent_Тепляков.Classes;
using ShopContent_Тепляков.Modell;
using ShopContent_Тепляков.View.Category.Categorys;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace ShopContent_Тепляков.Context
{
    public class CategorysContext : Categorys
    {
        public CategorysContext(bool save = false)
        {
            if (save) Save(true);
        }

        public static ObservableCollection<CategorysContext> AllCategorys()
        {
            ObservableCollection<CategorysContext> allCategorys = new ObservableCollection<CategorysContext>();
            SqlConnection connection;
            SqlDataReader dataCategorys = Classes.Connection.Query("SELECT * FROM Categorys", out connection);
            while (dataCategorys.Read())
            {
                allCategorys.Add(new CategorysContext()
                {
                    Id = dataCategorys.GetInt32(0),
                    Name = dataCategorys.GetString(1)
                });
            }
            Classes.Connection.CloseConnection(connection);
            return allCategorys;
        }

        public void Save(bool New = false)
        {
            SqlConnection connection;
            if (New)
            {
                SqlDataReader dataCategorys = Connection.Query("INSERT INTO" +
                    $"Categorys (Name) OUTPUT Inserted.Id VALUES (N'{this.Name}');", out connection);
                dataCategorys.Read();
                this.Id = dataCategorys.GetInt32(0);
            }
            else Connection.Query($"UPDATE Categorys SET Name = N'{this.Name}' WHERE Id = {this.Id}", out connection);
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.MainCategorys);
        }

        public void Delete()
        {
            SqlConnection connection;
            Connection.Query($"DELETE FROM Categorys WHERE Id = {this.Id}", out connection);
            Connection.CloseConnection(connection);
        }

        public RelayCommand OnEdit
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    MainWindow.init.frame.Navigate(new View.Category.Add(this));
                });
            }
        }

        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {

                    Save();
                });
            }
        }

        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.MainCategorys.DataContext as ViewModell.VMCategorys).Categorys.Remove(this);
                });
            }
        }
    }
}
