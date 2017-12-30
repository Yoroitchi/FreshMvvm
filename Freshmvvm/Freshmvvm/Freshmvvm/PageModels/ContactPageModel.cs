using FreshMvvm;
using Freshmvvm.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace Freshmvvm.PageModels
{
    public class ContactPageModel : FreshBasePageModel
    {
        // Use IoC to get our repository.
        private Repository _repository = FreshIOC.Container.Resolve<Repository>();

        // Backing data model.
        private Contact _contact;

        /// <summary>
        /// Public property exposing the contact's name for Page binding.
        /// </summary>
        public string ContactName
        {
            get { return _contact.Name; }
            set { _contact.Name = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Public property exposing the contact's email for Page binding.
        /// </summary>
        public string ContactEmail
        {
            get { return _contact.Email; }
            set { _contact.Email = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// Called whenever the page is navigated to.
        /// Either use a supplied Contact, or create a new one if not supplied.
        /// FreshMVVM does not provide a RaiseAllPropertyChanged,
        /// so we do this for each bound property, room for improvement.
        /// </summary>
        public override void Init(object initData)
        {
            _contact = initData as Contact;
            if (_contact == null) _contact = new Contact();
            base.Init(initData);
            RaisePropertyChanged(nameof(ContactName));
            RaisePropertyChanged(nameof(ContactEmail));
        }

        /// <summary>
        /// Command associated with the save action.
        /// Persists the contact to the database if the contact is valid.
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () => {
                    if (_contact.IsValid())
                    {
                        await _repository.CreateContact(_contact);
                        await CoreMethods.PopPageModel(_contact);
                    }
                });
            }
        }
    }
}
