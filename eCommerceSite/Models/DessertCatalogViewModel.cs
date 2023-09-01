namespace eCommerceSite.Models
{
    public class DessertCatalogViewModel
    {

        public DessertCatalogViewModel(List<Dessert> desserts, int lastPage, int currPage)
        {
            Desserts = desserts;
            LastPage = lastPage;
            CurrentPage = currPage;
        }

        public List<Dessert> Desserts { get; private set; }

        /// <summary>
        /// The last page of the catalog. Calculated by
        /// having a total number of products divided by
        /// products per page
        /// </summary>
        public int LastPage { get; private set; }

        /// <summary>
        /// The current page the user is viewing
        /// </summary>
        public int CurrentPage { get; private set; }
    }
}
