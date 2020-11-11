using BeautySalon.Commands;
using BeautySalon.Models;
using BeautySalon.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;

namespace BeautySalon.ViewModels
{
    class PositionsViewModel : BaseViewModel
    {
        private ObservableCollection<Position> positions;
        public ObservableCollection<Position> Positions { get => positions; set => OnChanged(out positions, value); }
        string posName;
        public string PosName { get => posName; set { OnChanged(out posName, value); AddPositionCommand.RaiseCanExecuteChanged(); }  }

        public PositionsViewModel() => UpdateTable();

        private async void UpdateTable()
        {
            using BeautySalonDBContext db = new BeautySalonDBContext();
            await db.Positions.LoadAsync();
            Positions = db.Positions.Local.ToObservableCollection();
        }

        private BaseCommand addPositionCommand = null;
        public BaseCommand AddPositionCommand => addPositionCommand ??= new BaseCommand(x =>
        {
            using BeautySalonDBContext dBContext = new BeautySalonDBContext();
            Position pos1 = new Position
            {
                Name = this.PosName
            };
            dBContext.Positions.Add(pos1);
            dBContext.SaveChanges();
            UpdateTable();
            PosName = string.Empty;

        }, () => !string.IsNullOrWhiteSpace(PosName));

    }
}
