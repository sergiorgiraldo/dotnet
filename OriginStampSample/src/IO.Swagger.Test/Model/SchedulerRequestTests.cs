/* 
 * OriginStamp API Documentation
 *
 * The following documentation describes the API v3 for OriginStamp. OriginStamp is a trusted timestamping service that uses the decentralized blockchain to store anonymous, tamper-proof timestamps for any digital content. OriginStamp allows users to timestamp files, emails, or plain text, and subsequently store the created hashes in the blockchain as well as retrieve and verify timetamps that have been committed to the blockchain.The trusted timestamping service of OriginStamp allows you to generate a hash fingerprint and prove that it was created at a specific point in time. If you are interested in integrating trusted timestamping into your own project, feel free to use our provided API. The following interactive documentation describes the interfaces and supports your integration. With this documentation you are able to try out the different requests and see the responses. For the authorization, add your API key to the Authorization header of your request.<br/><h2>Timestamping Steps</h2><ol><li><strong>Determine Hash: </strong> Calculate the SHA-256 of your record using a cryptographic library.</li><li><strong>Create Timestamp: </strong>Create a timestamp and add meta information to index it, e.g. a comment. You can also request a notification (email or webhook) once the tamper-proof timestamp has been created.</li><li><strong>Archive original file: </strong>Since we have no access to your original data, you should archive it because the timestamp is only valid in combination with the original file.</li><li><strong>Check Timestamp Status: </strong>Since the timestamps are always transmitted to the blockchain network at certain times, i.e. there is a delay, you can check the status of a hash and thus get the timestamp information.</li><li><strong>Get Timestamp Proof: </strong>As soon as the tamper-proof timestamp has been generated, you should archive the proof (Merkle Tree), which we created in our open procedure, together with the original file. With this proof, the existence of the file can be verified independently of OriginStamp. Here you can choose if the raw proof (xml) is sufficient proof or if you want to have a certificate (pdf).</li></ol><br/><h2>Installation Notes</h2><ul><li>Make sure you set the Authorization header correctly using your API key.</li><li>If a Cloudflare error occurs, please set a custom UserAgent header.</li><li>Please have a look at the models below to find out what each field means.</li></ul>
 *
 * OpenAPI spec version: 3.0
 * Contact: mail@originstamp.com
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */


using NUnit.Framework;

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using IO.Swagger.Api;
using IO.Swagger.Model;
using IO.Swagger.Client;
using System.Reflection;
using Newtonsoft.Json;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing SchedulerRequest
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the model.
    /// </remarks>
    [TestFixture]
    public class SchedulerRequestTests
    {
        // TODO uncomment below to declare an instance variable for SchedulerRequest
        //private SchedulerRequest instance;

        /// <summary>
        /// Setup before each test
        /// </summary>
        [SetUp]
        public void Init()
        {
            // TODO uncomment below to create an instance of SchedulerRequest
            //instance = new SchedulerRequest();
        }

        /// <summary>
        /// Clean up after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of SchedulerRequest
        /// </summary>
        [Test]
        public void SchedulerRequestInstanceTest()
        {
            // TODO uncomment below to test "IsInstanceOfType" SchedulerRequest
            //Assert.IsInstanceOfType<SchedulerRequest> (instance, "variable 'instance' is a SchedulerRequest");
        }


        /// <summary>
        /// Test the property 'Currency'
        /// </summary>
        [Test]
        public void CurrencyTest()
        {
            // TODO unit test for the property 'Currency'
        }

    }

}
