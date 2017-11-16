using Project.Core;
using SecurityAccess.Contracts.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAccess.Service.Repositories
{
    public interface IGetCategoriesRepository
    {
        GetCategoriesResponse GetCategories();
    }    

    [Export(typeof(IGetCategoriesRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class GetCategoriesRepository : IGetCategoriesRepository
    {
        #region Declarations
        private MainDevEnvEntities _MainDevEnvEntities;
        private GetCategoriesResponse _Response;
        private ICollection<Category> _CategoryList;
        #endregion Declarations

        public GetCategoriesResponse GetCategories()
        {
            _Response = new GetCategoriesResponse { ValidationResults = new ValidationResults() };

            assignCategories();
            assignResponse();
            return _Response;
        }

        private void assignCategories()
        {
            if (!_Response.ValidationResults.IsValid) return;

            using (_MainDevEnvEntities = new MainDevEnvEntities())
            {
                _CategoryList = _MainDevEnvEntities.Categories.ToList();
            }
        }

        private void assignResponse()
        {
            if (!_Response.ValidationResults.IsValid) return;
            if (_CategoryList == null || !_CategoryList.Any()) return;

            _Response.CategoryList = _CategoryList.Select(x => new Contracts.Category
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }
    }
}
