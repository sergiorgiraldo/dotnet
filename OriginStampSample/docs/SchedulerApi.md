# IO.Swagger.Api.SchedulerApi

All URIs are relative to *https://api.originstamp.org*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetActiveCurrencies**](SchedulerApi.md#getactivecurrencies) | **GET** /v3/currencies/get | Get active currencies
[**GetNextSchedulingTime**](SchedulerApi.md#getnextschedulingtime) | **POST** /v3/submission/times | Next scheduler


<a name="getactivecurrencies"></a>
# **GetActiveCurrencies**
> DefaultListCurrencyModel GetActiveCurrencies ()

Get active currencies

Returns an array with all active currencies.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetActiveCurrenciesExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new SchedulerApi();

            try
            {
                // Get active currencies
                DefaultListCurrencyModel result = apiInstance.GetActiveCurrencies();
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SchedulerApi.GetActiveCurrencies: " + e.Message );
            }
        }
    }
}
```

### Parameters
This endpoint does not need any parameter.

### Return type

[**DefaultListCurrencyModel**](DefaultListCurrencyModel.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getnextschedulingtime"></a>
# **GetNextSchedulingTime**
> DefaultSchedulerResponse GetNextSchedulingTime (string authorization, SchedulerRequest schedulerRequest)

Next scheduler

Get the next scheduling time for hash submissions to the blockchain.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetNextSchedulingTimeExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new SchedulerApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var schedulerRequest = new SchedulerRequest(); // SchedulerRequest | Request DTO for next schedules.

            try
            {
                // Next scheduler
                DefaultSchedulerResponse result = apiInstance.GetNextSchedulingTime(authorization, schedulerRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling SchedulerApi.GetNextSchedulingTime: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **schedulerRequest** | [**SchedulerRequest**](SchedulerRequest.md)| Request DTO for next schedules. | 

### Return type

[**DefaultSchedulerResponse**](DefaultSchedulerResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

