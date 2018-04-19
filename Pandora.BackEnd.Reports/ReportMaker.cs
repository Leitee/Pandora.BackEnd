using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace Pandora.BackEnd.Reports
{
    public class ReportMaker
    {
        const string reportType = "pdf";
        private string _fileName;
        private string _dataSourceName;
        private object _reportData;
        private string _deviceInfo;
        private List<ReportParameter> _reportParams;

        /// <summary>
        /// Create an instance of report without header params 
        /// </summary>
        /// <param name="pFileName">Name of .rdlc File to use.</param>
        /// <param name="pDataSourceName">Name of DataSet which is binding with the report</param>
        /// <param name="pReportData">Data list that is shown at the report</param>
        /// <param name="pDeviceInfo">XML configuration for device ouput size</param>

        public ReportMaker(string pFileName, string pDataSourceName, object pReportData, string pDeviceInfo)
        {
            _fileName = pFileName;
            _dataSourceName = pDataSourceName;
            _reportData = pReportData;
        }

        /// <summary>
        /// Create an instance of report with header params
        /// </summary>
        /// <param name="pFileName">Name of .rdlc File to use.</param>
        /// <param name="pDataSourceName">Name of DataSet which is binding with the report</param>
        /// <param name="pReportData">Data list that is shown at the report</param>
        /// <param name="pDeviceInfo">XML configuration for device ouput size</param>
        /// <param name="pReportParams">Lista of parameters for report header</param>
        public ReportMaker(string pFileName, string pDataSourceName, object pReportData, string pDeviceInfo, List<ReportParameter> pReportParams)
        {
            _fileName = pFileName;
            _dataSourceName = pDataSourceName;
            _reportData = pReportData;
            _reportParams = pReportParams;
        }

        public byte[] Create()
        {
                var lr = new LocalReport();
                string path = string.Empty;

            try
            {

                path = Path.Combine(HostingEnvironment.MapPath("~/Reports"), _fileName + ".rdlc");

                if (File.Exists(path))
                {
                    lr.ReportPath = path;
                }
                else
                {
                    throw new Exception("Path not found");
                }


                //add parameters
                if(_reportParams != null && _reportParams.Count > 0)
                    lr.SetParameters(_reportParams);

                //Render the report    
                var rd = new ReportDataSource(_dataSourceName, _reportData);
                lr.DataSources.Add(rd);

                return lr.Render(reportType, _deviceInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetDeviceInfoXML()
        {
            return "<DeviceInfo>" +
                    "  <OutputFormat>Pdf</OutputFormat>" +
                    "  <PageWidth>8.5in</PageWidth>" +
                    "  <PageHeight>11in</PageHeight>" +
                    "  <MarginTop>0.2in</MarginTop>" +
                    "  <MarginLeft>0.3in</MarginLeft>" +
                    "  <MarginRight>0.1in</MarginRight>" +
                    "  <MarginBottom>0.2in</MarginBottom>" +
                    "</DeviceInfo>";
        }
    }
}
