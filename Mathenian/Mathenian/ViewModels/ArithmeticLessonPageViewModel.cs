using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathenian.ViewModels
{
	public class ArithmeticLessonPageViewModel : ViewModelBase
	{
        public ArithmeticLessonPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Arithmetic Lesson";
        }
	}
}
