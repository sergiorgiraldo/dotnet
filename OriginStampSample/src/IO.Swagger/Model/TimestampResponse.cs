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
    /// Response object for the timestamp response. Create, Status and Webhookshare the same object. This saves customers additional implementation work, as the requests or data only have to be understood once.The difference is that the webhook is only triggered as soon as a tamper-proof timestamp exists.
    /// </summary>
    [DataContract]
    public partial class TimestampResponse :  IEquatable<TimestampResponse>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimestampResponse" /> class.
        /// </summary>
        /// <param name="comment">The comment which was added in the submission of the hash..</param>
        /// <param name="created">Field is set to true if it is a novel hash.If the flag is false, the hash was already submitted before..</param>
        /// <param name="dateCreated">The time when your hash was submitted to OriginStamp. The date is returned in the following format: [ms] since 1.1.1970 (unix epoch), timezone: UTC. This is not considered as a true timestamp..</param>
        /// <param name="hashString">The submitted hash in hex representation..</param>
        /// <param name="timestamps">Contains all the timestamp data of your hash until now..</param>
        public TimestampResponse(string comment = default(string), bool? created = default(bool?), long? dateCreated = default(long?), string hashString = default(string), List<TimestampData> timestamps = default(List<TimestampData>))
        {
            this.Comment = comment;
            this.Created = created;
            this.DateCreated = dateCreated;
            this.HashString = hashString;
            this.Timestamps = timestamps;
        }
        
        /// <summary>
        /// The comment which was added in the submission of the hash.
        /// </summary>
        /// <value>The comment which was added in the submission of the hash.</value>
        [DataMember(Name="comment", EmitDefaultValue=false)]
        public string Comment { get; set; }

        /// <summary>
        /// Field is set to true if it is a novel hash.If the flag is false, the hash was already submitted before.
        /// </summary>
        /// <value>Field is set to true if it is a novel hash.If the flag is false, the hash was already submitted before.</value>
        [DataMember(Name="created", EmitDefaultValue=false)]
        public bool? Created { get; set; }

        /// <summary>
        /// The time when your hash was submitted to OriginStamp. The date is returned in the following format: [ms] since 1.1.1970 (unix epoch), timezone: UTC. This is not considered as a true timestamp.
        /// </summary>
        /// <value>The time when your hash was submitted to OriginStamp. The date is returned in the following format: [ms] since 1.1.1970 (unix epoch), timezone: UTC. This is not considered as a true timestamp.</value>
        [DataMember(Name="date_created", EmitDefaultValue=false)]
        public long? DateCreated { get; set; }

        /// <summary>
        /// The submitted hash in hex representation.
        /// </summary>
        /// <value>The submitted hash in hex representation.</value>
        [DataMember(Name="hash_string", EmitDefaultValue=false)]
        public string HashString { get; set; }

        /// <summary>
        /// Contains all the timestamp data of your hash until now.
        /// </summary>
        /// <value>Contains all the timestamp data of your hash until now.</value>
        [DataMember(Name="timestamps", EmitDefaultValue=false)]
        public List<TimestampData> Timestamps { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TimestampResponse {\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  Created: ").Append(Created).Append("\n");
            sb.Append("  DateCreated: ").Append(DateCreated).Append("\n");
            sb.Append("  HashString: ").Append(HashString).Append("\n");
            sb.Append("  Timestamps: ").Append(Timestamps).Append("\n");
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
            return this.Equals(input as TimestampResponse);
        }

        /// <summary>
        /// Returns true if TimestampResponse instances are equal
        /// </summary>
        /// <param name="input">Instance of TimestampResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TimestampResponse input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Comment == input.Comment ||
                    (this.Comment != null &&
                    this.Comment.Equals(input.Comment))
                ) && 
                (
                    this.Created == input.Created ||
                    (this.Created != null &&
                    this.Created.Equals(input.Created))
                ) && 
                (
                    this.DateCreated == input.DateCreated ||
                    (this.DateCreated != null &&
                    this.DateCreated.Equals(input.DateCreated))
                ) && 
                (
                    this.HashString == input.HashString ||
                    (this.HashString != null &&
                    this.HashString.Equals(input.HashString))
                ) && 
                (
                    this.Timestamps == input.Timestamps ||
                    this.Timestamps != null &&
                    this.Timestamps.SequenceEqual(input.Timestamps)
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
                if (this.Comment != null)
                    hashCode = hashCode * 59 + this.Comment.GetHashCode();
                if (this.Created != null)
                    hashCode = hashCode * 59 + this.Created.GetHashCode();
                if (this.DateCreated != null)
                    hashCode = hashCode * 59 + this.DateCreated.GetHashCode();
                if (this.HashString != null)
                    hashCode = hashCode * 59 + this.HashString.GetHashCode();
                if (this.Timestamps != null)
                    hashCode = hashCode * 59 + this.Timestamps.GetHashCode();
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