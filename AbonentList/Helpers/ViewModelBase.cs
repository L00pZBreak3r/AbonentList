﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AbonentList.Helpers
{
  internal abstract class ViewModelBase : INotifyPropertyChanged
  {
    protected void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
  }
}
