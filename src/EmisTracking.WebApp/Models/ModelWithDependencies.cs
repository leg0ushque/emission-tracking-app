using EmisTracking.WebApi.Models.ViewModels;
using System.Collections.Generic;

namespace EmisTracking.WebApp.Models
{
    public class ModelWithDependencies<TItemViewModel, TDependencyViewModel>
        where TItemViewModel : class, IViewModel
        where TDependencyViewModel : class, IViewModel
    {
        public TItemViewModel MainItem { get; set; }
        public IEnumerable<TDependencyViewModel> Dependencies { get; set; }
    }
}
