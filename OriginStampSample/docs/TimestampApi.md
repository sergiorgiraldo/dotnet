# IO.Swagger.Api.TimestampApi

All URIs are relative to *https://api.originstamp.org*

Method | HTTP request | Description
------------- | ------------- | -------------
[**CreateTimestamp**](TimestampApi.md#createtimestamp) | **POST** /v3/timestamp/create | Submission
[**GetApiKeyUsage**](TimestampApi.md#getapikeyusage) | **GET** /v3/api_key/usage | Usage
[**GetHashStatus**](TimestampApi.md#gethashstatus) | **GET** /v3/timestamp/{hash_string} | Status


<a name="createtimestamp"></a>
# **CreateTimestamp**
> DefaultTimestampResponse CreateTimestamp (string authorization, TimestampRequest timestampRequest)

Submission

You can submit your hash with this function. If your api key is valid, your hash is added to batch and is scheduled for timestamping. If the hash already exists, the created flag in the response is set to false and the notification(s) of the current request will be totally ignored. You are also able to submit additional information, such as comment or notification credentials. Once a hash is successfully created for a certain crypto-currency, we can notify your desired target with the timestamp information (POST Request). The webhook is triggered as soon as the tamper-proof timestamp with the selected crypto currency has been created. Additionally, it is possible to include a preprint URL in the hash submission. Before the generation of the timestamp hash you can create a random UUID Version 4 and include https://originstamp.com/u/UUID where UUID is your UUID e.g. in a document you want to timestamp. In the preprint URL field you include your UUID and then it is possible to verify the timestamp within the document (or whatever). 

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class CreateTimestampExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TimestampApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var timestampRequest = new TimestampRequest(); // TimestampRequest | DTO for the hash submission. Add all relevant information concerning your hash submission.

            try
            {
                // Submission
                DefaultTimestampResponse result = apiInstance.CreateTimestamp(authorization, timestampRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimestampApi.CreateTimestamp: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **timestampRequest** | [**TimestampRequest**](TimestampRequest.md)| DTO for the hash submission. Add all relevant information concerning your hash submission. | 

### Return type

[**DefaultTimestampResponse**](DefaultTimestampResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="getapikeyusage"></a>
# **GetApiKeyUsage**
> DefaultUsageResponse GetApiKeyUsage (string authorization)

Usage

With this interface you can receive the current api usage.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetApiKeyUsageExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TimestampApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.

            try
            {
                // Usage
                DefaultUsageResponse result = apiInstance.GetApiKeyUsage(authorization);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimestampApi.GetApiKeyUsage: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 

### Return type

[**DefaultUsageResponse**](DefaultUsageResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: */*

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="gethashstatus"></a>
# **GetHashStatus**
> DefaultTimestampResponse GetHashStatus (string authorization, string hashString)

Status

The request returns information of a certain hash read from the URL parameter. The input parameter is a hash in hex representation. Field \"created\" always set to false.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetHashStatusExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new TimestampApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var hashString = hashString_example;  // string | The hash in string representation.

            try
            {
                // Status
                DefaultTimestampResponse result = apiInstance.GetHashStatus(authorization, hashString);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling TimestampApi.GetHashStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **hashString** | **string**| The hash in string representation. | 

### Return type

[**DefaultTimestampResponse**](DefaultTimestampResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: Not defined
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

