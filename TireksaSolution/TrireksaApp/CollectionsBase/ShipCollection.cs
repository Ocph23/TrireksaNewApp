using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Data;
using ModelsShared.Models;
using TrireksaApp.Common;

namespace TrireksaApp.CollectionsBase
{
    public class ShipCollection 
    {
        private Client client = new Client("Ships");
        public ship SelectedItem { get; set; }

        public ObservableCollection<ship> Source { get; set; }

        public CollectionView SourceView { get; set; }

        public ShipCollection()
        {
            Source = new ObservableCollection<ship>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            CompleteTask();
        }

        public async void CompleteTask()
        {
            var result = await client.GetAsync<List<ship>>("Get");
            if(result!=default(List<ship>))
            {
                foreach (var item in result)
                {
                    Source.Add(item);
                    SourceView.Refresh();
                }
            }
          
        }

        public ship GetItemById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ship> Add(ship item)
        {
            var result = await client.PostAsync<ship>("Post", item);
            if(result!=default(ship))
            {
                Source.Add(result);
                SourceView.Refresh();
            }
            return result;
        }
    }
}
