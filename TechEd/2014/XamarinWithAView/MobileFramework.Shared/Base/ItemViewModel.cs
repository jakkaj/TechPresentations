using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileFramework.Shared.Contract;

namespace MobileFramework.Shared.Base
{
    public class ItemViewModel<T> : ViewModel, ISelectableItem<T> where T : class
    {
        protected T _item;

        public override void Dispose()
        {
            base.Dispose();
            _item = null;
        }

        protected virtual void OnItemChanged(T item)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged(this, EventArgs.Empty);
            }
        }

        public T Item
        {
            get { return _item; }
            set
            {
                if (value == null)
                {
                    Debug.WriteLine("*** Missing item!");
                    return;
                }

                _item = value;
                OnPropertyChanged();
                OnItemChanged(_item);
            }
        }

        public event EventHandler SelectionChanged;
    }
}
