namespace eHandbook.BlazorWebApp.Shared.Test
{
    internal class ApiResponseToTest
    {
        // declare private jsonString field to hold the json string
        private static readonly string? jsonString;

        // Constructor to set the static jsonString field
        //A static constructor is used to initialize any static data, or to perform a particular action that needs to be performed only once
        static ApiResponseToTest()
        {
            jsonString = @"{
                    ""metadata"": {
                        ""Succeeded"": true,
                        ""Message"": ""Manuals retrieved successfully"",
                        ""MyCustomErrorMessage"": null
                    },
                    ""data"":[
                        {
                            ""id"": ""65b927ed-b563-4db4-9594-002fd5379178"",
                            ""Description"": ""How to use a computer"",  
                            ""Path"": ""C://User/Manuals/How to use a computer"",
                            ""AuditableDetails"": {
                                ""CreatedBy"": ""Admin"",
                                ""CreatedOn"": ""2021-10-01T00:00:00"",
                                ""UpdatedBy"": ""Admin"",
                                ""UpdatedOn"": ""2021-10-01T00:00:00"",
                                ""IsUpdated"": false,
                                ""DeletedOn"": null,
                                ""DeletedBy"": null,
                                ""IsDeleted"": false
                            }
                        },
                        {
                            ""id"": ""c77d57f3-2ad9-4f20-9454-006b53df3055"",
                            ""Description"": ""How to use a printer"",  
                            ""Path"": ""C://User/Manuals/How to use a printer"",
                            ""AuditableDetails"": {
                                ""CreatedBy"": ""Admin"",
                                ""CreatedOn"": ""2021-10-01T00:00:00"",
                                ""UpdatedBy"": ""Admin"",
                                ""UpdatedOn"": ""2021-10-01T00:00:00"",
                                ""IsUpdated"": false,
                                ""DeletedOn"": null,
                                ""DeletedBy"": null,
                                ""IsDeleted"": false
                            }
                        }
                    ]
                }";
        }

        // Method to return the jsonString
        public static string GetJsonString()
        {
            return jsonString;
        }
    }
}
