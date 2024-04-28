using ShopContent_Тепляков.Modell;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopContent_Тепляков.ViewModell
{
    public class VMCategorys : INotifyPropertyChanged
    {
        public ObservableCollection<Context.CategorysContext> Categorys { get; set; }

        public Classes.RelayCommand NewCategory
        {
            get
            {
                return new Classes.RelayCommand(obj =>
                {
                    Context.CategorysContext newModell = new Context.CategorysContext(true);
                    Categorys.Add(newModell);
                    MainWindow.init.frame.Navigate(new View.Category.Add(newModell));
                });
            }
        }

        public VMCategorys() => Categorys = Context.CategorysContext.AllCategorys();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
