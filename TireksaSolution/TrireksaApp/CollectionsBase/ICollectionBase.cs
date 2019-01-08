using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TrireksaApp.CollectionsBase
{
   public interface ICollectionBase<T>
    {
        ObservableCollection<T> Source { get; set; }
        CollectionView SourceView { get; set; }
        T SelectedItem { get; set; }
        void CompleteTask(Task<List<T>> task);
        T GetItemById(int Id);
        Task<bool> Add(T item);


    }
}
