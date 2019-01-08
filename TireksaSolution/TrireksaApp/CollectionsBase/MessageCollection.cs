using OcphSMSLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace TrireksaApp.CollectionsBase
{
    public class MessageCollection : ICollectionBase<Message>
    {
        public Message SelectedItem { get; set; }

        public ObservableCollection<Message> Source { get; set; }

        public CollectionView SourceView { get; set; }


        public MessageCollection()
        {
            Source = new ObservableCollection<Message>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        public async Task<bool> Add(Message item)
        {
            Source.Add(item);
            SourceView.Refresh();
            return await Task.Factory.StartNew(() => true);
        }

        public void CompleteTask(Task<List<Message>> task)
        {
            throw new NotImplementedException();
        }

        public Message GetItemById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
