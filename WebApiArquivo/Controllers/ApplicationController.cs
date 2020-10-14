using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Intrinsics;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Data;
using WebApplication1.Models;
using static System.Net.WebRequestMethods;

namespace WebApplication1.Controllers
{

    [ApiController]
    public class ApplicationController : Controller
    {
        public SqlConnection sqlConnection = new SqlConnection("Data Source =DESKTOP-8A19E8K\\SQLEXPRESS; Initial Catalog = master;" + "Integrated Security=SSPI;");

        [HttpGet]
        [Route("v1/application")]
        public async Task<ActionResult<List<Application>>> Get()
        {
            //sqlConnection.Open();
           
           
            string sqlCommand = "select*from  Application";
           

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);
            var arrayApps = new List<Application>();
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    Application App = new Application();
                    App.application = Convert.ToInt32(reader[0]);
                    App.url = Convert.ToString(reader[1]);
                    App.pathLocal = Convert.ToString(reader[2]);
                    App.debuggingMode = Convert.ToBoolean(reader[3]);

                    arrayApps.Add(App);

                }
            }
            sqlConnection.Close();

            return arrayApps;
        }
        [HttpGet]
        [Route("v1/application/{id:int}")]
        public async Task<ActionResult<Application>> Get(string id)
        {
            sqlConnection.Open();


            string sqlCommand = "select*from  application " ;
            if (id != null)
                sqlCommand += "where APPLICATION = " + id;

            SqlCommand command = new SqlCommand(sqlCommand, sqlConnection);
            var arrayApps = new Application();
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    Application App = new Application();
                    App.application = Convert.ToInt32(reader[0]);
                    App.url = Convert.ToString(reader[1]);                    
                    App.pathLocal = Convert.ToString(reader[2]);
                    App.debuggingMode = Convert.ToBoolean(reader[3]);
                    

                    arrayApps = App;

                }
            }
            sqlConnection.Close();

            return arrayApps;
        }

        [HttpPost]
        [Route("v1/application")]
        public async Task<ActionResult<Application>> Post(
            [FromBody] Application model)
        {
            string sql = "INSERT INTO Application(application, url, pathLocal, debuggingMode) VALUES(@application,@url, @pathLocal, @debuggingMode)";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            
            
            if (ModelState.IsValid)
            {
                command.Parameters.Add(new SqlParameter("@application", model.application));
                command.Parameters.Add(new SqlParameter("@url", model.url));
                command.Parameters.Add(new SqlParameter("@pathLocal", model.pathLocal));
                command.Parameters.Add(new SqlParameter("@debuggingMode", Convert.ToInt32(model.debuggingMode)));
                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();
                return Ok();
            }
            

            return BadRequest(ModelState);            

           
           
        }
        [HttpPatch]
        [Route("v1/application/{id:int}")]
        public async Task<ActionResult<Application>> Patch(string id,[FromBody] Application model)
        {
           
            string sql = "update Application set ";

            if (ModelState.IsValid)
            {
                if (model.pathLocal != null)
                {
                    string sqlAtribute = sql + "pathLocal = '" + model.pathLocal.ToString() + "'";
                    SqlCommand command = new SqlCommand(sqlAtribute, sqlConnection);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                if (model.url != null)
                {
                    string sqlAtribute = sql + "url = '" + model.url.ToString() + "'";
                    SqlCommand command = new SqlCommand(sqlAtribute, sqlConnection);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                if (model.pathLocal != null)
                {
                    string sqlAtribute = sql + "debuggingMode = '" + Convert.ToInt32(model.debuggingMode).ToString()+ "'";
                    SqlCommand command = new SqlCommand(sqlAtribute, sqlConnection);
                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                    sqlConnection.Close();
                }

                return Ok();
            }
            

            return BadRequest(ModelState);



        }
        [HttpDelete]
        [Route("v1/application/{id:int}")]
        public async Task<ActionResult<Application>> Delete(string id)
        {

            sqlConnection.Open();
            
            string sCommand = "select*from Application where APPLICATION = " + id;

            SqlCommand command = new SqlCommand(sCommand, sqlConnection);
            var Apps = new Application();
            using (SqlDataReader reader = command.ExecuteReader())
            {

                while (reader.Read())
                {

                    Application App = new Application();
                    App.application = Convert.ToInt32(reader[0]);
                    App.url = Convert.ToString(reader[1]);
                    App.pathLocal = Convert.ToString(reader[2]);
                    App.debuggingMode = Convert.ToBoolean(reader[3]);

                    Apps = App;

                }
                if (Apps == null)
                    return BadRequest();
            }
            sqlConnection.Close();

            if (Apps != null)
            {
                string deleteComand = "delete from Application where APPLICATION = " + id;
                SqlCommand sqlCommandDelete = new SqlCommand(deleteComand, sqlConnection);
                sqlConnection.Open();
                sqlCommandDelete.ExecuteNonQuery();
                sqlConnection.Close();
            }

            return Ok();
        }
    }
}
