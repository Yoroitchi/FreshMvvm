using FreshMvvm;
using Freshmvvm.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Freshmvvm.PageModels
{
    public class ContactListPageModel : FreshBasePageModel
    {
        private Repository _repository = FreshIOC.Container.Resolve<Repository>();
        private Contact _selectedContact = null;

        /// <summary>
        /// Collection used for binding to the Page's contact list view.
        /// </summary>
        public ObservableCollection<Contact> Contacts { get; private set; }

        /// <summary>
        /// Used to bind with the list view's SelectedItem property.
        /// Calls the EditContactCommand to start the editing.
        /// </summary>
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                if (value != null) EditContactCommand.Execute(value);
            }
        }

        public ContactListPageModel()
        {
            Contacts = new ObservableCollection<Contact>();
        }

        /// <summary>
        /// Called whenever the page is navigated to.
        /// Here we are ignoring the init data and just loading the contacts.
        /// </summary>
        public override void Init(object initData)
        {
            LoadContacts();
            if (Contacts.Count() < 1)
            {
                CreateSampleData();
            }
        }

        /// <summary>
        /// Called whenever the page is navigated to, but from a pop action.
        /// Here we are just updating the contact list with most recent data.
        /// </summary>
        /// <param name="returnedData"></param>
        public override void ReverseInit(object returnedData)
        {
            LoadContacts();
            base.ReverseInit(returnedData);
        }

        /// <summary>
        /// Command associated with the add contact action.
        /// Navigates to the ContactPageModel with no Init object.
        /// </summary>
        public ICommand AddContactCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<ContactPageModel>();
                });
            }
        }

        /// <summary>
        /// Command associated with the edit contact action.
        /// Navigates to the ContactPageModel with the selected contact as the Init object.
        /// </summary>
        public ICommand EditContactCommand
        {
            get
            {
                return new Command(async (contact) => {
                    await CoreMethods.PushPageModel<ContactPageModel>(contact);
                });
            }
        }

        /// <summary>
        /// Repopulate the collection with updated contacts data.
        /// Note: For simplicity, we wait for the async db call to complete,
        /// recommend making better use of the async potential.
        /// </summary>
        private void LoadContacts()
        {
            Contacts.Clear();
            Task<List<Contact>> getContactsTask = _repository.GetAllContacts();
            getContactsTask.Wait();
            foreach (var contact in getContactsTask.Result)
            {
                Contacts.Add(contact);
            }
        }
    }
}
