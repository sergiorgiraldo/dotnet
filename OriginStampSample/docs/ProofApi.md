# IO.Swagger.Api.ProofApi

All URIs are relative to *https://api.originstamp.org*

Method | HTTP request | Description
------------- | ------------- | -------------
[**GetProof**](ProofApi.md#getproof) | **POST** /v3/timestamp/proof/url | Proof


<a name="getproof"></a>
# **GetProof**
> DefaultDownloadLinkResponse GetProof (string authorization, ProofRequest proofRequestUrl)

Proof

Generates the download URL for Proof (Seed / Merkle Tree). This interface must be used to obtain the proof or certificate of your tamper-proof timestamp. The parameters are as follows: Cryptocurrency (e.g., Bitcoin, Ethereum,..), type of evidence (e.g., certificate, merkle tree) and the associated hash. The entries are analyzed, e.g., whether a valid timestamp exists for the hash. Then the URL and the filename are returned, with which your proof can be saved. Please note that the download link is only valid for 5 minutes.

### Example
```csharp
using System;
using System.Diagnostics;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Example
{
    public class GetProofExample
    {
        public void main()
        {
            // Configure API key authorization: API Key Authorization
            Configuration.Default.AddApiKey("Authorization", "YOUR_API_KEY");
            // Uncomment below to setup prefix (e.g. Bearer) for API key, if needed
            // Configuration.Default.AddApiKeyPrefix("Authorization", "Bearer");

            var apiInstance = new ProofApi();
            var authorization = authorization_example;  // string | A valid API key is essential for authorization to handle the request.
            var proofRequestUrl = new ProofRequest(); // ProofRequest | Information needed to return the proof.

            try
            {
                // Proof
                DefaultDownloadLinkResponse result = apiInstance.GetProof(authorization, proofRequestUrl);
                Debug.WriteLine(result);
            }
            catch (Exception e)
            {
                Debug.Print("Exception when calling ProofApi.GetProof: " + e.Message );
            }
        }
    }
}
```

### Parameters

Name | Type | Description  | Notes
------------- | ------------- | ------------- | -------------
 **authorization** | **string**| A valid API key is essential for authorization to handle the request. | 
 **proofRequestUrl** | [**ProofRequest**](ProofRequest.md)| Information needed to return the proof. | 

### Return type

[**DefaultDownloadLinkResponse**](DefaultDownloadLinkResponse.md)

### Authorization

[API Key Authorization](../README.md#API Key Authorization)

### HTTP request headers

 - **Content-Type**: application/json
 - **Accept**: application/json

[[Back to top]](#) [[Back to API list]](../README.md#documentation-for-api-endpoints) [[Back to Model list]](../README.md#documentation-for-models) [[Back to README]](../README.md)

