# IO.Swagger.Api.WebhookApi

All URIs are relative to *https://api.originstamp.org*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetWebhookStatus**](WebhookApi.md#getwebhookstatus) | **POST** /v3/webhook/information | Webhook
[**RegisterWebhookNotification**](WebhookApi.md#registerwebhooknotification) | **POST** /v3/webhook/register | Webhook
[**TriggerTimestampWebhook**](WebhookApi.md#triggertimestampwebhook) | **POST** /v3/webhook/start | Dev


<a name="getwebhookstatus"></a>
# **GetWebhookStatus**
> DefaultWebhookResponse GetWebhookStatus (string authorization, WebhookRequest webhookRequest)

Webhook

RESTful interface to receive the status of a webhook. Based on the input parameters (target URL, hash and currency), we look up the most recent entry in the notification queue.This method is intended to support the webhook integration.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetWebhookStatusExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WebhookApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var webhookRequest = new WebhookRequest(); // WebhookRequest | DTO for registering webhook information.

            try
            {
                // Webhook
                DefaultWebhookResponse result = apiInstance.GetWebhookStatus(authorization, webhookRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WebhookApi.GetWebhookStatus: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **webhookRequest** | [**WebhookRequest**](WebhookRequest.md)| DTO for registering webhook information. | 

### Return type

[**DefaultWebhookResponse**](DefaultWebhookResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="registerwebhooknotification"></a>
# **RegisterWebhookNotification**
> Default RegisterWebhookNotification (string authorization, WebhookRequest webhookRequest)

Webhook

Method which allows a subscription for a webhook notification. If this method is called, a new entry is added to notification queue that is triggered as soon as a tamper-proof timestamp or the hash is created. An empty data payload means that the entry was created successfully.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class RegisterWebhookNotificationExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WebhookApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var webhookRequest = new WebhookRequest(); // WebhookRequest | DTO for querying webhook information.

            try
            {
                // Webhook
                Default result = apiInstance.RegisterWebhookNotification(authorization, webhookRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WebhookApi.RegisterWebhookNotification: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **webhookRequest** | [**WebhookRequest**](WebhookRequest.md)| DTO for querying webhook information. | 

### Return type

[**Default**](Default.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

<a name="triggertimestampwebhook"></a>
# **TriggerTimestampWebhook**
> Default TriggerTimestampWebhook (string authorization, ManualWebhookRequest manualWebhookRequest)

Dev

With this interface you can trigger manual webhook to see how a webhook looks like. Please use a hash, that was already timestamped before such as https://redir.originstamp.com/hash/9f86d081884c7d659a2feaa0c55ad015a3bf4f1b2b0b822cd15d6c15b0f00a08 . Usually, the webhook is triggered as soon as the tamper-proof time stamp with the selected crypto currency has been created.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class TriggerTimestampWebhookExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new WebhookApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var manualWebhookRequest = new ManualWebhookRequest(); // ManualWebhookRequest | DTO for webhook request.

            try
            {
                // Dev
                Default result = apiInstance.TriggerTimestampWebhook(authorization, manualWebhookRequest);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling WebhookApi.TriggerTimestampWebhook: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **manualWebhookRequest** | [**ManualWebhookRequest**](ManualWebhookRequest.md)| DTO for webhook request. | 

### Return type

[**Default**](Default.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

