// Copyright (c) James Dingle

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace Primer.Validation
{

    /// <summary>
    /// Creates and initialises instances of the <see cref="Primer.Validation.ValidatorAttribute"/> class and its decendants and attaches them to a viewmodel.
    /// </summary>
    /// <typeparam name="TValidator"></typeparam>
    public class ValidatorAttacher<TValidator> where TValidator : ValidatorAttribute, new()
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
        /// Attaches the validator to a specified field.
        /// </summary>
        public ValidatorInitialiser<TValidator> OnField(string fieldProperty)
        {
            return new ValidatorInitialiser<TValidator>(_ParentViewModel, fieldProperty);
        }

    }

}
