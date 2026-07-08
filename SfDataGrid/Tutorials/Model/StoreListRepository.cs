using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataGrid
{
    /// <summary>
    /// This class represents the CategoryRepository
    /// </summary>
    public class CategoryRepository : List<String>
    {
        #region CTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        public CategoryRepository()
        {
            Add("Argentina");
            Add("Austria");
            Add("Belgium");
            Add("Brazil");            
            Add("Canada");
            Add("Denmark");
            Add("Finland");
            Add("France");
            Add("Germany");
            Add("Ireland");
            Add("Italy");
            Add("Mexico");
            Add("Norway");
            Add("Poland");
            Add("Portugal");
            Add("Spain");
            Add("Sweden");
            Add("Switzerland");
            Add("UK");
            Add("USA");
            Add("Venezuela");
        }
        #endregion
    }  
}
