using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using TrireksaApp.Common;
using TrireksaApp.Models;
using ModelsShared.Models;

namespace TrireksaApp.Contents.ManifestOutgoing
{
    internal class BrowseSTTVM:NotifyPropertyChanged
    {
        public BrowseSTTVM(Models.PackingListSimulation simulationPack)
        {
            this.SimulationPack = simulationPack;
            // this.Source = (CollectionView)CollectionViewSource.GetDefaultView(sTTs);
            Cancel = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => CancelAction() };
            MoveToPackDetail = new CommandHandler
            {
                CanExecuteAction = x => MoveToPackDetailValidation(),
                ExecuteAction = x => MoveToPackDetailAction()
            };
            MoveBack = new CommandHandler
            {
                CanExecuteAction = x => MoveBackValidation(),
                ExecuteAction = x => MoveBackAction()
            };

            NewPack = new CommandHandler { CanExecuteAction = x => NewPackValidation(), ExecuteAction = x => NewPackAction() };
            DeletePack = new CommandHandler { CanExecuteAction = x => DeletePackValidation(), ExecuteAction = x => DeletePackAction() };

        }

       


        //Command Defenition
        public Action CloseWindow { get; internal set; }
        public CommandHandler Save { get; set; }
        public CommandHandler Cancel { get; set; }
        public CommandHandler MoveToPackDetail { get; set; }
        public CommandHandler MoveBack { get; set; }
        public CommandHandler NewPack { get; set; }
        public CommandHandler DeletePack { get; set; }



        //Command Validation


        

        private bool NewPackValidation()
        {
            return true;
        }


        private bool MoveBackValidation()
        {
            if (SimulationPack.PackSelectedItem != null && SimulationPack.PackSelectedItem != null && SimulationPack.PackDetailsSelectedItem != null)
            {
                return true;
            }
            else
                return false;
        }

        private bool MoveToPackDetailValidation()
        {
            if (SimulationPack.PackSelectedItem != null && SimulationPack.STTDetailSelected != null && SimulationPack.PackSelectedItem != null)
            {
                return true;
            }
            else
                return false;
        }

        private bool DeletePackValidation()
        {
            if (SimulationPack.PackSelectedItem != null)
                return true;
            return false;
        }

        //Command Action
        private void NewPackAction()
        {
            var number = SimulationPack.Packs.Count;
            SimulationPack.Packs.Add(new Pack { PackingLists = new List<ModelsShared.Models.colly>(),
                PackNumber = number+1 });
            SimulationPack.PacksView.Refresh();

        }
        private void DeletePackAction()
        {
          if(SimulationPack.PackSelectedItem.PackingLists.Count>0)
            {
                ResourcesBase.ShowMessageError("Kosongkan Pack Sebelum Dihapus");
            }
            else
            {
                SimulationPack.Packs.Remove(SimulationPack.PackSelectedItem);
                SimulationPack.PacksView.Refresh();
            }
        }

        private void CancelAction()
        {
            CloseWindow();
        }

        
        private void MoveBackAction()
        {
            var item = SimulationPack.PackDetailsSelectedItem;
           // var item = (ModelsShared.Models.colly)SimulationPack.PackDetailsSelectedItem.Clone();
            SimulationPack.PackSelectedItem.PackingLists.Remove(SimulationPack.PackDetailsSelectedItem);
            SimulationPack.PacksView.Refresh();
            SimulationPack.PackDetailsView.Refresh();

            if(item!=null)
            {
                var source = SimulationPack.Source.SourceCollection as List<PenjualanView>;
                var selected = source.Find(O => O.Id == item.PenjualanId);
                if (selected != null)
                {
                    selected.Details.Add(item);
                    if(selected.Id==SimulationPack.SelectedItem.Id)
                    {
                        SimulationPack.Collies.Add(item);
                    }
                }



         //       SimulationPack.SelectedItem.Details.Add(item);
           
                SimulationPack.DetailPackSource.Remove(item);
            }
            else
            {
               
            }
            SimulationPack.PackDetailsSelectedItem = null;
           SimulationPack. Source.Refresh();
            SimulationPack.SelectedItemDetailsView.Refresh();
        }

        private void MoveToPackDetailAction()
        {
            var item =(ModelsShared.Models.colly)SimulationPack.STTDetailSelected.Clone();
            SimulationPack.PackSelectedItem.PackingLists.Add(item);
            SimulationPack.DetailPackSource.Add(item);
            SimulationPack.PacksView.Refresh();
            SimulationPack.PackDetailsView.Refresh();

            SimulationPack.SelectedItem.Details.Remove(SimulationPack.STTDetailSelected);
            SimulationPack.Collies.Remove(SimulationPack.STTDetailSelected);
            SimulationPack.Source.Refresh();
            SimulationPack.SelectedItemDetailsView.Refresh();
        }

        public PackingListSimulation SimulationPack { get; private set; }
    }
}