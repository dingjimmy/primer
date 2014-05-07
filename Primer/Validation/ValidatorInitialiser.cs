// Copyright (c) James Dingle

namespace Primer.Validation
{

    /// <summary>
    /// Creates and initialises instances of the <see cref="Primer.Validation.ValidatorAttribute"/> class and its decendants.
    /// </summary>
    /// <typeparam name="TValidator"></typeparam>
    public class ValidatorInitialiser<TValidator> where TValidator : ValidatorAttribute, new()
    {

        private ViewModel _ParentViewModel;
        private string _FieldName;


        /// <summary>
        /// Public constructor
        /// </summary>
        public ValidatorInitialiser(ViewModel parentViewModel, string name)
        {
            _FieldName = name;
            _ParentViewModel = parentViewModel;
        }



        /// <summary>
        /// Set the validator's parameters to their default value.
        /// </summary>
        public TValidator WithNoParameters()
        {
            return WithParameters(null);
        }



        /// <summary>
        /// Set the validator's parameters.
        /// </summary>
        public TValidator WithParameters(params object[] parameters)
        {

            TValidator validator = new TValidator();

            validator.Initialise(parameters);
            _ParentViewModel.AddValidatorAttribute(_FieldName, validator);

            return validator;

        }

    }

}