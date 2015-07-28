using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using PPAQoRdotnet.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PPAQoRdotnet.Controllers
{
    public class MetricsController : ApiController
    {
		//[HttpPost]
		
		
		
		[ActionName("GetMetrics")]
		public List<Metrics> Get()  //Get(int id) 
		{


			using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString()))
			{

				//string querystring = "Select problem, count(*) as counted from tblRCSIssues group by problem";

				var builder = new StringBuilder();
				builder.Append("SELECT ");
				builder.Append(" REPLACE(f1.value,'\"', '') AS 'apr_Cell_Count',");
				builder.Append(" REPLACE(f2.value,'\"', '') AS 'Test Case Name',");
				builder.Append(" REPLACE(f3.value,'\"', '') AS 'Kit',");
				builder.Append(" [proc].*");
				builder.Append(" FROM test.[proc] ");
				builder.Append(" left join test.data f1 on [proc].stem = f1.stem and [proc].proc_id= f1.proc_id and [proc].run = f1.run and f1.field = 'apr_Cell_Count'");
				builder.Append(" left join test.data f2 on [proc].stem = f2.stem and [proc].proc_id= f2.proc_id and [proc].run = f2.run and f2.value = '\"fdkex_sdc_scan_multivt\"'");
				builder.Append(" left join test.data f3 on [proc].stem = f3.stem and [proc].proc_id= f3.proc_id and [proc].run = f3.run and f3.field = 'Kit'");
				builder.Append(" WHERE ");
				builder.Append("[user]='tjstickx' ");
				builder.Append(" and [project] =  'PPAQoR' ");
				builder.Append(" and[proc].run > '2015-07-14' ");
				builder.Append(" and f2.value IS NOT NULL ");
				
				string innerString = builder.ToString();


				SqlCommand sqlCmd = new SqlCommand(innerString, con);

				SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = sqlCmd;
				DataTable dt = new DataTable();
				da.Fill(dt);


				List<Metrics> metrics = new List<Metrics>();

				foreach (DataRow dtrow in dt.Rows)
				{
					var metric = new Metrics();
					metric.MetricName = dtrow[0].ToString();
					metric.TestCaseName = dtrow[1].ToString(); //Convert.ToInt32(dtrow[1]);

					metrics.Add(metric);

				}

				return metrics;


			}


		}





    }


}
