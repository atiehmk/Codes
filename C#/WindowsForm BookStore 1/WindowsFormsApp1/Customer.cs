using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{  /// <summary>
///  The Customer class contains the customer information ( properties and methods)  
/// </summary>
    class Customer
    {

        private string _firstName;     
        private string _lastName;   
        private string _address;    
        private string _city;  
        private string _state;
        private string _zipCode;
        private string _phone;
        private string _email;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="zipCode"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        public Customer(string firstName, string lastName, string address, string city, string state, string zipCode, string phone, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _address = address;
            _city = city;
            _state = state;
            _zipCode = zipCode;
            _phone = phone;
            _email = email;

        }


        public Customer()
        {

        }

        // Define accessors ( get and set accessors ) 

        public string FirstName   // property
        {
            get { return _firstName; }   // get method
            set { _firstName = value; }  // set method
        }

        public string LastName   // property
        {
            get { return _lastName; }   // get method
            set { _lastName = value; }  // set method
        }
        public string Address  // property
        {
            get { return _address; }   // get method
            set { _address = value; }  // set method
        }

        public string City   // property
        {
            get { return _city; }   // get method
            set { _city = value; }  // set method
        }

        public string State   // property
        {
            get { return _state; }   // get method
            set { _state = value; }  // set method
        }

        public string ZipCode   // property
        {
            get { return _zipCode; }   // get method
            set { _zipCode = value; }  // set method
        }

        public string Phone   // property
        {
            get { return _phone; }   // get method
            set { _phone = value; }  // set method
        }

        public string Email   // property
        {
            get { return _email; }   // get method
            set { _email = value; }  // set method
        }


    }
}
