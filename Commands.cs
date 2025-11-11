using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using System;
using System.IO;

namespace RevitPlugin
{
	[Transaction(TransactionMode.Manual)]
	[Regeneration(RegenerationOption.Manual)]
	public class Commands : IExternalDBApplication
	{

		public ExternalDBApplicationResult OnStartup(ControlledApplication application)
		{
			DesignAutomationBridge.DesignAutomationReadyEvent += HandleDesignAutomationReadyEvent;
			return ExternalDBApplicationResult.Succeeded;
		}

		private void HandleDesignAutomationReadyEvent(object? sender, DesignAutomationReadyEventArgs e)
		{
			LogTrace("Design Automation Ready event triggered...");
			e.Succeeded = true;
            SampleMethod();
		}

        private void SampleMethod()
        {
            //Every log goes to the Automation workitem report
            Console.WriteLine("Sample method called");
			//Add your code here
        }

		/// <summary>
		/// This will appear on the Design Automation output
		/// </summary>
		private static void LogTrace(string format, params object[] args) { System.Console.WriteLine(format, args); }

        public ExternalDBApplicationResult OnShutdown(ControlledApplication application)
        {
            return ExternalDBApplicationResult.Succeeded;
        }
    }
}