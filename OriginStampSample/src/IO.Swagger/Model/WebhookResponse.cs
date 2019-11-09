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
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// response object for a webhook request. Contains only the most recent webhook information for target URL, hash and currency.
    /// </summary>
    [DataContract]
    public partial class WebhookResponse :  IEquatable<WebhookResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookResponse" /> class.
        /// </summary>
        /// <param name="currency">Currency for which the webhook is triggered, e.g.  0: Bitcoin 1: Ethereum.</param>
        /// <param name="executed">Shows if the webhook was executed..</param>
        /// <param name="hash">The submitted hash in hex representation..</param>
        /// <param name="success">Indicates whether the webhook was executed successfully or not..</param>
        /// <param name="tries">Returns the number of tries for the webhook execution..</param>
        public WebhookResponse(int? currency = default(int?), bool? executed = default(bool?), string hash = default(string), bool? success = default(bool?), int? tries = default(int?))
        {
            this.Currency = currency;
            this.Executed = executed;
            this.Hash = hash;
            this.Success = success;
            this.Tries = tries;
        }
        
        /// <summary>
        /// Currency for which the webhook is triggered, e.g.  0: Bitcoin 1: Ethereum
        /// </summary>
        /// <value>Currency for which the webhook is triggered, e.g.  0: Bitcoin 1: Ethereum</value>
        [DataMember(Name="currency", EmitDefaultValue=false)]
        public int? Currency { get; set; }

        /// <summary>
        /// Shows if the webhook was executed.
        /// </summary>
        /// <value>Shows if the webhook was executed.</value>
        [DataMember(Name="executed", EmitDefaultValue=false)]
        public bool? Executed { get; set; }

        /// <summary>
        /// The submitted hash in hex representation.
        /// </summary>
        /// <value>The submitted hash in hex representation.</value>
        [DataMember(Name="hash", EmitDefaultValue=false)]
        public string Hash { get; set; }

        /// <summary>
        /// Indicates whether the webhook was executed successfully or not.
        /// </summary>
        /// <value>Indicates whether the webhook was executed successfully or not.</value>
        [DataMember(Name="success", EmitDefaultValue=false)]
        public bool? Success { get; set; }

        /// <summary>
        /// Returns the number of tries for the webhook execution.
        /// </summary>
        /// <value>Returns the number of tries for the webhook execution.</value>
        [DataMember(Name="tries", EmitDefaultValue=false)]
        public int? Tries { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class WebhookResponse {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Executed: ").Append(Executed).Append("\n");
            sb.Append("  Hash: ").Append(Hash).Append("\n");
            sb.Append("  Success: ").Append(Success).Append("\n");
            sb.Append("  Tries: ").Append(Tries).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as WebhookResponse);
        }

        /// <summary>
        /// Returns true if WebhookResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of WebhookResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WebhookResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.Executed == input.Executed ||
                    (this.Executed != null &&
                    this.Executed.Equals(input.Executed))
                ) && 
                (
                    this.Hash == input.Hash ||
                    (this.Hash != null &&
                    this.Hash.Equals(input.Hash))
                ) && 
                (
                    this.Success == input.Success ||
                    (this.Success != null &&
                    this.Success.Equals(input.Success))
                ) && 
                (
                    this.Tries == input.Tries ||
                    (this.Tries != null &&
                    this.Tries.Equals(input.Tries))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.Executed != null)
                    hashCode = hashCode * 59 + this.Executed.GetHashCode();
                if (this.Hash != null)
                    hashCode = hashCode * 59 + this.Hash.GetHashCode();
                if (this.Success != null)
                    hashCode = hashCode * 59 + this.Success.GetHashCode();
                if (this.Tries != null)
                    hashCode = hashCode * 59 + this.Tries.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
