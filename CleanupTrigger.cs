using System;  
using Microsoft.AspNetCore.Mvc;  
using Microsoft.Azure.WebJobs;  
using Microsoft.Extensions.Logging;  
using System.Collections.Generic; 
using System.Data.SqlClient; 

namespace Company.Function
{
    public class CleanupTrigger
    {
        [FunctionName("CleanupTrigger")]
        public void Run([TimerTrigger("*/10 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            List<TodoModel> taskList = new List<TodoModel>();  
             try  
            {  
                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("SqlConnectionString")))  
                {  
                    connection.Open();  
                    var query = @"Select * from todos";  
                    SqlCommand command = new SqlCommand(query, connection);  
                    var reader = await command.ExecuteReaderAsync();  
                    while (reader.Read())  
                    {  
                        TodoModel task = new TodoModel()  
                        {  
                            Id = reader["Id"].ToString(),  
                            TaskDescription = reader["TaskDescription"].ToString(),  
                            CreatedTime = reader["CreatedTime"].ToString(),  
                            IsCompleted = (bool)reader["IsCompleted"]  
                        };  
                        taskList.Add(task);  
                    }  
                }  
            }  
            catch (Exception e)  
            {  
                log.LogError(e.ToString());  
            }

        }
    }
}
