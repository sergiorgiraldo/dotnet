/* 
 * OriginStamp API Documentation
 *
 * The following documentation describes the API v3 for OriginStamp. OriginStamp is a trusted timestamping service that uses the decentralized blockchain to store anonymous, tamper-proof timestamps for any digital content. OriginStamp allows users to timestamp files, emails, or plain text, and subsequently store the created hashes in the blockchain as well as retrieve and verify timetamps that have been committed to the blockchain.The trusted timestamping service of OriginStamp allows you to generate a hash fingerprint and prove that it was created at a specific point in time. If you are interested in integrating trusted timestamping into your own project, feel free to use our provided API. The following interactive documentation describes the interfaces and supports your integration. With this documentation you are able to try out the different requests and see the responses. For the authorization, add your API key to the Authorization header of your request.<br/><h2>Timestamping Steps</h2><ol><li><strong>Determine Hash: </strong> Calculate the SHA-256 of your record using a cryptographic library.</li><li><strong>Create Timestamp: </strong>Create a timestamp and add meta information to index it, e.g. a comment. You can also request a notification (email or webhook) once the tamper-proof timestamp has been created.</li><li><strong>Archive original file: </strong>Since we have no access to your original data, you should archive it because the timestamp is only valid in combination with the original file.</li><li><strong>Check Timestamp Status: </strong>Since the timestamps are always transmitted to the blockchain network at certain times, i.e. there is a delay, you can check the status of a hash and thus get the timestamp information.</li><li><strong>Get Timestamp Proof: </strong>As soon as the tamper-proof timestamp has been generated, you should archive the proof (Merkle Tree), which we created in our open procedure, together with the original file. With this proof, the existence of the file can be verified independently of OriginStamp. Here you can choose if the raw proof (xml) is sufficient proof or if you want to have a certificate (pdf).</li></ol><br/><h2>Installation Notes</h2><ul><li>Make sure you set the Authorization header correctly using your API key.</li><li>If a Cloudflare error occurs, please set a custom UserAgent header.</li><li>Please have a look at the models below to find out what each field means.</li></ul>
 *
 * OpenAPI spec version: 3.0
 * Contact: mail@originstamp.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace IO.Swagger.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface ISchedulerApi : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get active currencies
        /// </summary>
        /// <remarks>
        /// Returns an array with all active currencies.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>DefaultListCurrencyModel</returns>
        DefaultListCurrencyModel GetActiveCurrencies ();

        /// <summary>
        /// Get active currencies
        /// </summary>
        /// <remarks>
        /// Returns an array with all active currencies.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of DefaultListCurrencyModel</returns>
        ApiResponse<DefaultListCurrencyModel> GetActiveCurrenciesWithHttpInfo ();
        /// <summary>
        /// Next scheduler
        /// </summary>
        /// <remarks>
        /// Get the next scheduling time for hash submissions to the blockchain.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>DefaultSchedulerResponse</returns>
        DefaultSchedulerResponse GetNextSchedulingTime (string authorization, SchedulerRequest schedulerRequest);

        /// <summary>
        /// Next scheduler
        /// </summary>
        /// <remarks>
        /// Get the next scheduling time for hash submissions to the blockchain.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>ApiResponse of DefaultSchedulerResponse</returns>
        ApiResponse<DefaultSchedulerResponse> GetNextSchedulingTimeWithHttpInfo (string authorization, SchedulerRequest schedulerRequest);
        #endregion Synchronous Operations
        #region Asynchronous Operations
        /// <summary>
        /// Get active currencies
        /// </summary>
        /// <remarks>
        /// Returns an array with all active currencies.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of DefaultListCurrencyModel</returns>
        System.Threading.Tasks.Task<DefaultListCurrencyModel> GetActiveCurrenciesAsync ();

        /// <summary>
        /// Get active currencies
        /// </summary>
        /// <remarks>
        /// Returns an array with all active currencies.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (DefaultListCurrencyModel)</returns>
        System.Threading.Tasks.Task<ApiResponse<DefaultListCurrencyModel>> GetActiveCurrenciesAsyncWithHttpInfo ();
        /// <summary>
        /// Next scheduler
        /// </summary>
        /// <remarks>
        /// Get the next scheduling time for hash submissions to the blockchain.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>Task of DefaultSchedulerResponse</returns>
        System.Threading.Tasks.Task<DefaultSchedulerResponse> GetNextSchedulingTimeAsync (string authorization, SchedulerRequest schedulerRequest);

        /// <summary>
        /// Next scheduler
        /// </summary>
        /// <remarks>
        /// Get the next scheduling time for hash submissions to the blockchain.
        /// </remarks>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>Task of ApiResponse (DefaultSchedulerResponse)</returns>
        System.Threading.Tasks.Task<ApiResponse<DefaultSchedulerResponse>> GetNextSchedulingTimeAsyncWithHttpInfo (string authorization, SchedulerRequest schedulerRequest);
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class SchedulerApi : ISchedulerApi
    {
        private IO.Swagger.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerApi"/> class.
        /// </summary>
        /// <returns></returns>
        public SchedulerApi(String basePath)
        {
            this.Configuration = new IO.Swagger.Client.Configuration { BasePath = basePath };

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchedulerApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public SchedulerApi(IO.Swagger.Client.Configuration configuration = null)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = IO.Swagger.Client.Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = IO.Swagger.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.ApiClient.RestClient.BaseUrl.ToString();
        }

        /// <summary>
        /// Sets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        [Obsolete("SetBasePath is deprecated, please do 'Configuration.ApiClient = new ApiClient(\"http://new-path\")' instead.")]
        public void SetBasePath(String basePath)
        {
            // do nothing
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public IO.Swagger.Client.Configuration Configuration {get; set;}

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public IO.Swagger.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Gets the default header.
        /// </summary>
        /// <returns>Dictionary of HTTP header</returns>
        [Obsolete("DefaultHeader is deprecated, please use Configuration.DefaultHeader instead.")]
        public IDictionary<String, String> DefaultHeader()
        {
            return new ReadOnlyDictionary<string, string>(this.Configuration.DefaultHeader);
        }

        /// <summary>
        /// Add default header.
        /// </summary>
        /// <param name="key">Header field name.</param>
        /// <param name="value">Header field value.</param>
        /// <returns></returns>
        [Obsolete("AddDefaultHeader is deprecated, please use Configuration.AddDefaultHeader instead.")]
        public void AddDefaultHeader(string key, string value)
        {
            this.Configuration.AddDefaultHeader(key, value);
        }

        /// <summary>
        /// Get active currencies Returns an array with all active currencies.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>DefaultListCurrencyModel</returns>
        public DefaultListCurrencyModel GetActiveCurrencies ()
        {
             ApiResponse<DefaultListCurrencyModel> localVarResponse = GetActiveCurrenciesWithHttpInfo();
             return localVarResponse.Data;
        }

        /// <summary>
        /// Get active currencies Returns an array with all active currencies.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of DefaultListCurrencyModel</returns>
        public ApiResponse< DefaultListCurrencyModel > GetActiveCurrenciesWithHttpInfo ()
        {

            var localVarPath = "/v3/currencies/get";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);


            // authentication (API Key Authorization) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarHeaderParams["Authorization"] = this.Configuration.GetApiKeyWithPrefix("Authorization");
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetActiveCurrencies", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DefaultListCurrencyModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (DefaultListCurrencyModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DefaultListCurrencyModel)));
        }

        /// <summary>
        /// Get active currencies Returns an array with all active currencies.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of DefaultListCurrencyModel</returns>
        public async System.Threading.Tasks.Task<DefaultListCurrencyModel> GetActiveCurrenciesAsync ()
        {
             ApiResponse<DefaultListCurrencyModel> localVarResponse = await GetActiveCurrenciesAsyncWithHttpInfo();
             return localVarResponse.Data;

        }

        /// <summary>
        /// Get active currencies Returns an array with all active currencies.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (DefaultListCurrencyModel)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<DefaultListCurrencyModel>> GetActiveCurrenciesAsyncWithHttpInfo ()
        {

            var localVarPath = "/v3/currencies/get";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);


            // authentication (API Key Authorization) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarHeaderParams["Authorization"] = this.Configuration.GetApiKeyWithPrefix("Authorization");
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetActiveCurrencies", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DefaultListCurrencyModel>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (DefaultListCurrencyModel) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DefaultListCurrencyModel)));
        }

        /// <summary>
        /// Next scheduler Get the next scheduling time for hash submissions to the blockchain.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>DefaultSchedulerResponse</returns>
        public DefaultSchedulerResponse GetNextSchedulingTime (string authorization, SchedulerRequest schedulerRequest)
        {
             ApiResponse<DefaultSchedulerResponse> localVarResponse = GetNextSchedulingTimeWithHttpInfo(authorization, schedulerRequest);
             return localVarResponse.Data;
        }

        /// <summary>
        /// Next scheduler Get the next scheduling time for hash submissions to the blockchain.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>ApiResponse of DefaultSchedulerResponse</returns>
        public ApiResponse< DefaultSchedulerResponse > GetNextSchedulingTimeWithHttpInfo (string authorization, SchedulerRequest schedulerRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling SchedulerApi->GetNextSchedulingTime");
            // verify the required parameter 'schedulerRequest' is set
            if (schedulerRequest == null)
                throw new ApiException(400, "Missing required parameter 'schedulerRequest' when calling SchedulerApi->GetNextSchedulingTime");

            var localVarPath = "/v3/submission/times";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter
            if (schedulerRequest != null && schedulerRequest.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(schedulerRequest); // http body (model) parameter
            }
            else
            {
                localVarPostBody = schedulerRequest; // byte array
            }

            // authentication (API Key Authorization) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarHeaderParams["Authorization"] = this.Configuration.GetApiKeyWithPrefix("Authorization");
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetNextSchedulingTime", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DefaultSchedulerResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (DefaultSchedulerResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DefaultSchedulerResponse)));
        }

        /// <summary>
        /// Next scheduler Get the next scheduling time for hash submissions to the blockchain.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>Task of DefaultSchedulerResponse</returns>
        public async System.Threading.Tasks.Task<DefaultSchedulerResponse> GetNextSchedulingTimeAsync (string authorization, SchedulerRequest schedulerRequest)
        {
             ApiResponse<DefaultSchedulerResponse> localVarResponse = await GetNextSchedulingTimeAsyncWithHttpInfo(authorization, schedulerRequest);
             return localVarResponse.Data;

        }

        /// <summary>
        /// Next scheduler Get the next scheduling time for hash submissions to the blockchain.
        /// </summary>
        /// <exception cref="IO.Swagger.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="authorization">A valid API key is essential for authorization to handle the request.</param>
        /// <param name="schedulerRequest">Request DTO for next schedules.</param>
        /// <returns>Task of ApiResponse (DefaultSchedulerResponse)</returns>
        public async System.Threading.Tasks.Task<ApiResponse<DefaultSchedulerResponse>> GetNextSchedulingTimeAsyncWithHttpInfo (string authorization, SchedulerRequest schedulerRequest)
        {
            // verify the required parameter 'authorization' is set
            if (authorization == null)
                throw new ApiException(400, "Missing required parameter 'authorization' when calling SchedulerApi->GetNextSchedulingTime");
            // verify the required parameter 'schedulerRequest' is set
            if (schedulerRequest == null)
                throw new ApiException(400, "Missing required parameter 'schedulerRequest' when calling SchedulerApi->GetNextSchedulingTime");

            var localVarPath = "/v3/submission/times";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarHeaderParams = new Dictionary<String, String>(this.Configuration.DefaultHeader);
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // to determine the Content-Type header
            String[] localVarHttpContentTypes = new String[] {
                "application/json"
            };
            String localVarHttpContentType = this.Configuration.ApiClient.SelectHeaderContentType(localVarHttpContentTypes);

            // to determine the Accept header
            String[] localVarHttpHeaderAccepts = new String[] {
                "application/json"
            };
            String localVarHttpHeaderAccept = this.Configuration.ApiClient.SelectHeaderAccept(localVarHttpHeaderAccepts);
            if (localVarHttpHeaderAccept != null)
                localVarHeaderParams.Add("Accept", localVarHttpHeaderAccept);

            if (authorization != null) localVarHeaderParams.Add("Authorization", this.Configuration.ApiClient.ParameterToString(authorization)); // header parameter
            if (schedulerRequest != null && schedulerRequest.GetType() != typeof(byte[]))
            {
                localVarPostBody = this.Configuration.ApiClient.Serialize(schedulerRequest); // http body (model) parameter
            }
            else
            {
                localVarPostBody = schedulerRequest; // byte array
            }

            // authentication (API Key Authorization) required
            if (!String.IsNullOrEmpty(this.Configuration.GetApiKeyWithPrefix("Authorization")))
            {
                localVarHeaderParams["Authorization"] = this.Configuration.GetApiKeyWithPrefix("Authorization");
            }

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse) await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, localVarPostBody, localVarHeaderParams, localVarFormParams, localVarFileParams,
                localVarPathParams, localVarHttpContentType);

            int localVarStatusCode = (int) localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetNextSchedulingTime", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<DefaultSchedulerResponse>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (DefaultSchedulerResponse) this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(DefaultSchedulerResponse)));
        }

    }
}
