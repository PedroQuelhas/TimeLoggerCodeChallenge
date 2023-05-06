/*
 * Periwinkle API
 *
 * CRUD management of Periwinkle
 *
 * The version of the OpenAPI document: 1.1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using ServerApi.CodeGen.Converters;

namespace ServerApi.CodeGen.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PaginationDTO 
    {
        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [Required]
        [DataMember(Name="page", EmitDefaultValue=true)]
        public int Page { get; set; }

        /// <summary>
        /// Gets or Sets PerPage
        /// </summary>
        [Required]
        [DataMember(Name="per_page", EmitDefaultValue=true)]
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or Sets TotalRecords
        /// </summary>
        [Required]
        [DataMember(Name="total_records", EmitDefaultValue=true)]
        public int TotalRecords { get; set; }

    }
}
