// Copyright (c) James Dingle

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Primer.Validation
{

    // <summary>
    /// Creates and initialises instances of the <see cref="Primer.Validation.ValidatorAttribute"/> class and its decendants and attaches them to a viewmodel.
    /// </summary>
    /// <typeparam name="TValidator"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class ValidatorAttacher<TValidator, TData> where TValidator : ValidatorAttribute<TData>, new()
    {

        private ViewModel _ParentViewModel;


        /// <summary>
        /// Public constructor
        /// </summary>
        public ValidatorAttacher(ViewModel parentViewModel)
        {
            _ParentViewModel = parentViewModel;
        }



        /// <summary>
        /// 
        /// </summary>
        public ValidatorInitialiser<TValidator, TData> OnField(string fieldProperty)
        {
            return new ValidatorInitialiser<TValidator, TData>(_ParentViewModel, fieldProperty);
        }

    }

}
