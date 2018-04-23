using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
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
            _deviceInfo = pDeviceInfo;
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
                if (_reportParams != null && _reportParams.Count > 0)
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

        public static string GetDeviceInfoXML(string pOutputFormat = "Pdf", float pPageWith = 8.5f,
            float pPageHeight = 11f, float pMarginTop = 0.2f, float pMarginLeft = 0.3f,
            float pMarginRight = 0.1f, float pMarginBottom = 0.2f)
        {
            string layout = "<DeviceInfo>" +
                    $"<OutputFormat>{pOutputFormat}</OutputFormat><PageWidth>{pPageWith}in</PageWidth>" +
                    $"<PageHeight>{pPageHeight}in</PageHeight><MarginTop>{pMarginTop}in</MarginTop>" +
                    $"<MarginLeft>{pMarginLeft}in</MarginLeft><MarginRight>{pMarginRight}in</MarginRight>" +
                    $"<MarginBottom>{pMarginBottom}in</MarginBottom>" +
                    "</DeviceInfo>";
            return layout;
        }
    }
}
