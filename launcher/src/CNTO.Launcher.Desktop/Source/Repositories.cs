using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNTO.Launcher.Infrastructure;

namespace UI.Source
{
    public class Repositories : INotifyPropertyChanged
    {
        private List<RepositoryDto> _repositories;

        public IEnumerable<RepositoryDto> All => _repositories;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<RepositoryDto> GetSelected() => _repositories.Where(r => r.Selected);

        public void Load(FilesystemRepositoryCollection collection)
        {
            _repositories = collection.All().Select(c => new RepositoryDto()
            {
                Identity = c.RepositoryId.Name,
                Path = c.Path,
                Selected = false,
                ServerSide = c.ServerSide ? "Yes" : string.Empty
            }).ToList();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("All"));
        }
    }
}
