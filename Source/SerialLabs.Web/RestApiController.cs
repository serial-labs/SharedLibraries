using Newtonsoft.Json;
using SerialLabs.Serialization;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace SerialLabs.Web.Http
{
    /// <summary>
    /// REST API Controller
    /// Enforce REST patterns and lower camel case formatting for json results (as per Javascript standards) by default.
    /// </summary>
    public abstract class RestApiController<TResource, TResourceId> : ApiController
        where TResource : class, new()
    {
        protected JsonSerializerSettings JsonSerializerSettings { get; private set; }

        /// <summary>
        /// Create a new instance of the <see cref="RestApiController"/>
        /// </summary>
        protected RestApiController()
            : this(CreateDefaultSerializerSettings())
        { }

        /// <summary>
        /// Create a new instance of the <see cref="RestApiController"/>
        /// </summary>
        /// <param name="jsonSerializerSettings"></param>
        protected RestApiController(JsonSerializerSettings jsonSerializerSettings)
        {
            Guard.ArgumentNotNull(jsonSerializerSettings, "jsonSerializerSettings");
            JsonSerializerSettings = jsonSerializerSettings;
        }

        #region Http Handlers
        /// <summary>
        /// [GET] Returns a single resource matching the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(TResourceId id)
        {
            TResource model = await GetOne(id);
            if (model == null)
            {
                return NotFound();
            }
            return FormattedJson(model);
        }

        /// <summary>
        /// [GET] Returns a paged collection of resources filtered by the provided filter options (if any)
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetAll([FromUri] PagedCollectionFilterOptions filterOptions)
        {
            PagedCollection<TResource> collection = await GetCollectionAsync(filterOptions);
            if (collection == null)
            {
                return NotFound();
            }
            return FormattedJson(collection);
        }
        #endregion

        #region Abstracts
        protected abstract Task<TResource> GetOne(TResourceId id);
        protected abstract Task<PagedCollection<TResource>> GetCollectionAsync(PagedCollectionFilterOptions filterOptions);
        #endregion

        #region Utility
        protected internal virtual JsonResult<T> FormattedJson<T>(T content)
        {
            return Json<T>(content, JsonSerializerSettings);
        }
        private static JsonSerializerSettings CreateDefaultSerializerSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ContractResolver = new LowerCamelCaseContractResolver();
            return settings;
        }
        #endregion
    }
}
